using System.Security.Claims;
using A2Template.Data;
using A2Template.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using A2Template.Helper;

namespace A2Template.Controllers
{
    [Route("webapi")]
    [ApiController]
    public class WebApiController : Controller
    {
        private readonly IA2Repo _repo;

        public WebApiController(IA2Repo repo)
        {
            _repo = repo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> RegisterAsync(User user)
        {
            //check if entered correctly, no missing values
            if (string.IsNullOrEmpty(user.UserName))
            {
                return Ok("UserName cannot be empty.");
            }

            //check if username taken
            if (await _repo.GetUserByUserNameAsync(user.UserName) != null)
            {
                return Ok($"UserName {user.UserName} is not available.");
            }

            await _repo.RegisterUserAsync(user);
            return Ok("User successfully registered.");
        }

        [Authorize(AuthenticationSchemes = "MyAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("Donation/{amount}")]
        public async Task<ActionResult<DonationCert>> MakeADonationAsync(int amount)
        {
            if (amount <= 0)
            {
                return BadRequest("Amount must be a positive number.");
            }

            Claim claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (claim == null) {
                return Unauthorized();
            }
            var donation = new DonationCert
            {
                UserName = claim.Value.ToString(),
                Amount = amount
            };
            return Ok(donation);
        }

        [Authorize(AuthenticationSchemes = "MyAuthentication")]
        [Authorize(Policy = "StaffOnly")]
        [HttpPost("AddEvent")]
        public async Task<ActionResult<String>> AddEventAsync(EventInput newEvent)
        {
            //format checking
            string pattern = @"^\d{8}T\d{6}Z$";
            bool startValid = Regex.IsMatch(newEvent.start, pattern);
            bool endValid = Regex.IsMatch(newEvent.end, pattern);

            if (!startValid && !endValid)
            {
                return BadRequest(new
                {
                    message = "Bad request",
                    errorCode = 400,
                    detail = "The format of Start and End should be yyyyMMddTHHmmssZ."
                });
            }
            if (!startValid)
            {
                return BadRequest(new
                {
                    message = "Bad request",
                    errorCode = 400,
                    detail = "The format of Start should be yyyyMMddTHHmmssZ."
                });
            }
            if (!endValid)
            {
                return BadRequest(new
                {
                    message = "Bad request",
                    errorCode = 400,
                    detail = "The format of End should be yyyyMMddTHHmmssZ."
                });
            }

            await _repo.AddEventAsync(newEvent);
            return Ok("Success");
        }

        [Authorize(AuthenticationSchemes = "MyAuthentication")]
        [Authorize(Policy = "StaffOnly")]
        [HttpGet("EventCount")]
        public async Task<ActionResult<int>> CountEvents()
        {
            var events = await _repo.GetAllEventsAsync();
            return Ok(events.Count());
        }

        [Authorize(AuthenticationSchemes = "MyAuthentication")]
        [Authorize(Policy = "StaffOnly")]
        [HttpGet("Event/{id}")]
        public async Task<ActionResult> GetEventById(int id)
        {
            var ev = await _repo.GetEventByIdAsync(id);
            if (ev == null)
            {
            return BadRequest($"Event {id} does not exist.");
            }

            string calendarContent = CalendarOutputFormatter.FormatEventAsCalendar(ev);

            return new ContentResult
            {
                Content = calendarContent,
                ContentType = "text/calendar; charset=utf-8",
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
