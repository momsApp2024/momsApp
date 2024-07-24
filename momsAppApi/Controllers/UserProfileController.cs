﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using momsAppServer.Dal;
using momsAppServer.Models;
using BCrypt.Net;


namespace momsAppServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase

    {
        private readonly DataContext _context;

        public UserProfileController(DataContext context)
        {
            _context = context;
        }

        //        [HttpPatch("{id}")]
        //        public async Task<IActionResult> UpdateProfile(int PersonalInformationId, [FromBody] Dictionary<string, object> updates)
        //        {
        //            var userProfile = await _context.PersonalInformations.FindAsync(PersonalInformationId);
        //            if (userProfile == null)
        //            {
        //                return NotFound();
        //            }

        //            foreach (var update in updates)
        //            {
        //                var propertyInfo = typeof(PersonalInformation).GetProperty(update.Key, BindingFlags.Public | BindingFlags.Instance);
        //                if (propertyInfo != null && propertyInfo.CanWrite)
        //                {
        //                    propertyInfo.SetValue(userProfile, Convert.ChangeType(update.Value, propertyInfo.PropertyType), null);
        //                }
        //            }

        //            await _context.SaveChangesAsync();
        //            return NoContent();
        //        }
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        //{
        //    var user = await _context.PersonalInformations
        //        .FirstOrDefaultAsync(u => u.Email == loginRequest.Email);

        //    if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        //    {
        //        //return Unauthorized();
        //        return BadRequest("Invalid credentials"); // Or any other appropriate status code

        //    }

        //    var isProfileComplete = user.IsProfileComplete;
        //    return Ok(new { RequiresRegistration = !isProfileComplete });
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _context.PersonalInformations
                .FirstOrDefaultAsync(u => u.Email == loginRequest.Email);

            if (user == null)
            {
                // Return a specific status code or message indicating a new user
                return NotFound(new { Message = "New user" });
            }

            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
            {
                // Return a specific status code or message indicating invalid credentials
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            var isProfileComplete = user.IsProfileComplete;
            return Ok(new { RequiresRegistration = !isProfileComplete });
        }

        //        [HttpPost("external-login")]
        //        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginRequest request)
        //        {
        //            var user = await _context.PersonalInformations
        //                .FirstOrDefaultAsync(u => u.Email == request.Email);

        //            if (user == null)
        //            {
        //                user = new PersonalInformations
        //                {
        //                    Email = request.Email,
        //                    FirstName = request.FirstName,
        //                    LastName = request.LastName,
        //                    IsProfileComplete = false
        //                };
        //                _context.PersonalInformations.Add(user);
        //                await _context.SaveChangesAsync();
        //            }

        //            var isProfileComplete = user.IsProfileComplete;
        //            return Ok(new { RequiresRegistration = !isProfileComplete });
        //        }
        [HttpGet("districts")]
        public async Task<IActionResult> GetDistricts()
        {
            var districts = await _context.Districts.ToListAsync();
            return Ok(districts);
        }
        [HttpGet("{email}")]
        public async Task<IActionResult> GetProfile(string email)
        {
            var user = await _context.PersonalInformations
                                     .Include(pi => pi.Address)
                                     .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateProfile(string email, [FromBody] PersonalInformation updatedProfile)
        {
            var user = await _context.PersonalInformations
                                     .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = updatedProfile.FirstName;
            user.LastName = updatedProfile.LastName;
            user.Phone = updatedProfile.Phone;
            user.ChildNumber = updatedProfile.ChildNumber;
            user.ChildGender = updatedProfile.ChildGender;
            user.ChildDateOfBirth = updatedProfile.ChildDateOfBirth;
            user.AddressId = updatedProfile.AddressId;
            user.IsProfileComplete = true;

            _context.PersonalInformations.Update(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}