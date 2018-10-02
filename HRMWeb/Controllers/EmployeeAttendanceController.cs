using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMWeb.DataModel;

namespace HRMWeb.Controllers
{
    public class EmployeeAttendanceController : Controller
    {
        private HRM_DBEntities db = new HRM_DBEntities();

        // GET: EmployeeAttendance
        public async Task<ActionResult> Index()
        {
            if (Session["LoginUserID"].ToString() == Resources.HRMResources.AdminUser)
            {
                var t_EmployeeAttendance = db.T_EmployeeAttendance.Include(t => t.M_EmployeeMasters);
                return View(await t_EmployeeAttendance.ToListAsync());
            }
            else
            {
                string EmployeeCode = Session["LoginUserID"].ToString();
                var m_EmployeeMasters = db.T_EmployeeAttendance.Where(x => x.EmployeeID == EmployeeCode).OrderByDescending(x=>x.CreatedDate);
                return View(await m_EmployeeMasters.ToListAsync());
            }
            
        }

        // GET: EmployeeAttendance/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EmployeeAttendance t_EmployeeAttendance = await db.T_EmployeeAttendance.FindAsync(id);
            if (t_EmployeeAttendance == null)
            {
                return HttpNotFound();
            }
            return View(t_EmployeeAttendance);
        }

        // GET: EmployeeAttendance/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName");
            return View();
        }

        // POST: EmployeeAttendance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeID,InTime,Reason")] T_EmployeeAttendance t_EmployeeAttendance)
        {
            //if (ModelState.IsValid)
            //{
                t_EmployeeAttendance.EmployeeID= Session["LoginUserID"].ToString();
                t_EmployeeAttendance.InTime = DateTime.Now;
                t_EmployeeAttendance.CreatedBy = Session["LoginUserID"].ToString();
                t_EmployeeAttendance.CreatedDate = DateTime.Now;
                t_EmployeeAttendance.ModifiedBy = Session["LoginUserID"].ToString();
                t_EmployeeAttendance.ModifiedDate = DateTime.Now;
                t_EmployeeAttendance.Active = true;
                db.T_EmployeeAttendance.Add(t_EmployeeAttendance);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            //}

            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", t_EmployeeAttendance.EmployeeID);
            return View(t_EmployeeAttendance);
        }

        // GET: EmployeeAttendance/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EmployeeAttendance t_EmployeeAttendance = await db.T_EmployeeAttendance.FindAsync(id);
            if (t_EmployeeAttendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", t_EmployeeAttendance.EmployeeID);
            return View(t_EmployeeAttendance);
        }

        // POST: EmployeeAttendance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AttendanceID,EmployeeID,InTime,OutTime,Reason,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active")] T_EmployeeAttendance t_EmployeeAttendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_EmployeeAttendance).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", t_EmployeeAttendance.EmployeeID);
            return View(t_EmployeeAttendance);
        }

        // GET: EmployeeAttendance/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EmployeeAttendance t_EmployeeAttendance = await db.T_EmployeeAttendance.FindAsync(id);
            if (t_EmployeeAttendance == null)
            {
                return HttpNotFound();
            }
            return View(t_EmployeeAttendance);
        }

        // POST: EmployeeAttendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            T_EmployeeAttendance t_EmployeeAttendance = await db.T_EmployeeAttendance.FindAsync(id);
            db.T_EmployeeAttendance.Remove(t_EmployeeAttendance);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult MarkAttendance()
        {
            T_EmployeeAttendance t_EmployeeAttendance = new T_EmployeeAttendance();
            t_EmployeeAttendance.EmployeeID = Session["LoginUserID"].ToString();
            t_EmployeeAttendance.InTime = DateTime.Now;
            t_EmployeeAttendance.CreatedBy = Session["LoginUserID"].ToString();
            t_EmployeeAttendance.CreatedDate = DateTime.Now;
            t_EmployeeAttendance.ModifiedBy = Session["LoginUserID"].ToString();
            t_EmployeeAttendance.ModifiedDate = DateTime.Now;
            t_EmployeeAttendance.Active = true;
            db.T_EmployeeAttendance.Add(t_EmployeeAttendance);
            db.SaveChangesAsync();
            ViewBag.AttendanceMark = "Attendance Mark Successfully on : " + t_EmployeeAttendance.InTime;
            return RedirectToAction("Index");
         }
        public ActionResult AttendanceLogout(int AttendanceID)
        {
            if (AttendanceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EmployeeAttendance t_EmployeeAttendance = db.T_EmployeeAttendance.Where(x=>x.AttendanceID== AttendanceID).OrderByDescending(x=>x.AttendanceID).FirstOrDefault();
            if (t_EmployeeAttendance == null)
            {
                return HttpNotFound();
            }
            t_EmployeeAttendance.OutTime = DateTime.Now;
            t_EmployeeAttendance.ModifiedBy = Session["LoginUserID"].ToString();
            t_EmployeeAttendance.ModifiedDate = DateTime.Now;
            db.Entry(t_EmployeeAttendance).State = EntityState.Modified;
            db.SaveChangesAsync();
            ViewBag.AttendanceMark = "Attendance Logout Successfully on : " + t_EmployeeAttendance.InTime;
            return RedirectToAction("Index");
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
