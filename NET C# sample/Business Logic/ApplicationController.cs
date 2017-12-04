using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ReleaseMgmtWeb.Models;
using ReleaseMgmtWeb.DAL;
using ReleaseMgmtWeb.Filters;

namespace ReleaseMgmtWeb.Controllers
{
    [AuthenticationFilter]
    public class ApplicationController : Controller
    {
        private ReleaseMgmtDataEntities db = new ReleaseMgmtDataEntities();

        // GET: Application
        public ActionResult Index()
        {
            return View(db.Application.ToList());
        }

        // GET: Application/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Application.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Application/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Application/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EPRId,ApplicationName,ApplicationAlias,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Application application)
        {
            if (ModelState.IsValid)
            {
                //Lookup current user.
                string userName = User.Identity.Name;

                //Checks windows user is not NULL. Else, adds error to Null value.
                if (userName != null)
                {
                    // checks to see if current Windows User is already in the database, if not: throw error Access Denied.
                    if (db.Users.Any(a => a.UserID == userName))
                    {
                        //Get current Windows User's Id
                        var userId = db.Users.Where(a => a.UserID == userName).Select(b => b.Id).First();
                        
                        //Data for {CreatedBy, CreatedDate, UpdatedBy, UpdatedDate}
                        application.CreatedBy = userId;
                        application.CreatedDate = DateTime.Now;
                        application.UpdatedBy = userId;
                        application.UpdatedDate = application.CreatedDate;

                        // checks to see if ERPID is already in the database, if not: saves change else: adds error to model state
                        if (!db.Application.Any(a => a.EPRId == application.EPRId))
                        {
                            //save changes
                            db.Application.Add(application);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "An Application with this ERPID already exists in the database.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error: Access Denied. Contact website Administrator.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error: Windows User is NULL value.");
                }
            }
            return View(application);
        }

        // GET: Application/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Application.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Application/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EPRId,ApplicationName,ApplicationAlias,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Application application)
        {
            if (ModelState.IsValid)
            {
                //Lookup current user.
                string userName = User.Identity.Name;

                //Checks windows user is not NULL. Else, adds error to Null value.
                if (userName != null)
                {
                    // checks to see if current Windows User is already in the database, if not: throw error Access Denied.
                    if (db.Users.Any(a => a.UserID == userName))
                    {
                        //Get current Windows User's Id
                        var userId = db.Users.Where(a => a.UserID == userName).Select(b => b.Id).First();
                        
                        //Data for {UpdatedBy, UpdatedDate}
                        application.UpdatedBy = userId;
                        application.UpdatedDate = DateTime.Now;
                        
                        // checks to see if ERPID already exists in the database and is not the original to be edited, if not: save changes else: add error to model state
                        if (!db.Application.Any(a => a.EPRId == application.EPRId && a.Id != application.Id))
                        {
                            //Save changes
                            db.Entry(application).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "An Application with this ERPID already exists in the database.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error: Access Denied. Contact website Administrator.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error: Windows User is NULL value.");
                }
            }
            return View(application);
        }

        // GET: Application/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Application.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Application.Find(id);
            try
            {
                db.Application.Remove(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // catches database constraint exception. 
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An Application Release is dependent on this EPRId, this application cannot be deleted.");
                return View(application);
            }
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
