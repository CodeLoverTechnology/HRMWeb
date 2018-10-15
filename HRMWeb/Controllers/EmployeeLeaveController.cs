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
    public class EmployeeLeaveController : Controller
    {
        private HRM_DBEntities db = new HRM_DBEntities();

        // GET: EmployeeLeave
        public async Task<ActionResult> Index()
        {
            if (Session["LoginUserID"].ToString() == Resources.HRMResources.AdminUser)
            {
                var t_EmployeeLeave = db.T_EmployeeLeave.Include(t => t.M_CommonMasterTable).Include(t => t.M_EmployeeMasters).Include(t => t.M_EmployeeMasters1);
                return View(await t_EmployeeLeave.ToListAsync());
            }
            else
            {
                string EmployeeCode = Session["LoginUserID"].ToString();
                var m_EmployeeMasters = db.T_EmployeeLeave.Where(x => x.EmployeeID == EmployeeCode).OrderByDescending(x => x.CreatedDate).Include(t => t.M_CommonMasterTable).Include(t => t.M_EmployeeMasters).Include(t => t.M_EmployeeMasters1);
                return View(await m_EmployeeMasters.ToListAsync());
            }
            
        }

        // GET: EmployeeLeave/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EmployeeLeave t_EmployeeLeave = await db.T_EmployeeLeave.FindAsync(id);
            if (t_EmployeeLeave == null)
            {
                return HttpNotFound();
            }
            return View(t_EmployeeLeave);
        }

        // GET: EmployeeLeave/Create
        public ActionResult Create()
        {
            string EmpCode = Session["LoginUserID"].ToString();
            ViewBag.TypeOfLeaveID = new SelectList(db.M_CommonMasterTable.Where(x=>x.TableName== "TypeOfLeave"), "ID", "FieldValue");
            if (Session["LoginUserID"] != null && Session["LoginUserID"].ToString() == Resources.HRMResources.AdminUser)
            {
                ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName");
            }
            else
            {
                ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters.Where(x => x.EmployeeID == EmpCode), "EmployeeID", "EmployeeName");
            }
            ViewBag.ApproverManagerID = new SelectList(db.M_EmployeeMasters.Where(x=>x.ManagerID==null), "EmployeeID", "EmployeeName");
            return View();
        }

        // POST: EmployeeLeave/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeID,LeaveReason,LeaveFromDate,LeaveToDate,NoOfLeave,TypeOfLeaveID,ApproverManagerID,ComponsetReason,LeaveStatus,LeaveFileAttachment")] T_EmployeeLeave t_EmployeeLeave)
        {
            if (ModelState.IsValid)
            {
                t_EmployeeLeave.NoOfLeave = (t_EmployeeLeave.LeaveToDate - t_EmployeeLeave.LeaveFromDate).TotalDays;
                t_EmployeeLeave.CreatedBy = Session["LoginUserID"].ToString();
                t_EmployeeLeave.CreatedDate = DateTime.Now;
                t_EmployeeLeave.ModifiedBy = Session["LoginUserID"].ToString();
                t_EmployeeLeave.ModifiedDate = DateTime.Now;
                t_EmployeeLeave.Active = true;

                db.T_EmployeeLeave.Add(t_EmployeeLeave);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            string EmpCode = Session["LoginUserID"].ToString();
            ViewBag.TypeOfLeaveID = new SelectList(db.M_CommonMasterTable.Where(x => x.TableName == "TypeOfLeave"), "ID", "FieldValue");
            if (Session["LoginUserID"] != null && Session["LoginUserID"].ToString() == Resources.HRMResources.AdminUser)
            {
                ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName");
            }
            else
            {
                ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters.Where(x => x.EmployeeID == EmpCode), "EmployeeID", "EmployeeName");
            }
            ViewBag.ApproverManagerID = new SelectList(db.M_EmployeeMasters.Where(x => x.ManagerID == null), "EmployeeID", "EmployeeName");
            ViewBag.LeaveStatus = new SelectList(db.M_CommonMasterTable.Where(x => x.TableName == "LeaveStatus"), "ID", "FieldValue");
            return View(t_EmployeeLeave);
        }

        // GET: EmployeeLeave/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EmployeeLeave t_EmployeeLeave = await db.T_EmployeeLeave.FindAsync(id);
            if (t_EmployeeLeave == null)
            {
                return HttpNotFound();
            }
            string EmpCode = Session["LoginUserID"].ToString();
            ViewBag.TypeOfLeaveID = new SelectList(db.M_CommonMasterTable.Where(x => x.TableName == "TypeOfLeave"), "ID", "FieldValue");
            if (Session["LoginUserID"] != null && Session["LoginUserID"].ToString() == Resources.HRMResources.AdminUser)
            {
                ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName");
            }
            else
            {
                ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters.Where(x => x.EmployeeID == EmpCode), "EmployeeID", "EmployeeName");
            }
            ViewBag.ApproverManagerID = new SelectList(db.M_EmployeeMasters.Where(x => x.ManagerID == null), "EmployeeID", "EmployeeName");
            ViewBag.LeaveStatus = new SelectList(db.M_CommonMasterTable.Where(x => x.TableName == "LeaveStatus"), "ID", "FieldValue");
            return View(t_EmployeeLeave);
        }

        // POST: EmployeeLeave/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LeaveID,EmployeeID,LeaveReason,LeaveFromDate,LeaveToDate,NoOfLeave,TypeOfLeaveID,ApproverManagerID,ComponsetReason,LeaveStatus,LeaveFileAttachment,CreatedBy,CreatedDate,Active")] T_EmployeeLeave t_EmployeeLeave)
        {
            if (ModelState.IsValid)
            {
                t_EmployeeLeave.ModifiedBy = Session["LoginUserID"].ToString();
                t_EmployeeLeave.ModifiedDate = DateTime.Now;
                db.Entry(t_EmployeeLeave).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TypeOfLeaveID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", t_EmployeeLeave.TypeOfLeaveID);
            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", t_EmployeeLeave.EmployeeID);
            ViewBag.ApproverManagerID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", t_EmployeeLeave.ApproverManagerID);
            return View(t_EmployeeLeave);
        }

        // GET: EmployeeLeave/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EmployeeLeave t_EmployeeLeave = await db.T_EmployeeLeave.FindAsync(id);
            if (t_EmployeeLeave == null)
            {
                return HttpNotFound();
            }
            return View(t_EmployeeLeave);
        }

        // POST: EmployeeLeave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            T_EmployeeLeave t_EmployeeLeave = await db.T_EmployeeLeave.FindAsync(id);
            db.T_EmployeeLeave.Remove(t_EmployeeLeave);
            await db.SaveChangesAsync();
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
