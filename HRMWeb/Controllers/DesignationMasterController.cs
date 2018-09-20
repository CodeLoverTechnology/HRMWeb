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
    public class DesignationMasterController : Controller
    {
        private HRM_DBEntities db = new HRM_DBEntities();

        // GET: DesignationMaster
        public async Task<ActionResult> Index()
        {
            return View(await db.M_DesignationMaster.ToListAsync());
        }

        // GET: DesignationMaster/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_DesignationMaster m_DesignationMaster = await db.M_DesignationMaster.FindAsync(id);
            if (m_DesignationMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_DesignationMaster);
        }

        // GET: DesignationMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DesignationMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DesignationID,Designation,Description,OrderNo")] M_DesignationMaster m_DesignationMaster)
        {
            if (ModelState.IsValid)
            {
                m_DesignationMaster.CreatedBy = Session["LoginUserID"].ToString();
                m_DesignationMaster.CreatedDate = DateTime.Now;
                m_DesignationMaster.ModifiedBy = Session["LoginUserID"].ToString();
                m_DesignationMaster.ModifiedDate = DateTime.Now;
                m_DesignationMaster.Active = true;
                db.M_DesignationMaster.Add(m_DesignationMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(m_DesignationMaster);
        }

        // GET: DesignationMaster/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_DesignationMaster m_DesignationMaster = await db.M_DesignationMaster.FindAsync(id);
            if (m_DesignationMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_DesignationMaster);
        }

        // POST: DesignationMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DesignationID,Designation,Description,OrderNo,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active")] M_DesignationMaster m_DesignationMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_DesignationMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(m_DesignationMaster);
        }

        // GET: DesignationMaster/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_DesignationMaster m_DesignationMaster = await db.M_DesignationMaster.FindAsync(id);
            if (m_DesignationMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_DesignationMaster);
        }

        // POST: DesignationMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            M_DesignationMaster m_DesignationMaster = await db.M_DesignationMaster.FindAsync(id);
            db.M_DesignationMaster.Remove(m_DesignationMaster);
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
