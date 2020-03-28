using CleanArchitectureDemo.Application.Common.Handler;
using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.Role.Models;
using CleanArchitectureDemo.Application.System.Models;
using CleanArchitectureDemo.Application.User.Models;
using CleanArchitectureDemo.Infrastructure.Identity.Context;
using CleanArchitectureDemo.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<Role> roleManager, NorthwindIdentityContext identityContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _identityContext = identityContext;
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
            var result = await _userManager.CreateAsync(identityUser, userModel.Password);
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
    }
}
