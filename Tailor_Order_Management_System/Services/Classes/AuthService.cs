using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tailor_Order_Management_System.Helpres;
using Tailor_Order_Management_System.Models.DbContext;
using Tailor_Order_Management_System.Models.DTOs.Incoming;
using Tailor_Order_Management_System.Models.DTOs.Outgoing;
using Tailor_Order_Management_System.Models.EntityClasses;
using Tailor_Order_Management_System.Services.Interfaces;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Tailor_Order_Management_System.Services.Classes
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly ApplicationDbContext _context;
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, ApplicationDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _jwt = jwt.Value;
            _context = dbContext;
        }
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {

            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "This Email Is registred!!" };
            if (await _userManager.FindByNameAsync(model.Name) is not null)
                return new AuthModel { Message = "This Username Is registred!!" };
            var User = new ApplicationUser
            {
                UserName = model.Name,
                Email = model.Email,

            };
            var result = await _userManager.CreateAsync(User, model.Password);
            if (!result.Succeeded)
            {
                string errors = "";
                foreach (var error in result.Errors)
                {
                    errors += error.Description + " ,";
                }
                return new AuthModel { Message = errors };
            }
            await _userManager.AddToRoleAsync(User, model.Role);

            var jwtSecurityToken = await CreateJwtToken(User);

            return new AuthModel
            {
                Email = User.Email,
                //ExpirasOn = jwtSecurityToken.ValidTo,
                IsAuthentcated = true,
                Roles = new List<string> { model.Role },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = User.UserName,
            };
        }
        public async Task<AuthModel> GetTokenAsync(TokenRequestModel tokenRequestModel)
        {
            var authModel = new AuthModel();
            if (tokenRequestModel is  null)
            {
                authModel.Message = "tokenRequestModel nulll !!";
            }
            var user = await _userManager.FindByNameAsync(tokenRequestModel.UserName);
            if (user is null || !await _userManager.CheckPasswordAsync(user, tokenRequestModel.Password))
            {
                authModel.Message = "Emial or Password is incorrect!!";
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var roleList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthentcated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            //authModel.ExpirasOn = jwtSecurityToken.ValidTo;
            authModel.Roles = roleList.ToList();
            var activeRefreshTokens =  _context.RefreshTokens.Where(rt => rt.UserId == user.Id ).ToList();
            //var activeRefreshTokens = user.RefreshTokens.Any(rt => rt.IsActive);
            if (activeRefreshTokens is not null)
            {
                var activeRefreshToken = activeRefreshTokens.FirstOrDefault(rt => rt.IsActive);
                authModel.RefreshToken = activeRefreshToken.Token;
                authModel.RefreshTokenExpirasOn = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken(user);
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpirasOn = refreshToken.ExpiresOn;
                _context.RefreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync();
            }
            
            return authModel;
        }
        public async Task<AuthModel> RefreshTokenAsync(string token)
        {
            var refreshToken = _context.RefreshTokens.Include(rt => rt.User).FirstOrDefault(rt => rt.Token == token);
            if (refreshToken is null)
                return new AuthModel { Message = "Invalid Token" };
            if (!refreshToken.IsActive)
                return new AuthModel { Message = "Token Expired" };


            refreshToken.RevokedOn = DateTime.UtcNow;
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                return new AuthModel { Message = "Something went wrong" };

            var refreshTokenNew = GenerateRefreshToken(refreshToken.User);
            _context.RefreshTokens.Add(refreshTokenNew);
            await _context.SaveChangesAsync();

            var user = refreshToken.User;
            var jwtSecurityToken = await CreateJwtToken(user);
            var roleList = await _userManager.GetRolesAsync(user);
            var authModel = new AuthModel
            {
                IsAuthentcated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                Username = user.UserName,
                Roles = roleList.ToList(),
                RefreshToken = refreshToken.Token,
                RefreshTokenExpirasOn = refreshToken.ExpiresOn
            };
            return authModel;
        }
        public async Task<bool> RevokeTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
            if (refreshToken == null || !refreshToken.IsActive)
            {
                return false;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;


            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<string> AddRloeAsync(AddRoleModel addRoleModel)
        {
            var User = await _userManager.FindByIdAsync(addRoleModel.UserId);
            if (User is null || !await _roleManager.RoleExistsAsync(addRoleModel.RoleName))
            {
                return "Invali User ID or Role";
            }
            if (await _userManager.IsInRoleAsync(User, addRoleModel.RoleName))
            {
                return "User arealy assign in this role";
            }
            var result = await _userManager.AddToRoleAsync(User, addRoleModel.RoleName);
            return result.Succeeded ? "the user added to " + addRoleModel.RoleName + "Succesded" : "Something went wrong";




        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id)
            }
            .Union(userClims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(

                issuer: _jwt.Issuer,
                audience: _jwt.Audiense,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        private RefreshToken GenerateRefreshToken(ApplicationUser user )
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddMinutes(2),
                CreatedOn = DateTime.UtcNow,
                UserId= user.Id
            };

        }

    }
}
