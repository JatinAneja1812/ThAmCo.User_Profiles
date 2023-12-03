using Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Route("GetAllCustomers")]
        public ActionResult<List<UserProfilesDTO>> GetAllCustomser()
        {
            try
            {
                List<UserProfilesDTO> result = _userService.GetAllCustomser();

                if (result == null)
                {
                    return StatusCode(500,
                        "Failed to retrieve all customers details from the database. If this error presists contact administrator");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                 new EventId((int)LogEventIdEnum.UnknownError),
                 $"Unexpected exception was caught in UserProfilesController at GetAllCustomser() .\nException:\n{ex.Message}\nInner exception:\n{ex.InnerException}\nStack trace:\n{ex.StackTrace}");

                return StatusCode(500, "Server error. An unknown error occurred on the server..");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetStaffDetails")]
        public ActionResult<UserProfilesDTO> GetStaffData([FromHeader] string Email)
        {
            try
            {
                UserProfilesDTO result = _userService.GetStaffData(Email);

                if (result == null)
                {
                    return StatusCode(500,
                        "Failed to retrieve currently logged in staff user details from the database. If this error presists contact administrator");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                 new EventId((int)LogEventIdEnum.UnknownError),
                 $"Unexpected exception was caught in UserProfilesController at GetStaffData() .\nException:\n{ex.Message}\nInner exception:\n{ex.InnerException}\nStack trace:\n{ex.StackTrace}");

                return StatusCode(500, "Server error. An unknown error occurred on the server..");
            }
        }

        [Authorize]
        [HttpPost]
        [Route("AddUser")]
        public ActionResult<bool> AddNewUser([FromBody] UserProfilesDTO userProfileDTO)
        {
            try
            {
                bool result = _userService.AddNewUser(userProfileDTO);

                if (!result)
                {
                    return StatusCode(500,
                        "Failed to add new user to the database. If this error presists contact administrator");
                }

                return Ok(result);
            }
            catch (UserExistsException)
            {
                return StatusCode(400, "User already exists in the database. Try again with different values.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                 new EventId((int)LogEventIdEnum.UnknownError),
                 $"Unexpected exception was caught in UserProfilesController at AddNewUser() .\nException:\n{ex.Message}\nInner exception:\n{ex.InnerException}\nStack trace:\n{ex.StackTrace}");

                return StatusCode(500, "Server error. An unknown error occurred on the server..");
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("RemoveUser")]
        public ActionResult<bool> RemoveExistingUser([FromHeader] string UserId)
        {
            try
            {
                bool result = _userService.DeleteUser(UserId);

                if (!result)
                {
                    return StatusCode(500,
                        "Failed to remove existing user from the database. If this error presists contact administrator.");
                }

                return Ok(result);
            }
            catch (DataNotFoundException)
            {
                return StatusCode(400, "User selected to remove does not exists in the database.Try to refresh your browser.If this error presists contact administrator.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                 new EventId((int)LogEventIdEnum.UnknownError),
                 $"Unexpected exception was caught in UserProfilesController at RemoveExistingUser() .\nException:\n{ex.Message}\nInner exception:\n{ex.InnerException}\nStack trace:\n{ex.StackTrace}");

                return StatusCode(500, "Server error. An unknown error occurred on the server..");
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateUser")]
        public ActionResult<bool> UpdateExistingUser([FromBody] UserProfilesDTO userProfileDTO)
        {
            try
            {
                bool result = _userService.UpdateUser(userProfileDTO);

                if (!result)
                {
                    return StatusCode(500,
                        "Failed to update existing user details to the database. If this error presists contact administrator.");
                }

                return Ok(result);
            }
            catch (DataNotFoundException)
            {
                return StatusCode(400, "User selected to update does not exists in the database.Try to refresh your browser.If this error presists contact administrator.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                 new EventId((int)LogEventIdEnum.UnknownError),
                 $"Unexpected exception was caught in UserProfilesController at UpdateExistingUser() .\nException:\n{ex.Message}\nInner exception:\n{ex.InnerException}\nStack trace:\n{ex.StackTrace}");

                return StatusCode(500, "Server error. An unknown error occurred on the server..");
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateUserFunds")]
        public ActionResult<bool> UpdateExistingUserFunds(CustomerFundsDTO customerFundsDTO)
        {
            try
            {
                bool result = _userService.UpdateCustomerFunds(customerFundsDTO);

                if (!result)
                {
                    return StatusCode(500,
                        "Failed to update user funds to the database. If this error presists contact administrator.");
                }

                return Ok(result);
            }
            catch (DataNotFoundException)
            {
                return StatusCode(400, "User selected to update does not exists in the database.Try to refresh your browser.If this error presists contact administrator.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                 new EventId((int)LogEventIdEnum.UnknownError),
                 $"Unexpected exception was caught in UserProfilesController at UpdateExistingUserFunds() .\nException:\n{ex.Message}\nInner exception:\n{ex.InnerException}\nStack trace:\n{ex.StackTrace}");

                return StatusCode(500, "Server error. An unknown error occurred on the server..");
            }
        }
    }
}
