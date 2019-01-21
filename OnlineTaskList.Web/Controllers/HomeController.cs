using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTaskList.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // I might want to add more features to my page than just a task list - This is where I'd put the homepage/menu
            return RedirectToAction("Index", "TaskItems");
        }
    }
}