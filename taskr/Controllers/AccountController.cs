using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using taskr.Dtos;
using taskr.Interfaces;
using taskr.Models;

namespace taskr.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = new User
                {
                    UserName = userRequest.UserName,
                };

                var createdUser = await this.userManager.CreateAsync(user, userRequest.Password!);

                if (createdUser.Succeeded)
                {
                    var roleResult = await this.userManager.AddToRoleAsync(user, "User");

                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new UserResponse
                            {
                                UserName = user.UserName,
                                Token = this.tokenService.CreateToken(user)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            } catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRequest userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await this.userManager.Users.FirstOrDefaultAsync(user => user.UserName == userRequest.UserName!.ToLower());

            if (user == null)
            {
                return Unauthorized("Nome de usuário inválido");
            }

            var result = await this.signInManager.CheckPasswordSignInAsync(user, userRequest.Password!, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Nome de usuário inválido e/ou senha incorreta");
            }

            return Ok(
                new UserResponse
                {
                    UserName = user.UserName!,
                    Token = this.tokenService.CreateToken(user)
                }
            );
        }
    }
}