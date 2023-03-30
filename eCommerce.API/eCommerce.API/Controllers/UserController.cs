using eCommerce.API.Models;
using eCommerce.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Buscar()
        {
            return Ok(_userRepository.Get()); // HTTP - 200
        }
        
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var returnedUser = _userRepository.GetById(id);

            if (returnedUser == null)
                return NotFound();

            return Ok(returnedUser); // HTTP - 200
        }
        [HttpPost]
        public IActionResult Inserir(User usuario)
        {
            _userRepository.Insert(usuario);

            return Ok();
        }
        //[HttpPut]

        //[HttpDelete]
    }
}
