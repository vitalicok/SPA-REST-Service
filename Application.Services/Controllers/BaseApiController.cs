using Application.Data.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Application.Services.Controllers
{
    public class BaseApiController : ApiController
    {
        public BaseApiController(IRepositoryData data)
        {
            this.Data = data;
        }

        protected IRepositoryData Data { get; private set; }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return this.InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return this.BadRequest();
                }
                return this.BadRequest(this.ModelState);
            }
            return null;
        }
    }
}