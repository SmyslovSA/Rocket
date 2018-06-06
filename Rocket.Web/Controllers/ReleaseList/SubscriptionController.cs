﻿using IdentityServer3.Core.Extensions;
using Rocket.BL.Common.Services.ReleaseList;
using System.Web.Http;

namespace Rocket.Web.Controllers.ReleaseList
{
    [RoutePrefix("")]
    public class SubscriptionController : ApiController
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPut]
        [Route("subscribe/{id:int:min(1)}")]
        public IHttpActionResult Subscribe(int id)
        {
            // todo: обработка ошибок
            _subscriptionService.Subscribe(User.GetSubjectId(), id);
            return Ok();
        }

        [HttpPut]
        [Route("unsubscribe/{id:int:min(1)}")]
        public IHttpActionResult Unsubscribe(int id)
        {
            // todo: обработка ошибок
            _subscriptionService.Unsubscribe(User.GetSubjectId(), id);
            return Ok();
        }
    }
}
