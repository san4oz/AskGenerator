﻿using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Business.Managers
{
    public class TeacherManager : BaseEntityManager<Teacher, ITeacherProvider>, ITeacherManager
    {
        protected ITeacherQuestionManager TQ { get; private set; }

        public TeacherManager(ITeacherProvider provider, ITeacherQuestionManager tqManager)
            : base(provider)
        {
            TQ = tqManager;
        }

        public List<Student> GetRelatedStudents(string teacherId)
        {
            return Provider.GetRelatedStudents(teacherId);
        }

        public bool Create(Teacher teacher, ICollection<string> ids)
        {
            var r = Provider.Create(teacher, ids);
            if (r)
                OnCreated(teacher);
            return r;
        }

        public bool Update(Teacher teacher, ICollection<string> ids)
        {
            return Provider.Update(teacher, ids);
        }

        public List<Teacher> List()
        {
            var key = GetListKey();
            var list = FromCache(key, Provider.List);
            foreach (var t in list) t.Initialize();
            return list;
        }

        public List<Teacher> All(bool loadMarks)
        {
            var teachers = this.List();
            if (!loadMarks)
                return teachers;
            var answers = TQ.All().ToLookup(tq => tq.TeacherId);
            foreach (var t in teachers)
            {
                var list = answers[t.Id];
                t.Marks = list.ToList();
                float avg = 0;
                int count = 0;
                t.Marks = t.Marks.Where(m => m.Answer != 0).ToList();
                foreach (var mark in t.Marks)
                {
                    avg += mark.Answer;
                    count++;
                }
                if (avg != 0)
                    avg /= (float)count;
                else
                    avg = -0.001f;
                t.Marks.Insert(0, new TeacherQuestion() { Answer = avg, QuestionId = Question.AvarageId });

            }
            return teachers;
        }

        public Task<List<Teacher>> AllAsync(bool loadMarks)
        {
            return Task.Factory.StartNew(() => All(loadMarks));
        }

        public Task<List<Teacher>> ListAsync()
        {
            return Task.Factory.StartNew(() => this.List());
        }
    }
}