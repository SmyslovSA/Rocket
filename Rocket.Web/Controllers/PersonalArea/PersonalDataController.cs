using FluentValidation;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace Rocket.Web.Controllers.PersonalArea
{
    [RoutePrefix("personal/user/info")]
    public class ChangePersonalDataController : ApiController
    {
        private IPersonalData _ipersonaldata;

        public ChangePersonalDataController(IPersonalData personalData)
        {
            _ipersonaldata = personalData;
        }

        [HttpPut]
        public IHttpActionResult UpdateUserPersonalData(SimpleUser user)
        {
            if (user == null)
            {
                return BadRequest("User data cannot be empty");
            }

            try
            {
                _ipersonaldata.ChangePersonalData(user);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }

        [HttpPut]
        public IHttpActionResult UpdateUserPassword(SimpleUser user, string password, string passwordConfirm)
        {
            if (user == null)
            {
                return BadRequest("User data cannot be empty");
            }

            try
            {
                _ipersonaldata.ChangePasswordData(user, password, passwordConfirm);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }
    }
}