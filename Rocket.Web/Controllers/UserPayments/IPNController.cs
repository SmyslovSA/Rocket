using Rocket.BL.Common.Services;
using Rocket.BL.Common.Services.User;
using Rocket.BL.Common.Services.UserPayment;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Rocket.Web.Controllers
{
    [RoutePrefix("ipn")]
    public class IPNController : ApiController
    {
        private class IPNContext
        {
            public HttpRequestMessage IPNRequest { get; set; }

            public string RequestBody { get; set; }

            public string Verification { get; set; } = String.Empty;
        }

        private readonly IUserPaymentService _userPaymentService;
        private readonly IUserAccountLevelService _userAccountLevelService;

        public IPNController(IUserPaymentService userPaymentService, IUserAccountLevelService userAccountLevelService)
        {
            _userPaymentService = userPaymentService;
            _userAccountLevelService = userAccountLevelService;
        }

        [HttpPost]
        [Route("receive")]
        public IHttpActionResult Receive()
        {
            IPNContext ipnContext = new IPNContext()
            {
                IPNRequest = Request
            };

           
            ipnContext.RequestBody = ipnContext.IPNRequest.Content.ToString();

            //Store the IPN received from PayPal
            LogRequest(ipnContext);

            //Fire and forget verification task
            Task.Run(() => VerifyTask(ipnContext));

            //Reply back a 200 code
            return StatusCode(HttpStatusCode.OK);
        }

        private void VerifyTask(IPNContext ipnContext)
        {
            try
            {
                var verificationRequest = WebRequest.Create("https://www.sandbox.paypal.com/cgi-bin/webscr");

                //Set values for the verification request
                verificationRequest.Method = "POST";
                verificationRequest.ContentType = "application/x-www-form-urlencoded";

                //Add cmd=_notify-validate to the payload
                string strRequest = "cmd=_notify-validate&" + ipnContext.RequestBody;
                verificationRequest.ContentLength = strRequest.Length;

                //Attach payload to the verification request
                using (StreamWriter writer = new StreamWriter(verificationRequest.GetRequestStream(), Encoding.ASCII))
                {
                    writer.Write(strRequest);
                }

                //Send the request to PayPal and get the response
                using (StreamReader reader = new StreamReader(verificationRequest.GetResponse().GetResponseStream()))
                {
                    ipnContext.Verification = reader.ReadToEnd();
                }
            }
            catch (Exception exception)
            {
                //Capture exception for manual investigation
            }

            ProcessVerificationResponse(ipnContext);
        }


        private void LogRequest(IPNContext ipnContext)
        {
            // Persist the request values into a database or temporary data store
        }

        private void ProcessVerificationResponse(IPNContext ipnContext)
        {
            if (ipnContext.Verification.Equals("VERIFIED"))
            {
                // check that Payment_status=Completed
                // check that Txn_id has not been previously processed
                // check that Receiver_email is your Primary PayPal email
                // check that Payment_amount/Payment_currency are correct
                // process payment
                var paymentInfo = ipnContext.RequestBody;
                var payment = new BL.Common.Models.UserPayment();

                int userID = 0; //TODO: взять ид юзера из сообщения о поступлении платежа

                payment.UserId = userID;
                payment.FirstName = new Regex(@"first_name\s*=(.*)").Match(paymentInfo).Groups[1].Value.Trim();
                payment.LastName = new Regex(@"last_name\s*=(.*)").Match(paymentInfo).Groups[1].Value.Trim();
                payment.Result = new Regex(@"payment_status\s*=(.*)").Match(paymentInfo).Groups[1].Value.Trim();
                payment.Email = new Regex(@"payer_email\s*=(.*)").Match(paymentInfo).Groups[1].Value.Trim();
                payment.Summ = decimal.Parse(new Regex(@"payment_gross\s*=(.*)").Match(paymentInfo).Groups[1].Value.Trim());
                payment.Currentcy = new Regex(@"mc_currency\s*=(.*)").Match(paymentInfo).Groups[1].Value.Trim();
                payment.CustomString = new Regex(@"custom\s*=(.*)").Match(paymentInfo).Groups[1].Value.Trim();
                _userPaymentService.AddUserPayment(payment);
                _userAccountLevelService.SetUserAccountLevel(userID, new BL.Common.Models.User.AccountLevel());
            }
            else if (ipnContext.Verification.Equals("INVALID"))
            {
                //Log for manual investigation
            }
            else
            {
                //Log error
            }
        }
    }
}
