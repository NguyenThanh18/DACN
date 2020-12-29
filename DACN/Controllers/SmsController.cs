using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace DACN.Controllers
{
    public class SmsController : Controller
    {
        private DBContext db = new DBContext();

        private string accountSid = "AC745c0f25a5d22a09b5ac9b4a65ab7ad8";
        private string authToken = "7293676f5a15db4ccfde76941a18584f";
        private string serviceSid = "VA6e4555560e9122ac67161df99f4f8613";
        // GET: Sms
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveInfo(string cmnd, string PhoneNumber)
        {
            var session = (DACN.Common.UserLogin)Session[DACN.Common.CommonConstants.USER_SESSION];
            TaiKhoan tk = db.TaiKhoans.Find(session.userID);
            tk.CMND = cmnd;
            tk.QuyenHan = "post";
            db.SaveChanges();
            return RedirectToAction("Posting","Posting");
        }
        [HttpPost]
        public string sendOTP(string phoneNumber)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;  
            TwilioClient.Init(accountSid, authToken);
            CreateVerificationOptions options = new CreateVerificationOptions(serviceSid, phoneNumber, "sms");
            var verification = VerificationResource.Create(options);
            return verification.Status;
        }

        [HttpPost]
        public string verifyOTP(string phoneNumber, string OTP)
        {
            TwilioClient.Init(accountSid, authToken);
            var verificationCheck = VerificationCheckResource.Create(to: phoneNumber, code: OTP, pathServiceSid: serviceSid);
            return verificationCheck.Status;
        }
    }
}