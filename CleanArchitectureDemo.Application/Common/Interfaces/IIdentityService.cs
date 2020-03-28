using CleanArchitectureDemo.Application.Common.Handler;
using CleanArchitectureDemo.Application.Role.Models;
using CleanArchitectureDemo.Application.System.Models;
using CleanArchitectureDemo.Application.User.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<UserModel>> CreateUserAsync(UserModel userModel);
        Task<Result<RoleModel>> CreateRoleAsync(RoleModel roleModel);
        Task<bool> UserExisted(string emil);
        Task CreateActionsAsync(IEnumerable<ActionModel> actions);
        Task<bool> RoleExsited(string role);
        Task<Result<string>> Login(LoginModel loginModel);
        Task<bool> IsUserAuthorized(AuthorizationModel authorizationModel);
    }
}
