using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp;
using WeatherApp.Controllers;
using WeatherApp.Models;

namespace WeatherApp.Tests.Controllers
{
    [TestClass]
    public class PlaceControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            PlaceController controller = new PlaceController(new PlaceRepository());

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        /*
        [TestMethod]
        public void About()
        {
            // Arrange
            PlaceController controller = new PlaceController(new PlaceRepository());

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            PlaceController controller = new PlaceController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
         */ 
    }
}
