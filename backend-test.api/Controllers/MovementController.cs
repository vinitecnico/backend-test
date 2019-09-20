using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_test.Repository;
using Microsoft.AspNetCore.Mvc;
// using testJokenpo.Entities;
// using testJokenpo.Repository;

namespace backend_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly IRepository _movementRepository;

        public MovementController(IRepository movementRepository)
        {
            _movementRepository = movementRepository;
        }

        // GET api/game/{player1}/{player2}
        [HttpGet("{player1}/{player2}")]
        public string Get()
        {
            // return _gameRepository.WinningPlay(player1.ToString(), player2.ToString());
            return "";
        }
    }
}
