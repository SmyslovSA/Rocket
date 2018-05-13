using AutoMapper;
using FluentValidation;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.UoW;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using Ninject;
using Rocket.BL.Services.PersonalArea;


namespace Rocket.Web.Controllers.PersonalArea
{
    [RoutePrefix("PersonalArea/UserInfo")]
    public class ChangePersonalDataController : ApiController
    {
        
        private IPersonalData _ipersonaldata;
        private IUnitOfWork _unitOfWork;

        public ChangePersonalDataController()
        {
            Mapper.Initialize(cfg => cfg.CreateMissingTypeMaps = true);
            var container = new StandardKernel();
            container.Bind<IPersonalData>().To<ChangePersonalDataService>();
            var moq = new Mock<IUnitOfWork>();
            moq.Setup(x => x.UserRepository.Insert(It.IsAny<DbAuthorisedUser>()));
            container.Bind<IUnitOfWork>().ToMethod(r => moq.Object);
            _unitOfWork = moq.Object;
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public IHttpActionResult GetUserPersonalDataById(int id)
        {
            var model = _unitOfWork.UserRepository.GetById(id);
            return model == null ? (IHttpActionResult)NotFound() : Ok(model);
        }

        [HttpPost]
        public IHttpActionResult CreateUserPersonalData(SimpleUser user)
        {
            if (user == null)
            {
                return BadRequest("User data cannot be empty");
            }          
            var model = Mapper.Map<DbAuthorisedUser>(user);
            _unitOfWork.UserRepository.Insert(model);
            return Created($"users/{model.Id}",model);
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

        [HttpDelete]
        public IHttpActionResult DeleteUserPersonalData(SimpleUser user)
        {
            if (user == null)
            {
                return BadRequest("User data cannot be empty");
            }
            _unitOfWork.UserRepository.Delete(user);
            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }
    }
}
