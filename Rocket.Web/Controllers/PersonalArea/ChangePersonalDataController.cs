using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using FluentValidation;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.UoW;

namespace Rocket.Web.Controllers.PersonalArea
{
    [RoutePrefix("PersonalArea/UserInfo")]
    public class ChangePersonalDataController : ApiController
    {       
        //private IPersonalData _ipersonaldata;
        //private IUnitOfWork _unitOfWork;

        //[HttpGet]
        //[Route("{id:int:min(1)}")]
        //public IHttpActionResult GetUserPersonalDataById(int id)
        //{
        //    //var model = _unitOfWork.UserAuthorisedRepository.GetById(id);
        //    return model == null ? (IHttpActionResult)NotFound() : Ok(model);
        //}

        [HttpPost]
        public IHttpActionResult CreateUserPersonalData(SimpleUser user)
        {
            if (user == null)
            {
                return BadRequest("User data cannot be empty");
            }    
            
            var model = Mapper.Map<DbAuthorisedUser>(user);
            //_unitOfWork.UserAuthorisedRepository.Insert(model);

            return Created($"users/{model.Id}", model);
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
                //_ipersonaldata.ChangePersonalData(user);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }

        [HttpDelete]
        public IHttpActionResult DeleteUserPersonalData(SimpleUser user)
        {
            if (user == null)
            {
                return BadRequest("User data cannot be empty");
            }

            //_unitOfWork.UserAuthorisedRepository.Delete(user.Id);

            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }
    }
}
