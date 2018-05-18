using FluentValidation;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.Web.Properties;
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
                return BadRequest(Resources.UserEmptyData);
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

       
    }
}