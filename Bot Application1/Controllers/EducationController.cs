﻿using System;
using System.Collections.Generic;

namespace Bot_Application1.IDialog
{
    [Serializable]
    internal class EducationController
    {
      
  
        internal string[] getStudyUnits()
        {
            return new string[]
            {
               "היסטוריה א'","היסטוריה ב'"
            };
        }

        internal IEnumerable<string> getStudyCategory(string unit)
        {
            return new string[]
            {
               "בית שני","ציונות'"
            };
        }

        internal static string[] getQuestion(string studySubject)
        {
            return new string[]
            {
               "שאלה כלשהי"
            };
        }
    }
}