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
    public class EmployeeSalaryMasterController : Controller
    {
        private HRM_DBEntities db = new HRM_DBEntities();

        // GET: EmployeeSalaryMaster
        public async Task<ActionResult> Index()
        {
            var m_EmployeeSalaryMaster = db.M_EmployeeSalaryMaster.Include(m => m.M_CommonMasterTable).Include(m => m.M_EmployeeMasters);
            return View(await m_EmployeeSalaryMaster.ToListAsync());
        }

        // GET: EmployeeSalaryMaster/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_EmployeeSalaryMaster m_EmployeeSalaryMaster = await db.M_EmployeeSalaryMaster.FindAsync(id);
            if (m_EmployeeSalaryMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_EmployeeSalaryMaster);
        }

        // GET: EmployeeSalaryMaster/Create
        public ActionResult Create()
        {
            ViewBag.SalaryTypeID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue");
            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName");
            return View();
        }

        // POST: EmployeeSalaryMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SalaryID,EmployeeID,SalaryTypeID,Basic,DA,HRA,SPECIAL_ALLOWANCE,AWARDS,MEDICAL_ALLOWANCE,GROSS_EARNING,GROSS_DEDUCTIONS,NET_Pay,Remarks,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active")] M_EmployeeSalaryMaster m_EmployeeSalaryMaster)
        {
            if (ModelState.IsValid)
            {
                db.M_EmployeeSalaryMaster.Add(m_EmployeeSalaryMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SalaryTypeID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", m_EmployeeSalaryMaster.SalaryTypeID);
            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", m_EmployeeSalaryMaster.EmployeeID);
            return View(m_EmployeeSalaryMaster);
        }

        // GET: EmployeeSalaryMaster/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_EmployeeSalaryMaster m_EmployeeSalaryMaster = await db.M_EmployeeSalaryMaster.FindAsync(id);
            if (m_EmployeeSalaryMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.SalaryTypeID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", m_EmployeeSalaryMaster.SalaryTypeID);
            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", m_EmployeeSalaryMaster.EmployeeID);
            return View(m_EmployeeSalaryMaster);
        }

        // POST: EmployeeSalaryMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SalaryID,EmployeeID,SalaryTypeID,Basic,DA,HRA,SPECIAL_ALLOWANCE,AWARDS,MEDICAL_ALLOWANCE,GROSS_EARNING,GROSS_DEDUCTIONS,NET_Pay,Remarks,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active")] M_EmployeeSalaryMaster m_EmployeeSalaryMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_EmployeeSalaryMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SalaryTypeID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", m_EmployeeSalaryMaster.SalaryTypeID);
            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", m_EmployeeSalaryMaster.EmployeeID);
            return View(m_EmployeeSalaryMaster);
        }

        // GET: EmployeeSalaryMaster/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_EmployeeSalaryMaster m_EmployeeSalaryMaster = await db.M_EmployeeSalaryMaster.FindAsync(id);
            if (m_EmployeeSalaryMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_EmployeeSalaryMaster);
        }

        // POST: EmployeeSalaryMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            M_EmployeeSalaryMaster m_EmployeeSalaryMaster = await db.M_EmployeeSalaryMaster.FindAsync(id);
            db.M_EmployeeSalaryMaster.Remove(m_EmployeeSalaryMaster);
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
