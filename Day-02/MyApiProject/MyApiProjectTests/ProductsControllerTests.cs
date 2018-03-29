using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyApiProject;
using MyApiProject.Controllers;
using System.Web.Http;
using MyApiProject.Models;
using System.Web.Http.Results;
using System.Net;

namespace MyApiProjectTests
{
    [TestClass]
    public class ProductsControllerTests
    {
        [TestMethod]
        public void Returns_OK_Result_For_Existing_Product()
        {
            var controller = new ProductsController();

            IHttpActionResult response = controller.Get(9);

            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<Product>));

           /* var product = ((OkNegotiatedContentResult<Product>)(response)).Content;

            Assert.AreEqual(9, product.Id);
            */
        }

        [TestMethod]
        public void Returns_StatusCode_Result_For_NonExisting_Product()
        {
            var controller = new ProductsController();

            IHttpActionResult response = controller.Get(6);

            Assert.IsInstanceOfType(response, typeof(StatusCodeResult));

            
        }

        [TestMethod]
        public void Returns_NotFound_Result_For_NonExisting_Product()
        {
            var controller = new ProductsController();

            IHttpActionResult response = controller.Get(6);

            Assert.IsInstanceOfType(response, typeof(StatusCodeResult));

            var scr = (StatusCodeResult)response;

            Assert.AreEqual(HttpStatusCode.NotFound, scr.StatusCode);
            
        }
    }
}
