using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using testJokenpo.Entities;
// using testJokenpo.Repository;

namespace testJokenpo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        // private readonly IRepository _gameRepository;

        public MovementController()
        {
            //_gameRepository = gameRepository;
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
