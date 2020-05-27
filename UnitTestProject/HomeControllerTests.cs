using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;
using WingtipToys.Controllers;
using WingtipToys.Data.Entities;
using WingtipToys.Models;
using WingtipToys.Services;

namespace UnitTestProject
{
    public class HomeControllerTests
    {
        private HomeController _homeController;
        private Mock<IWingtipService> _wingtipMockService;
        private Mock<ILogger<HomeController>> _loggerMockService;

        [SetUp]
        public void Setup()
        {
            //Setup Mock services
            _wingtipMockService = new Mock<IWingtipService>();
            _loggerMockService = new Mock<ILogger<HomeController>>();
            _homeController = new HomeController(_loggerMockService.Object, _wingtipMockService.Object); 
        }

        [Test]
        public void When_CategoryIdIsValidAndProductsExistInDB_Then_ProductsWillBeReturned()
        {
            //Arrange 
            var productsResponse = new List<Product>()
            {
                new Product()
                {
                    ProductID = 1,
                    ProductName = "Convertible Car",
                    CategoryID = 1,
                    UnitPrice = 21.23
                },
                new Product()
                {
                    ProductID = 2,
                    ProductName = "Fast Car",
                    CategoryID = 1,
                    UnitPrice = 31.23
                }
            };

            _wingtipMockService.Setup(x => x.GetCategoryID(It.IsAny<string>())).Returns(1);
            _wingtipMockService.Setup(x => x.GetProductsByCategoryId(It.IsAny<int>())).Returns(productsResponse);

            //Act
            var result = (ViewResult)_homeController.Index();
            var modelResponse = (ProductViewModel) result.Model;

            //Assert
            Assert.IsNotNull(modelResponse);
            Assert.AreEqual(2, modelResponse.Products.ToList().Count);
        }

        [Test]
        public void When_CategoryIdIsInvalid_Then_NoProductsWillBeReturned()
        {
            //Arrange 
            _wingtipMockService.Setup(x => x.GetCategoryID(It.IsAny<string>())).Returns(-1);

            //Act
            var result = (ViewResult)_homeController.Index();
            var modelResponse = (ProductViewModel)result.Model;

            //Assert
            Assert.IsNull(modelResponse.Products);
        }

        [Test]
        public void When_CategoryIdIsValidAndProductsDoNotExistInDB_Then_NoProductsWillBeReturned()
        {
            //Arrange 
            _wingtipMockService.Setup(x => x.GetCategoryID(It.IsAny<string>())).Returns(1);
            _wingtipMockService.Setup(x => x.GetProductsByCategoryId(It.IsAny<int>())).Returns(new List<Product>());

            //Act
            var result = (ViewResult)_homeController.Index();
            var modelResponse = (ProductViewModel)result.Model;

            //Assert
            Assert.IsNull(modelResponse.Products);
        }
    }
}