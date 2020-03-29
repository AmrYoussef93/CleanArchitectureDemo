using CleanArchitectureDemo.Application.Common.Handler;
using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.Role.Models;
using CleanArchitectureDemo.Application.System.Models;
using CleanArchitectureDemo.Application.User.Models;
using CleanArchitectureDemo.Infrastructure.Identity.Context;
using CleanArchitectureDemo.Infrastructure.Identity.Entities;
using CleanArchitectureDemo.Infrastructure.Identity.Handler;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly NorthwindIdentityContext _identityContext;
        private readonly IdentitySetting _identitySetting;
        private readonly IEmailService _emailService;
        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<Role> roleManager, NorthwindIdentityContext identityContext,
            IOptions<IdentitySetting> identitySetting, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _identityContext = identityContext;
            _identitySetting = identitySetting.Value;
            _emailService = emailService;
        }
        public async Task<Result<RoleModel>> CreateRoleAsync(RoleModel roleModel)
        {
            var role = new Role()
            {
                Name = roleModel.Name,
                NormalizedName = roleModel.Name,
                DisplayName = roleModel.Name,
                AllActionsEnabled = roleModel.EnableActions
            };
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                return Result<RoleModel>.BadRequest(result.Errors.Select(e => e.Description).ToArray());
            }
            roleModel.Id = role.Id;
            return Result<RoleModel>.Created(roleModel);
        }
        public async Task<Result<UserModel>> CreateUserAsync(UserModel userModel)
        {
            var identityUser = new User()
            {
                UserName = userModel.Email,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                PhoneNumber = userModel.PhoneNumber,
                LockoutEnabled = false
            };
            // send email to user contain confirmation code , and temp password
            var result = await _userManager.CreateAsync(identityUser);
            if (result.Succeeded)
            {
                userModel.Id = identityUser.Id;
                if (userModel.Roles.Any())
                {
                    result = await _userManager.AddToRolesAsync(identityUser, userModel.Roles);
                    if (!result.Succeeded)
                        return Result<UserModel>.BadRequest(result.Errors.Select(e => e.Description).ToArray());
                }
                return Result<UserModel>.Created(userModel);
            }
            else
            {
                return Result<UserModel>.BadRequest(result.Errors.Select(e => e.Description).ToArray());
            }
        }
        public async Task CreateActionsAsync(IEnumerable<ActionModel> actions)
        {
            var existedActions = _identityContext.Actions.ToList();
            var identityActions = actions.Where(a => !existedActions.Any(x => x.Name == a.Name))
                                         .Select(a => new IdentityAction { Name = a.Name, ControllerName = a.ControllerName, DisplayName = a.DisplayName }).ToList();
            if (identityActions.Any())
            {
                await _identityContext.Actions.AddRangeAsync(identityActions);
                await _identityContext.SaveChangesAsync();
            }
        }

        public async Task<bool> UserExisted(string emil)
        {
            return (await _userManager.FindByEmailAsync(emil) != null) ? true : false;
        }

        public async Task<bool> RoleExsited(string role)
        {
            return await _roleManager.RoleExistsAsync(role);
        }
        public async Task<Result<string>> Login(LoginModel loginModel)
        {
            var identityUser = await _userManager.FindByEmailAsync(loginModel.Email);
            if (identityUser == null)
            {
                return Result<string>.BadRequest(new string[] { ErrorMessages.UserNotFound });
            }
            var result = await _signInManager.PasswordSignInAsync(identityUser, loginModel.Password, false, false);
            if (result.Succeeded)
            {
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, identityUser.Id.ToString()));
                claims.AddClaim(new Claim(ClaimTypes.Name, identityUser.UserName));
                claims.AddClaim(new Claim(ClaimTypes.Email, identityUser.Email));

                var userRoles = await _userManager.GetRolesAsync(identityUser);
                foreach (string roleName in userRoles)
                {
                    claims.AddClaim(new Claim(ClaimTypes.Role, roleName));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_identitySetting.SigningKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddHours(1),
                    IssuedAt = DateTime.Now,
                    NotBefore = DateTime.Now,
                    SigningCredentials = credentials
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Result<string>.Ok(tokenHandler.WriteToken(token));
            }
            else
            {
                return Result<string>.Unauthorized(new string[] { ErrorMessages.InvalidEmailOrPassword });
            }
        }

        public async Task<bool> IsUserAuthorized(AuthorizationModel authorizationModel)
        {
            var user = await _userManager.FindByIdAsync(authorizationModel.UserId);
            if (user == null)
                return false;
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(SystemRole.SystemAdmin))
                return true;
            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);
                var roleActions = _identityContext.RoleActions.Where(rlAction => rlAction.RoleId == role.Id).Include(rlAction => rlAction.Action).ToList();
                if (roleActions.Exists(rolAction => rolAction.Action.ControllerName == authorizationModel.ControllerName &&
                rolAction.Action.Name == authorizationModel.ActionName
                ))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<Result<RegisterUserResponse>> RegisterUserAsync(UserModel userModel)
        {
            var identityUser = new User()
            {
                UserName = userModel.Email,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                PhoneNumber = userModel.PhoneNumber,
                LockoutEnabled = false
            };
            var result = await _userManager.CreateAsync(identityUser, userModel.Password);
            if (result.Succeeded)
            {
                userModel.Id = identityUser.Id;
                result = await _userManager.AddToRoleAsync(identityUser, SystemRole.User);
                if (!result.Succeeded)
                    return Result<RegisterUserResponse>.BadRequest(result.Errors.Select(e => e.Description).ToArray());
                await _emailService.SendRegistrationEmailAsyn($"{userModel.FirstName} {userModel.LastName}", userModel.Email, "ASW123");
                return Result<RegisterUserResponse>.Created(new RegisterUserResponse() { Id = identityUser.Id, Email = userModel.Email });
            }
            else
            {
                return Result<RegisterUserResponse>.BadRequest(result.Errors.Select(e => e.Description).ToArray());
            }
        }
    }
}
