using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendRegistrationEmailAsyn(string name,string email,string confirmationCode);
    }
}
