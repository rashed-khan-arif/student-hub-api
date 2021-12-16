using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentHub.Models.Auth;
using StudentHub.Models.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StuedentHub.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }


        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var claimIdentity = new ClaimsIdentity(claims);
                var token = GenerateToken(claimIdentity.Claims);
                var refToken = GenerateRefrashToken();
                var header = _configuration["AppSettings:RequestTokenHeader"];

                var obj = new Authorization
                {
                    Token = token.Item1,
                    Roles = userRoles,
                    User = new UserWrapper { UserId = user.Id, FirstName = user.FirstName, LastName = user.LastName, Sex = user.Sex },
                    Expiration = token.Item2,
                    RefreshToken = refToken,
                    Header = header
                };

                return Ok(obj);
            }
            return Unauthorized();
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userNameExists = await userManager.FindByNameAsync(model.Username);
            if (userNameExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new SHResponse { Status = "Error", Message = "Username already exists!" });

            var emailExists = await userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new SHResponse { Status = "Error", Message = "Email already exists!" });

            var user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Sex = model.Sex,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new SHResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new SHResponse { Status = "Success", Message = "User created successfully!" });
        }


        [HttpGet("/token-validation")]
        public async Task<IActionResult> ValidateToken([FromQuery] string token)
        {
            var isValidToken = await Task.FromResult(ValidateJwtToken(token));

            return Ok($"Your token is: {isValidToken}");
        }

        private (string, DateTime) GenerateToken(IEnumerable<Claim> claims)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);
            var sighingkey = new SymmetricSecurityKey(key);

            var token = new JwtSecurityToken(
                issuer: "STUDENT_HUB",
                audience: "STUDENT_HUB",
                expires: DateTime.UtcNow.AddHours(5),
                claims: claims,
                signingCredentials: new SigningCredentials(sighingkey, SecurityAlgorithms.HmacSha256)
            );
            return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
        }

        private static string GenerateRefrashToken()
        {
            var randNum = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(randNum);
            }

            return Convert.ToBase64String(randNum);
        }

        private bool ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                //var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "sub").Value);

                // return account id from JWT token if validation successful
                return true;
            }
            catch
            {
                // return null if validation fails
                return false;
            }
        }
    }
}
