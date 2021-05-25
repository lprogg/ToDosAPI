using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    [Route("todos")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly ToDosContext _context;

        public ToDosController(ToDosContext context) => _context = context;
    }
}