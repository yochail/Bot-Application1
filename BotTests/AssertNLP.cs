﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject1;

namespace BotTests
{
     class AssertNLP
    {
        public static bool contains(string[] listOptions, string str) {
            try
            {
                contains(new List<string>(listOptions), str);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            }
        public static void contains(List<string> listOptions, string str)
        {
            var flag = false;
            var res = "[";
            foreach (var o in listOptions)
            {
                res += o + ",";
                if (contains(o, str)) {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                res += "]";
                res = str + " Not in Array " + listOptions;
                throw new AssertFailedException(res);
            }
        }


        public static bool contains(string sorcse, string input)
        {
            var counter = sorcse.Split(' ').Length;
            foreach (var o in input.Split(' '))
            {
                if (!sorcse.Contains(o)){
                    counter--;
                }
            }

            if (counter < sorcse.Split(' ').Length * 0.2) return false;
            else return true;
        }


        public static void contains(List<string> listOptions, string[] str)
        {
            var flage = false;
            var res = "[";
            foreach (var o in str)
            {
                try
                {
                    if(listOptions.Contains(new DialogsTestsBase().DBbotPhrase(Pkey.innerException)[0]))
                    {
                        throw new Exception("ExceptionInBot");
                    }

                    res += o + ",";
                    contains(listOptions, o);
                    flage = true;
                    break;
                }catch(AssertFailedException ex)
                {

                }
            }


                if (!flage)
                {
                    res += "]";
                    res = res + " Not in Array " + listOptions.FirstOrDefault();
                    throw new AssertFailedException(res);
                }
            
        }

        public static void contains(string[] listOptions, string[] str)
        {

              contains(new List<string>(listOptions), str);


        }
    }
}
