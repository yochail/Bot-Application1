﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bot_Application1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Model.dataBase;
using NLP.Models;
using Model;
using Model.Models;
using UnitTestProject1;

namespace Bot_Application1.Controllers.Tests
{
    [TestClass()]
    public class ConversationControllerTests : MockObjectTestBase
    {

        ConversationController convCtrl;

        [TestInitialize()]
        public void ConversationControllerTest()
        {
            initializeMocksObject();
            convCtrl = new ConversationController(mockUserFem.Object,mockStudySession.Object);
        }

        [TestMethod()]
        public void FindMatchFromOptionsTest()
        {
            // nlpControler.matchStrings
            mockNLPCtrl.Setup(x=>x.matchStrings(It.IsAny<string>(),It.IsAny<string>())).Returns((string x,string y) => (x.Contains(y) || y.Contains(x)) && x.Length > 0 && y.Length > 0 ? 100 : 0);
            convCtrl.NlpControler = mockNLPCtrl.Object;

            //good
           Assert.AreEqual(convCtrl.FindMatchFromOptions(new string[] { "1", "2", "3" }, "3 אופציה"),"3");
            //bad
            Assert.AreEqual(convCtrl.FindMatchFromOptions(new string[] { "1", "2", "3" }, "dsds אופציה" ), null);
            //sad
            Assert.AreEqual(convCtrl.FindMatchFromOptions(new string[] { "1", "2", "3" }, ""), null);
        }

        //[TestMethod()]
        //public void isStopSessionTest()
        //{
        //    //good
        //    Assert.IsTrue(convCtrl.isStopSession("מספיק"));
        //    //bad
        //    Assert.IsFalse(convCtrl.isStopSession("תמשיך"));
        //    //sad
        //    Assert.IsFalse(convCtrl.isStopSession(""));
        //}



        [TestMethod()]
        public void endOfSessionTest()
        {
            var hs = new List<IQuestion>();
            hs.Add(mockQuestion1.Object);
            hs.Add(mockQuestion2.Object);
            hs.Add(mockQuestion3.Object);

            mockStudySession.Setup(x => x.QuestionAsked).Returns(hs);
            //good
            convCtrl.Db = mockDB.Object;


            var a = convCtrl.endOfSession();
            var b = convCtrl.getPhrase(Pkey.goodSessionEnd, new string[] { }, new string[] { });
            Assert.AreEqual(convCtrl.endOfSession()[0], EnumVal(Pkey.goodSessionEnd));


            mockQuestion1.Setup(x => x.AnswerScore).Returns(30);
            mockQuestion2.Setup(x => x.AnswerScore).Returns(30);
            mockQuestion3.Setup(x => x.AnswerScore).Returns(30);

            hs = new List<IQuestion>();
            hs.Add(mockQuestion1.Object);
            hs.Add(mockQuestion2.Object);
            hs.Add(mockQuestion3.Object);
            mockStudySession.Setup(x => x.QuestionAsked).Returns(hs);

            Assert.AreEqual(convCtrl.endOfSession()[0], EnumVal(Pkey.badSessionEnd));

            //bad
            mockStudySession.Setup(x => x.QuestionAsked).Returns(new List<IQuestion>());
            Assert.AreEqual(convCtrl.endOfSession()[0], EnumVal(Pkey.earlyDiparture));



            //sad
            mockQuestion1.Setup(x => x.AnswerScore).Returns(-99999);

            hs = new List<IQuestion>();
            hs.Add(mockQuestion1.Object);
            hs.Add(mockQuestion2.Object);
            hs.Add(mockQuestion3.Object);
            mockStudySession.Setup(x => x.QuestionAsked).Returns(hs);

            Assert.AreEqual(convCtrl.endOfSession()[0], EnumVal(Pkey.badSessionEnd));
        }

      

        [TestMethod()]
        public void getNumTest()
        {
            //good
            Assert.AreEqual(convCtrl.getNum("50"), 50);
          //  Assert.AreEqual(convCtrl.getNum("מאה"), 100);


            //bad
            Assert.AreEqual(convCtrl.getNum("גדגדגדגד"), -1);

            //sad
            Assert.AreEqual(convCtrl.getNum(int.MinValue.ToString()), -1);
        }

        [TestMethod()]
        public void getNameTest()
        {
            //good
          //  Assert.AreEqual(convCtrl.getName("קוראים לי יוחאי"), "יוחאי");
            Assert.AreEqual(convCtrl.getName("יוחאי"), "יוחאי");


            //bad
            Assert.AreEqual(convCtrl.getName("אין לי שם"), null);

            //sad
        //    Assert.AreEqual(convCtrl.getName("asassasaas"), null);
        }

        [TestMethod()]
        public void getGenderValueTest()
        {
           
            //good
            //  Assert.AreEqual(convCtrl.getName("קוראים לי יוחאי"), "יוחאי");
            Assert.AreEqual(convCtrl.getGenderValue("בן"), "masculine");
            Assert.AreEqual(convCtrl.getGenderValue("בת"), "feminine");
            //Assert.AreEqual(convCtrl.getGenderValue("בחור"), "masculine");
            //Assert.AreEqual(convCtrl.getGenderValue("בחורה"), "feminine");
            //bad

            Assert.AreEqual(convCtrl.getGenderValue("אני א-מיני"), null);

            //sad
            Assert.AreEqual(convCtrl.getGenderValue("asassasaas"), null);
        }

        [TestMethod()]
        public void getClassTest()
        {
            //good
            //  Assert.AreEqual(convCtrl.getName("קוראים לי יוחאי"), "יוחאי");
            Assert.AreEqual(convCtrl.getClass("יא"), "יא");
            //Assert.AreEqual(convCtrl.getClass("שמינית"), "יב");
            //bad

            Assert.AreEqual(convCtrl.getClass("אני באקסטרני"), null);

            //sad
            Assert.AreEqual(convCtrl.getClass("asassasaas"), null);
        }

        //[TestMethod()]  //NOT_IMPLAMENTED_IN_THIS_VERSION
        //public void getGeneralFeelingTest()
        //{
        //    //good
        //    //  Assert.AreEqual(convCtrl.getName("קוראים לי יוחאי"), "יוחאי");
        //    Assert.AreEqual(convCtrl.getGeneralFeeling("סבבה"), "good");
        //    Assert.AreEqual(convCtrl.getGeneralFeeling("באסה"), "bad");
        //    //Assert.AreEqual(convCtrl.getClass("שמינית"), "יב");
        //    //bad

        //    Assert.AreEqual(convCtrl.getGeneralFeeling("גו גו פאוור רנג'ר"), null);

        //    //sad
        //    Assert.AreEqual(convCtrl.getGeneralFeeling("asassasaas"), null);
        //}


    }
}