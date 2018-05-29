﻿using FluentValidation;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.Web.Properties;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace Rocket.Web.Controllers.PersonalArea
{
    [RoutePrefix("personal/email")]
    public class EmailController : ApiController
    {
        private IEmailManager _emailEmailManager;

        public EmailController(IEmailManager emailManager)
        {
            _emailEmailManager = emailManager;
        }

        [HttpPost]
        [Route("add")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Created, "New model description", typeof(Email))]
        public IHttpActionResult AddEmail(int id, Email email)
        {
            if (email == null)
            {
                return BadRequest(Resources.EmptyEmail);
            }

            try
            {
                _emailEmailManager.AddEmail(id, email);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

            return Created($"add/{email.Id}", email);
        }

        [HttpDelete]
        [Route("delete/{id:int:min(1)}")]
        public IHttpActionResult DeleteEmail(int id)
        {
            _emailEmailManager.DeleteEmail(id);
            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }
    }
}