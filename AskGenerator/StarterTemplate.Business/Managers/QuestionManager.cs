using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Managers
{
    public class QuestionManager : BaseManager<Question, IQuestionProvider>, IQuestionManager
    {
        public QuestionManager(IQuestionProvider provider)
            : base(provider)
        { }

        /// <summary>
        /// Gets questions for certain category.
        /// </summary>
        /// <param name="isAboutTeacher">Indicates whether question about teachers should be retrived.</param>
        /// <returns>List of retrived questions.</returns>
        public List<Question> List(bool isAboutTeacher = false)
        {
            return Provider.List(isAboutTeacher);
        }

        /// <summary>
        /// Gets questions for certain category.
        /// </summary>
        /// <param name="isAboutTeacher">Indicates whether question about teachers should be retrived.</param>
        /// <returns>List of retrived questions.</returns>
        public Task<List<Question>> ListAsync(bool isAboutTeacher = false)
        {
            return new TaskFactory().StartNew(() => Provider.List(isAboutTeacher));
        }

        public Dictionary<string, Badge> CreateBadges()
        {
            var questions = this.List(true);
            var result = new Dictionary<string, Badge>(questions.Count * 2);
            foreach (var question in questions)
            {
                if (question.LeftLimit.AvgLimit > 0)
                {
                    var badge = new Badge()
                    {
                        Id = question.Id,
                        Image = question.LeftLimit.Image,
                        ToolTipFormat = "{0}",
                        Limit = question.LeftLimit.AvgLimit
                    };
                    result.Add(badge.Id + "l", badge);
                }
                if (question.RightLimit.AvgLimit > 0)
                {
                    var badge = new Badge()
                    {
                        Id = question.Id,
                        Image = question.RightLimit.Image,
                        ToolTipFormat = "{0}",
                        Limit = question.RightLimit.AvgLimit
                    };
                    result.Add(badge.Id + "r", badge);
                }
            }
            return result;
        }


        public Task<Dictionary<string, Badge>> CreateBadgesAsync()
        {
            return new TaskFactory().StartNew((Func<Dictionary<string, Badge>>)CreateBadges);
        }
    }
}
