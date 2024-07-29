using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NWBC_Assignment03.Models;
using NWBC_Assignment03.Helpers;
using NWBC_Assignment03.Exceptions;
using System.Threading.Tasks;

namespace NWBC_Assignment03.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtTokenHelper jwtTokenHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                DateOfBirth = model.DateOfBirth,
                Address = model.Address
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(new { Message = "User registered successfully" });
            }

            throw new BadRequestException("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenHelper.GenerateToken(user, roles);
                return Ok(new { Token = token });
            }

            throw new UnauthorizedAccessException("Login failed: Invalid email or password.");
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return Ok("Role assigned successfully");
            }

            throw new BadRequestException("Role assignment failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}