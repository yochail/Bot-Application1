﻿
using Bot_Application1.dataBase;
using Model.dataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLPtest.Models
{
    [Serializable]
    public class StudySession
    {

        public HashSet<Question> questionAsked = new HashSet<Question>();
        public Question currentQuestion = null;
        public int sessionLength = 3;

        public string SubCategory { get; set; }
        public string Category { get; set; }

        public HashSet<Question> QuestionAsked
        {
            get
            {
                return questionAsked;
            }

            set
            {
                questionAsked = value;
            }
        }


    }
}