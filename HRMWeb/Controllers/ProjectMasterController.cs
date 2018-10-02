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
    public class ProjectMasterController : Controller
    {
        private HRM_DBEntities db = new HRM_DBEntities();

        // GET: ProjectMaster
        public async Task<ActionResult> Index()
        {
            var m_ProjectMaster = db.M_ProjectMaster.Include(m => m.M_CompanyMasters);
            return View(await m_ProjectMaster.ToListAsync());
        }

        // GET: ProjectMaster/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_ProjectMaster m_ProjectMaster = await db.M_ProjectMaster.FindAsync(id);
            if (m_ProjectMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_ProjectMaster);
        }

        // GET: ProjectMaster/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.M_CompanyMasters, "CompanyID", "CompanyName");
            return View();
        }

        // POST: ProjectMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProjectCode,CompanyID,ProjectName,ProjectLocation")] M_ProjectMaster m_ProjectMaster)
        {
            if (ModelState.IsValid)
            {
                m_ProjectMaster.CreatedBy = Session["LoginUserID"].ToString();
                m_ProjectMaster.CreatedDate = DateTime.Now;
                m_ProjectMaster.ModifiedBy = Session["LoginUserID"].ToString();
                m_ProjectMaster.ModifiedDate = DateTime.Now;
                m_ProjectMaster.Active = true;
                db.M_ProjectMaster.Add(m_ProjectMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.M_CompanyMasters, "CompanyID", "CompanyName", m_ProjectMaster.CompanyID);
            return View(m_ProjectMaster);
        }

        // GET: ProjectMaster/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_ProjectMaster m_ProjectMaster = await db.M_ProjectMaster.FindAsync(id);
            if (m_ProjectMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.M_CompanyMasters, "CompanyID", "CompanyName", m_ProjectMaster.CompanyID);
            return View(m_ProjectMaster);
        }

        // POST: ProjectMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProjectID,ProjectCode,CompanyID,ProjectName,ProjectLocation,CreatedBy,CreatedDate,Active")] M_ProjectMaster m_ProjectMaster)
        {
            if (ModelState.IsValid)
            {
                m_ProjectMaster.ModifiedBy = Session["LoginUserID"].ToString();
                m_ProjectMaster.ModifiedDate = DateTime.Now;

                db.Entry(m_ProjectMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.M_CompanyMasters, "CompanyID", "CompanyName", m_ProjectMaster.CompanyID);
            return View(m_ProjectMaster);
        }

        // GET: ProjectMaster/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_ProjectMaster m_ProjectMaster = await db.M_ProjectMaster.FindAsync(id);
            if (m_ProjectMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_ProjectMaster);
        }

        // POST: ProjectMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            M_ProjectMaster m_ProjectMaster = await db.M_ProjectMaster.FindAsync(id);
            db.M_ProjectMaster.Remove(m_ProjectMaster);
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
