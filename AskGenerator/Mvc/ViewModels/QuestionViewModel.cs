using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.ViewModels
{
    public class QuestionViewModel : BaseViewModel, IMapFrom<Question>
    {
        [Display(Name="Вопрос")]
        public string QuestionBody { get; set; }

        [Display(Name = "Нижняя отметка")]
        public string LowerRateDescription { get; set; }

        [Display(Name = "Верхняя отметка")]
        public string HigherRateDescription { get; set; }
    }
}
