using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineTaskList.Web.Models;

namespace OnlineTaskList.Web.Controllers
{
    public class TaskItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TaskItems
        public ActionResult Index()
        {
            if (GetCurrentUser() == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult TableBuilder()
        {
            IEnumerable<TaskItem> tasks = GetUsersTasks();
            return PartialView("_TaskTable", tasks);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCreateNew([Bind(Include = "Id,Description")] TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                taskItem.User = GetCurrentUser();
                taskItem.IsComplete = false;
                taskItem.LastUpdated = DateTime.Now;
                db.TaskItems.Add(taskItem);
                db.SaveChanges();
                return PartialView("_TaskTable", GetUsersTasks());
            }

            return View(taskItem);
        }

        // GET: TaskItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskItem taskItem = db.TaskItems.Find(id);
            if (taskItem == null)
            {
                return HttpNotFound();
            }
            
            return View(taskItem);
        }

        // POST: TaskItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,IsComplete")] TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                taskItem.LastUpdated = DateTime.Now;
                db.Entry(taskItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskItem);
        }

        [HttpPost]
        public ActionResult AjaxEditExisting(int? id, bool isComplete)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskItem taskItem = db.TaskItems.Find(id);
            if (taskItem == null)
            {
                return HttpNotFound();
            }
            taskItem.IsComplete = isComplete;
            taskItem.LastUpdated = DateTime.Now;
            db.Entry(taskItem).State = EntityState.Modified;
            db.SaveChanges();
            return PartialView("_TaskTable", GetUsersTasks());
        }

        // GET: TaskItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskItem taskItem = db.TaskItems.Find(id);
            if (taskItem == null)
            {
                return HttpNotFound();
            }
            return View(taskItem);
        }

        // POST: TaskItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskItem taskItem = db.TaskItems.Find(id);
            db.TaskItems.Remove(taskItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private ApplicationUser GetCurrentUser()
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(
                u => u.Id == userId);
            return currentUser;
        }

        private IEnumerable<TaskItem> GetUsersTasks()
        {
            return db.TaskItems.ToList().Where(i => i.User == GetCurrentUser());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
