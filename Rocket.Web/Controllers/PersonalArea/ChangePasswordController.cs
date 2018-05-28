using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.Web.Properties;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace Rocket.Web.Controllers.PersonalArea
{
    [RoutePrefix("personal/user/password")]
    public class ChangePasswordController : ApiController
    {
        private IPersonalData _ipersonaldata;

        public ChangePasswordController(IPersonalData personalData)
        {
            _ipersonaldata = personalData;
        }

        [HttpPut]
        public IHttpActionResult UpdateUserPassword(SimpleUser user, string password, string passwordConfirm)
        {
            if (user == null)
            {
                return BadRequest(Resources.UserEmptyData);
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
