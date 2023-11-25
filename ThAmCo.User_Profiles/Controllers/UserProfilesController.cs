using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ThAmCo.User_Profiles.DTOs;
using ThAmCo.User_Profiles.Enums;
using ThAmCo.User_Profiles.Services.Service.Interfaces;

namespace ThAmCo.User_Profiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserProfilesController> _logger;

        public UserProfilesController(IUserService UserService, ILogger<UserProfilesController> Logger)
        {
            _userService = UserService;
            _logger = Logger;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllUsers")]
        public ActionResult<List<UserProfilesDTO>> GetAllOrders([FromHeader] string Authorization)
        {
            try
            {
                List<UserProfilesDTO> result = _userService.GetAllCustomser();

                if(result == null)
                {
                    return StatusCode(500, 
                        "Failed to retrieve all customers details from the database. If this error presists contact administrator");
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(
                 new EventId((int)LogEventIdEnum.UnknownError),
                 $"Unexpected exception was caught in ProductsController at GetAllProducts() .\nException:\n{ex.Message}\nInner exception:\n{ex.InnerException}\nStack trace:\n{ex.StackTrace}");

                return StatusCode(500, "Server error. An unknown error occurred on the server..");
            }
        }

    }
}
