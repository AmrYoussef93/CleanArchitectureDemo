using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureDemo.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        private IMediator mediator;
        protected IMediator _mediator => mediator ??= (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));
    }
}