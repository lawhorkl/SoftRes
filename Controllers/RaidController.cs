using Microsoft.AspNetCore.Mvc;
using SoftRes.Models;

namespace SoftRes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RaidController
    {
        public Raid Get()
        {
            return new Raid();
        }
    }
}