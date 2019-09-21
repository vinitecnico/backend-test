using System;
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

namespace Web.Api.UnitTest
{
    public class ExtractTest
    {
        private List<Movement> GetTestSessions()
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

        [Fact]
        public async Task GetAllMovementsTest()
        {
            // arrange
            var mockRepo = new Mock<IExtractRepository>();
            mockRepo.Setup(repo => repo.GetAllMovements())
                .ReturnsAsync(GetTestSessions());
            var controller = new ExtractController(mockRepo.Object);
            // Act
            var result = await controller.GetAllMovements();

            // Assert.True(result.Count > 0);
        }
    }
}