using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Rocket.BL.Common.Services;
using Rocket.BL.Common.Services.UserPayment;

namespace Rocket.Web.Controllers
{
    /// <summary>
    /// Контроллер WebApi работы с инфой о платеже.
    /// </summary>
    [RoutePrefix("userpayments")]
    public class UserPaymentsController : ApiController
    {
        private readonly IUserPaymentService _userPaymentService;

        public UserPaymentsController(IUserPaymentService userPaymentService)
        {
            _userPaymentService = userPaymentService;
        }

        /// <summary>
        /// Возвращает информацию о платеже.
        /// </summary>
        /// <returns>Информация о платеже.</returns>
        [HttpGet]
        [Route("paymentInfo")]
        public IHttpActionResult GetPaymentInfo()
        {
            Rocket.BL.Common.Models.User.User user = null;
            if (user == null)
            {
                //TODO: get current user
                return NotFound();
            }

            var payment = _userPaymentService.GetUserPayment(user);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }
    }
}
