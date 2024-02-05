using ContactFrom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactFrom.Controllers
{
    public class Web_Quality_ContactController : Controller
    {
        // GET: Web_Quality_Contact
        public ActionResult Index()
        {
            ViewBag.types = new Web_Quality_ContactType().Web_Quality_GetContactType();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Web_Quality_Contact obj)
        {
            try
            {
                ViewBag.types = new Web_Quality_ContactType().Web_Quality_GetContactType();
                var result = new Web_Quality_Contact().Web_Quality_InsertContact(obj);
                return Json(new { Success = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = "An error occurred on your request." });
            }
        }

    }
}