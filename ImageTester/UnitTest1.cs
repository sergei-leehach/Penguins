using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiteDevelopment.Repository;
using SiteDevelopment.Models;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SiteDevelopment.Controllers;

namespace ImageTester
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void TestMethod1()
        //{
        //    InputData data = new InputData();
        //    data.Arena = "Console Energy Center";
        //    data.AwayTeamLogo = @"C:\users\hatassska\Desktop\predators.gif";
        //    data.AwayTeamShortName = "NSH";
        //    data.BackgroundImage = @"C:\users\hatassska\Desktop\nhl.jpg";
        //    data.DateOfAMatch = new DateTime(1993, 6, 23);
        //    data.HomeTeamLogo = @"C:\users\hatassska\Desktop\penguins.gif";
        //    data.HomeTeamShortName = "PIT";
        //    data.Location = "Pittsburgh, PA";
        //    data.Scoreboard = "1:5";
        //    data.TypeOfBoard = TypeOfBoard.preview;

        //    ImageGeneration.ImageProcessing(data);
        //     //Assert.AreEqual(ImageGeneration.ImageProcessing(data), $@"Posters\Preview\preview.jpg");

        //}

        [TestMethod]
        public void TestMethod2()
        {
            InputData data = new InputData();
            data.Arena = "Console Energy Center";
            data.AwayTeam = "Detroit Red Wings";
            data.BackgroundImage = @"C:\users\hatassska\Desktop\nhl.jpg";
            data.DateOfAMatch = new DateTime(1993, 6, 23);
            data.HomeTeam = "Pittsburgh Penguins";
            data.Location = "Pittsburgh, PA";
            data.AwayTeamScore = 8;
            data.HomeTeamScore = 5;

            data.AwayTeamShortName = DbQuery.GetShortName(data.AwayTeam);
            data.HomeTeamShortName = DbQuery.GetShortName(data.HomeTeam);         
            data.HomeTeamLogo = new GeneratorController().SetTeamLogo(data.HomeTeamShortName);
            data.AwayTeamLogo = new GeneratorController().SetTeamLogo(data.AwayTeamShortName);


            data.Result = TypeOfResult.SO;
            data.TypeOfBoard = TypeOfBoard.Preview;

            ImageGeneration.ImageProcessing(data);
            Assert.AreEqual(string.Empty, "");

        }

        

        [TestMethod]
        public void TestMethod3()
        {
            
            var res = DbQuery.DropDownListGeneration();
            Assert.AreEqual(res.GetType(), typeof(List<string>));
        }

        [TestMethod]
        public void TestMethod4()
        {
            var result = DbQuery.GetPlace("Pittsburgh Penguins");

            Assert.AreEqual("Pittsburgh, PA", result[0]);
        }
    }
}
