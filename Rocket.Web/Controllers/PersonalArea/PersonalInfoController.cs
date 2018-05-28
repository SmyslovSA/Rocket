﻿using FluentValidation;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.Web.Properties;
using System.Net;
using System.Web.Http;
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
        [Route("info/{id:int:min(1)}")]
        public IHttpActionResult UpdateUserPersonalInfo(int id, string firstName, string lastName, string avatar)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                return BadRequest(Resources.UserEmptyFirstNameOrLastName);
            }

            try
            {
                _ipersonaldata.ChangePersonalData(id,firstName,lastName,avatar);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }

        [HttpPut]
        [Route("password/{id:int:min(1)}")]
        public IHttpActionResult UpdateUserPassword(int id, string password, string passwordConfirm)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordConfirm))
            {
                return BadRequest(Resources.UserEmptyFirstNameOrLastName);
            }

            try
            {
                _ipersonaldata.ChangePasswordData(id, password, passwordConfirm);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }
    }
}