using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Web.Api.Controllers;
using Web.Api.Entities;
using Web.Api.Repository;
using Xunit;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Assert = NUnit.Framework.Assert;

namespace Web.Api.UnitTest
{
    public class ExtractControllerTest
    {
        private List<Movement> GetTestSessionsByListMoment()
        {
            var sessions = new List<Movement>();
            sessions.Add(new Movement()
            {
                data = DateTime.Now,
                descricao = "Auto Posto Shell",
                moeda = "R$",
                valor = -50.00,
                categoria = "transporte"
            });
            sessions.Add(new Movement()
            {
                data = DateTime.Now,
                descricao = "Auto Posto Shell",
                moeda = "R$",
                valor = -50.00,
                categoria = "transporte"
            });
            return sessions;
        }

        private Dictionary<string, double> GetTestSessionsByDictionary()
        {
            var sessions = new Dictionary<string, double>();
            sessions.Add("transporte", -150);
            sessions.Add("diversao", -200);
            return sessions;
        }

        [Fact]
        public async Task UploadFileDbLog()
        {
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "21-Mar;INGRESSO.COM;-159,53;diversao";
            var fileName = "db.log";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            var file = fileMock.Object;

            // arrange
            var mockRepo = new Mock<IExtractRepository>();
            mockRepo.Setup(repo => repo.UploadFileDbLog(file))
                .ReturnsAsync(true);
            var controller = new ExtractController(mockRepo.Object);
            // Act
            var result = await controller.UploadFileDbLog(file);

            Assert.True(result);
        }

        [Fact]
        public async Task GetAllMovementsTest()
        {
            // arrange
            var mockRepo = new Mock<IExtractRepository>();
            mockRepo.Setup(repo => repo.GetAllMovements())
                .ReturnsAsync(GetTestSessionsByListMoment());

            var controller = new ExtractController(mockRepo.Object);
            // Act
            var result = await controller.GetAllMovements();

            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task TotalByCategory()
        {
            // arrange
            var mockRepo = new Mock<IExtractRepository>();
            mockRepo.Setup(repo => repo.TotalByCategory())
                .ReturnsAsync(GetTestSessionsByDictionary());
            
            var controller = new ExtractController(mockRepo.Object);
            // Act
            var result = await controller.TotalByCategory();

            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task CustomerCategorySpentMore()
        {
            var item = new KeyValuePair<string, double>("transporte", -250);
            // arrange
            var mockRepo = new Mock<IExtractRepository>();
            mockRepo.Setup(repo => repo.CustomerCategorySpentMore())
                .ReturnsAsync(item);

            var controller = new ExtractController(mockRepo.Object);
            // Act
            var result = await controller.CustomerCategorySpentMore();

            Assert.True(!string.IsNullOrEmpty(result.Key));
        }

        [Fact]
        public async Task MonthCustomerCategorySpentMore()
        {
            var item = new KeyValuePair<string, double>("Jan", -500);
            // arrange
            var mockRepo = new Mock<IExtractRepository>();
            mockRepo.Setup(repo => repo.MonthCustomerCategorySpentMore())
                .ReturnsAsync(item);
            
            var controller = new ExtractController(mockRepo.Object);
            // Act
            var result = await controller.MonthCustomerCategorySpentMore();

            Assert.True(!string.IsNullOrEmpty(result.Key));
        }

        [Fact]
        public async Task MoneyCustomerSpent()
        {
            // arrange
            var mockRepo = new Mock<IExtractRepository>();
            mockRepo.Setup(repo => repo.MoneyCustomerSpent())
                .ReturnsAsync(-500);
            
            var controller = new ExtractController(mockRepo.Object);
            // Act
            var result = await controller.MoneyCustomerSpent();

            Assert.True(result < 0);
        }

        [Fact]
        public async Task MoneyCustomerReceived()
        {
            // arrange
            var mockRepo = new Mock<IExtractRepository>();
            mockRepo.Setup(repo => repo.MoneyCustomerReceived())
                .ReturnsAsync(1000);
            
            var controller = new ExtractController(mockRepo.Object);
            // Act
            var result = await controller.MoneyCustomerReceived();

            Assert.True(result > 0);
        }

        [Fact]
        public async Task TotalMovementCustomer()
        {
            // arrange
            var mockRepo = new Mock<IExtractRepository>();
            mockRepo.Setup(repo => repo.TotalMovementCustomer())
                .ReturnsAsync(-200);
            
            var controller = new ExtractController(mockRepo.Object);
            // Act
            var result = await controller.TotalMovementCustomer();

            Assert.True(result < 0);
        }
    }
}