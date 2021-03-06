﻿using System;
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
    public class EmployeeTimeSheetTableController : Controller
    {
        private HRM_DBEntities db = new HRM_DBEntities();

        // GET: EmployeeTimeSheetTable
        public async Task<ActionResult> Index()
        {
            var t_EmployeeTimeSheetTable = db.T_EmployeeTimeSheetTable.Include(t => t.M_CommonMasterTable).Include(t => t.M_EmployeeMasters).Include(t => t.M_ProjectMaster);
            return View(await t_EmployeeTimeSheetTable.ToListAsync());
        }

        // GET: EmployeeTimeSheetTable/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EmployeeTimeSheetTable t_EmployeeTimeSheetTable = await db.T_EmployeeTimeSheetTable.FindAsync(id);
            if (t_EmployeeTimeSheetTable == null)
            {
                return HttpNotFound();
            }
            return View(t_EmployeeTimeSheetTable);
        }

        // GET: EmployeeTimeSheetTable/Create
        public ActionResult Create()
        {
            ViewBag.TypeOfWorkID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue");
            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName");
            ViewBag.ProjectID = new SelectList(db.M_ProjectMaster, "ProjectID", "ProjectCode");
            return View();
        }

        // POST: EmployeeTimeSheetTable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TimeSheetID,EmployeeID,ProjectID,TypeOfWorkID,WorkDate,WorkingHours,WorkDescription,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active")] T_EmployeeTimeSheetTable t_EmployeeTimeSheetTable)
        {
            if (ModelState.IsValid)
            {
                db.T_EmployeeTimeSheetTable.Add(t_EmployeeTimeSheetTable);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TypeOfWorkID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", t_EmployeeTimeSheetTable.TypeOfWorkID);
            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", t_EmployeeTimeSheetTable.EmployeeID);
            ViewBag.ProjectID = new SelectList(db.M_ProjectMaster, "ProjectID", "ProjectCode", t_EmployeeTimeSheetTable.ProjectID);
            return View(t_EmployeeTimeSheetTable);
        }

        // GET: EmployeeTimeSheetTable/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EmployeeTimeSheetTable t_EmployeeTimeSheetTable = await db.T_EmployeeTimeSheetTable.FindAsync(id);
            if (t_EmployeeTimeSheetTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeOfWorkID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", t_EmployeeTimeSheetTable.TypeOfWorkID);
            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", t_EmployeeTimeSheetTable.EmployeeID);
            ViewBag.ProjectID = new SelectList(db.M_ProjectMaster, "ProjectID", "ProjectCode", t_EmployeeTimeSheetTable.ProjectID);
            return View(t_EmployeeTimeSheetTable);
        }

        // POST: EmployeeTimeSheetTable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TimeSheetID,EmployeeID,ProjectID,TypeOfWorkID,WorkDate,WorkingHours,WorkDescription,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active")] T_EmployeeTimeSheetTable t_EmployeeTimeSheetTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_EmployeeTimeSheetTable).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TypeOfWorkID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", t_EmployeeTimeSheetTable.TypeOfWorkID);
            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", t_EmployeeTimeSheetTable.EmployeeID);
            ViewBag.ProjectID = new SelectList(db.M_ProjectMaster, "ProjectID", "ProjectCode", t_EmployeeTimeSheetTable.ProjectID);
            return View(t_EmployeeTimeSheetTable);
        }

        // GET: EmployeeTimeSheetTable/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EmployeeTimeSheetTable t_EmployeeTimeSheetTable = await db.T_EmployeeTimeSheetTable.FindAsync(id);
            if (t_EmployeeTimeSheetTable == null)
            {
                return HttpNotFound();
            }
            return View(t_EmployeeTimeSheetTable);
        }

        // POST: EmployeeTimeSheetTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            T_EmployeeTimeSheetTable t_EmployeeTimeSheetTable = await db.T_EmployeeTimeSheetTable.FindAsync(id);
            db.T_EmployeeTimeSheetTable.Remove(t_EmployeeTimeSheetTable);
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
