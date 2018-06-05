﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Rocket.BL.Common.Services.UserPayment;

namespace Rocket.Web.Controllers.UserPayments
{
    [RoutePrefix("ipn")]
    public class IPNController : ApiController
    {
        private class IPNContext
        {
            public HttpRequestMessage IPNRequest { get; set; }

            public string RequestBody { get; set; }

            public string Verification { get; set; } = string.Empty;
        }

        private readonly IUserPaymentService _userPaymentService;

        public IPNController(IUserPaymentService userPaymentService)
        {
            _userPaymentService = userPaymentService;
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
                var payment = new BL.Common.Models.UserPayment.UserPayment();
                //TODO:  parse info and write to payment
                var user = new Rocket.BL.Common.Models.User.User();
                _userPaymentService.AddUserPayment(payment);
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
