using Microsoft.AspNetCore.Mvc;
using SoftRes.Auth;
using SoftRes.Models;

namespace SoftRes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RaidController
    {
        private IBlizzardAuthHandler _authHandler;

        public RaidController(IBlizzardAuthHandler authHandler)
        {
            _authHandler = authHandler;
        }

        public Raid Get()
        {
            return new Raid();
        }
    }
}