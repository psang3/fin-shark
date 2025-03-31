using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signinManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signinManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                var user = await _userManager.Users
                    .FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

                if (user == null)
                    return Unauthorized(new { Error = "Invalid username or password" });

                var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (!result.Succeeded)
                    return Unauthorized(new { Error = "Invalid username or password" });

                return Ok(new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while logging in", Details = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
                    return BadRequest(new { Error = "Email already registered" });

                if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.UserName))
                    return BadRequest(new { Error = "Username already taken" });

                var user = new AppUser
                {
                    UserName = registerDto.UserName.ToLower(),
                    Email = registerDto.Email.ToLower(),
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded)
                    return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });

                var roleResult = await _userManager.AddToRoleAsync(user, "User");

                if (!roleResult.Succeeded)
                    return StatusCode(500, new { Errors = roleResult.Errors.Select(e => e.Description) });

                return Ok(new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while registering", Details = ex.Message });
            }
        }
    }
}