using FluentValidation;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.Web.Filters;
using Rocket.Web.Properties;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Rocket.Web.Controllers.PersonalArea
{
    [RoutePrefix("personal/user")]
    public class PersonalInfoController : ApiController
    {
        private IPersonalData _ipersonaldata;

        public PersonalInfoController(IPersonalData personalData)
        {
            _ipersonaldata = personalData;
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public IHttpActionResult GetAuthorisedUser(int id)
        {
            var user = _ipersonaldata.GetUserData(id);
            return user == null ? (IHttpActionResult)NotFound() : Ok(user);
        }

        [HttpPut]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Data is not valid", typeof(string))]
        [Route("info/{id:int:min(1)}")]
        public IHttpActionResult UpdateUserPersonalInfo(int id, string firstName, string lastName, string avatar)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                return BadRequest(Resources.UserEmptyFirstNameOrLastName);
            }

            try
            {
                _ipersonaldata.ChangePersonalData(id, firstName, lastName, avatar);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }

        [HttpPut]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Password is not valid", typeof(string))]
        [Route("password/{id:int:min(1)}")]
    //    [CustomException(ExceptionType = typeof(ValidationException),
    //StatusCode = HttpStatusCode.BadRequest, Message = "password wrong")]
        public IHttpActionResult UpdateUserPassword(int id, string password, string passwordConfirm)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordConfirm))
            {
                return BadRequest(Resources.EmptyPassword);
            }

            //try
            //{
                _ipersonaldata.ChangePasswordData(id, password, passwordConfirm);
            //}
            //catch (ValidationException exception)
            //{
            //    return BadRequest(exception.Message);
            //}

            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }
    }
}