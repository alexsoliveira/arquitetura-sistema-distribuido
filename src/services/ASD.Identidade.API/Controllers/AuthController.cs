using ASD.Identidade.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASD.Identidade.API.Controllers
{
    [ApiController]
    [Route("api/identidade")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager
            )
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        [HttpPost("nova-conta")]
        public async Task<IActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var user = new IdentityUser
                {
                    UserName = usuarioRegistro.Email,
                    Email = usuarioRegistro.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var result = await _signInManager.PasswordSignInAsync(
                    usuarioLogin.Email, 
                    usuarioLogin.Senha,
                    false,
                    true);

                if(result.Succeeded)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
