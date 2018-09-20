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
    public class M_EmployeeMastersController : Controller
    {
        private HRM_DBEntities db = new HRM_DBEntities();

        // GET: M_EmployeeMasters
        public async Task<ActionResult> Index()
        {
            var m_EmployeeMasters = db.M_EmployeeMasters.Include(m => m.M_CommonMasterTable).Include(m => m.M_CommonMasterTable1).Include(m => m.M_DesignationMaster).Include(m => m.M_EmployeeMasters2);
            return View(await m_EmployeeMasters.ToListAsync());
        }

        // GET: M_EmployeeMasters/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_EmployeeMasters m_EmployeeMasters = await db.M_EmployeeMasters.FindAsync(id);
            if (m_EmployeeMasters == null)
            {
                return HttpNotFound();
            }
            return View(m_EmployeeMasters);
        }

        // GET: M_EmployeeMasters/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.M_CommonMasterTable.Where(x => x.TableName == "Country"), "ID", "FieldValue");
            ViewBag.GenderID = new SelectList(db.M_CommonMasterTable.Where(x=>x.TableName== "Gender"), "ID", "FieldValue");
            ViewBag.DesignationID = new SelectList(db.M_DesignationMaster, "DesignationID", "Designation");
            ViewBag.ManagerID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName");
            return View();
        }

        // POST: M_EmployeeMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeID,EmployeeName,Address,City,State,CountryID,PersonalEmailID,CEmailID,ContactNo,EmergencyContactNo,GenderID,DOB,DOJ,Department,DesignationID,ManagerID,Pwd")] M_EmployeeMasters m_EmployeeMasters)
        {
            if (ModelState.IsValid)
            {
                m_EmployeeMasters.CreatedBy = Session["LoginUserID"].ToString();
                m_EmployeeMasters.CreatedDate = DateTime.Now;
                m_EmployeeMasters.ModifiedBy = Session["LoginUserID"].ToString();
                m_EmployeeMasters.ModifiedDate = DateTime.Now;
                m_EmployeeMasters.Active = true;
                db.M_EmployeeMasters.Add(m_EmployeeMasters);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CountryID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", m_EmployeeMasters.CountryID);
            ViewBag.GenderID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", m_EmployeeMasters.GenderID);
            ViewBag.DesignationID = new SelectList(db.M_DesignationMaster, "DesignationID", "Designation", m_EmployeeMasters.DesignationID);
            ViewBag.ManagerID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", m_EmployeeMasters.ManagerID);
            return View(m_EmployeeMasters);
        }

        // GET: M_EmployeeMasters/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_EmployeeMasters m_EmployeeMasters = await db.M_EmployeeMasters.FindAsync(id);
            if (m_EmployeeMasters == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", m_EmployeeMasters.CountryID);
            ViewBag.GenderID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", m_EmployeeMasters.GenderID);
            ViewBag.DesignationID = new SelectList(db.M_DesignationMaster, "DesignationID", "Designation", m_EmployeeMasters.DesignationID);
            ViewBag.ManagerID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", m_EmployeeMasters.ManagerID);
            return View(m_EmployeeMasters);
        }

        // POST: M_EmployeeMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EmployeeID,EmployeeName,Address,City,State,CountryID,PersonalEmailID,CEmailID,ContactNo,EmergencyContactNo,GenderID,DOB,DOJ,Department,DesignationID,ManagerID,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active")] M_EmployeeMasters m_EmployeeMasters)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_EmployeeMasters).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", m_EmployeeMasters.CountryID);
            ViewBag.GenderID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", m_EmployeeMasters.GenderID);
            ViewBag.DesignationID = new SelectList(db.M_DesignationMaster, "DesignationID", "Designation", m_EmployeeMasters.DesignationID);
            ViewBag.ManagerID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", m_EmployeeMasters.ManagerID);
            return View(m_EmployeeMasters);
        }

        // GET: M_EmployeeMasters/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_EmployeeMasters m_EmployeeMasters = await db.M_EmployeeMasters.FindAsync(id);
            if (m_EmployeeMasters == null)
            {
                return HttpNotFound();
            }
            return View(m_EmployeeMasters);
        }

        // POST: M_EmployeeMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            M_EmployeeMasters m_EmployeeMasters = await db.M_EmployeeMasters.FindAsync(id);
            db.M_EmployeeMasters.Remove(m_EmployeeMasters);
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

        public ActionResult ApplyLeave()
        {
            return View();
        }
    }
}
