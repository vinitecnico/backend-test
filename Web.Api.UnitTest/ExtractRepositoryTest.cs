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
using Microsoft.Extensions.Configuration;

namespace Web.Api.UnitTest
{
    public class ExtractRepositoryTest
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
        public async Task UploadFileDbLogExtractRepository()
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

            var configuration = new Mock<IConfiguration>();
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value)
            .Returns("https://my-json-server.typicode.com/cairano/backend-test");

            configuration.Setup(a => a.GetSection("BackendTest:BaseURL"))
            .Returns(configurationSection.Object);

            // arrange
            var mockRepo = new Mock<IExtractRepository>();
            mockRepo.Setup(repo => repo.UploadFileDbLog(file))
                .ReturnsAsync(true);
            var extractRepository = new ExtractRepository(configuration.Object, new System.Net.Http.HttpClient());
            // Act
            var result = await extractRepository.UploadFileDbLog(file);

            Assert.True(result);
        }

        [Fact]
        public async Task GetAllMovementsTestExtractRepository()
        {
            var configuration = new Mock<IConfiguration>();
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value)
            .Returns("https://my-json-server.typicode.com/cairano/backend-test");

            configuration.Setup(a => a.GetSection("BackendTest:BaseURL"))
            .Returns(configurationSection.Object);

            // arrange
            var mockRepo = new Mock<IExtractRepository>();
            mockRepo.Setup(repo => repo.GetAllMovements())
                .ReturnsAsync(GetTestSessionsByListMoment());
            var extractRepository = new ExtractRepository(configuration.Object, new System.Net.Http.HttpClient());

            // Act
            var result = await extractRepository.GetAllMovements();

            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task TotalByCategoryExtractRepository()
        {
            var configuration = new Mock<IConfiguration>();
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value)
            .Returns("https://my-json-server.typicode.com/cairano/backend-test");

            configuration.Setup(a => a.GetSection("BackendTest:BaseURL"))
            .Returns(configurationSection.Object);

            // arrange
            var mockRepo = new Mock<IExtractRepository>();
            mockRepo.Setup(repo => repo.TotalByCategory())
                .ReturnsAsync(GetTestSessionsByDictionary());

            var extractRepository = new ExtractRepository(configuration.Object, new System.Net.Http.HttpClient());

            // Act
            var result = await extractRepository.TotalByCategory();

            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task CustomerCategorySpentMoreExtractRepository()
        {
            var item = new KeyValuePair<string, double>("transporte", -250);

            var configuration = new Mock<IConfiguration>();
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value)
            .Returns("https://my-json-server.typicode.com/cairano/backend-test");

            // arrange
            var mockRepo = new Mock<IExtractRepository>();
            mockRepo.Setup(repo => repo.CustomerCategorySpentMore())
                .ReturnsAsync(item);

            mockRepo
            .Setup(repo => repo.GetMovementAll("pagamentos", "https://my-json-server.typicode.com/cairano/backend-test"))            
            .ReturnsAsync(GetTestSessionsByListMoment());

            var extractRepository = new ExtractRepository(configuration.Object, new System.Net.Http.HttpClient());            

            // Act
            var result = await extractRepository.GetMovementAll("pagamentos", "https://my-json-server.typicode.com/cairano/backend-test");

            Assert.True(result.Count > 0);
        }
    }
}