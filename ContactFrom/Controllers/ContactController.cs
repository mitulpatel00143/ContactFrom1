using ContactFrom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactFrom.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Index(Contact obj)
        {
            var result = new Contact().InsertContact(obj);
            return Json(new { Success = result }, JsonRequestBehavior.AllowGet);
        }
    }
}