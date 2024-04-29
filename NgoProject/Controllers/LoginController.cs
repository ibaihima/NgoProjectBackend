using NgoProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace NgoProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		public IConfiguration _config;
		private readonly AppDbContext context;
		public LoginController(IConfiguration config, AppDbContext context)
		{
			_config = config;
			this.context = context;
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Login([FromBody] UserLogin userLogin)
		{
			var user = Authenticate(userLogin);
			if (user != null)
			{
				//generate token
				var token = Generate(user);
				return Ok(token);
			}
			return NotFound("User not found");
		}

		private string Generate(UserModel user)
		{
			//generate the JWT here
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserName),
				new Claim(ClaimTypes.Email, user.EmailAddress),
				new Claim(ClaimTypes.GivenName, user.FirstName),
				new Claim(ClaimTypes.Surname, user.LastName),
				new Claim(ClaimTypes.Role, user.Role)
			};

			var token = new JwtSecurityToken(_config["JWT:Issuer"], _config["JWT:Audience"], claims, expires: DateTime.Now.AddMinutes(15),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		private UserModel Authenticate(UserLogin userLogin)
		{
			//Find user here
			var currentUser = context.Users.FirstOrDefault(o => o.UserName.ToLower() == userLogin.UserName.ToLower() && o.Password == userLogin.Password);

			if (currentUser != null)
			{
				return currentUser;
			}
			return null;
		}
		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<ActionResult<List<UserModel>>> AddUser(UserModel addEvent)
		{
			context.Users.Add(addEvent);
			context.SaveChanges();
			return Ok(context.Users);
		}
	}
}
