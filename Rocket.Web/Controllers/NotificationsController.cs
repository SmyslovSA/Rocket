using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Common.Logging;
using Rocket.Web.Helpers;
using Rocket.Web.Models;

namespace Rocket.Web.Controllers
{
    //todo admin filter
    [System.Web.Http.RoutePrefix("api/notifications")]
    public class NotificationsController : ApiController
    {
        private readonly IPushNotificationsHelper _pushNotificationsHelper;
        private readonly ILog _log;

        public NotificationsController(IPushNotificationsHelper pushNotificationsHelper, ILog log)
        {
            _pushNotificationsHelper = pushNotificationsHelper;
            _log = log;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("notifyAllPush")]
        public ActionResult NotifyAllPush(string msg)
        {
            try
            {
                _pushNotificationsHelper.SendPushNotificationsToAll(msg);
            }
            catch (Exception e)
            {
                //todo log
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("notifyOfReleasePush")]
        public ActionResult NotifyOfReleasePush(IEnumerable<PushNotificationModel> notifications)
        {
            //_log.Info("start NotifyOfReleasePush"); //todo
            try
            {
                _pushNotificationsHelper.SendPushNotificationsOfRelease(notifications);

            }
            catch (Exception e)
            {
                //todo log
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            //_log.Info("end NotifyOfReleasePush");

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }
    }
}