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
    public class CompanyMastersController : Controller
    {
        private HRM_DBEntities db = new HRM_DBEntities();

        // GET: CompanyMasters
        public async Task<ActionResult> Index()
        {
            var m_CompanyMasters = db.M_CompanyMasters.Include(m => m.M_LocationMasters);
            return View(await m_CompanyMasters.ToListAsync());
        }

        // GET: CompanyMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_CompanyMasters m_CompanyMasters = await db.M_CompanyMasters.FindAsync(id);
            if (m_CompanyMasters == null)
            {
                return HttpNotFound();
            }
            return View(m_CompanyMasters);
        }

        // GET: CompanyMasters/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.M_LocationMasters, "LocationID", "State");
            return View();
        }

        // POST: CompanyMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompanyName,Address,ContactPerson,CompanyContactNo,EmailID,LocationID,Locality,OrderBy")] M_CompanyMasters m_CompanyMasters)
        {
            if (ModelState.IsValid)
            {
                m_CompanyMasters.CreatedBy = Session["LoginUserID"].ToString();
                m_CompanyMasters.CreatedDate = DateTime.Now;
                m_CompanyMasters.ModifiedBy = Session["LoginUserID"].ToString();
                m_CompanyMasters.ModifiedDate = DateTime.Now;
                m_CompanyMasters.Active = true;
                db.M_CompanyMasters.Add(m_CompanyMasters);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.M_LocationMasters, "LocationID", "State", m_CompanyMasters.LocationID);
            return View(m_CompanyMasters);
        }

        // GET: CompanyMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_CompanyMasters m_CompanyMasters = await db.M_CompanyMasters.FindAsync(id);
            if (m_CompanyMasters == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.M_LocationMasters, "LocationID", "State", m_CompanyMasters.LocationID);
            return View(m_CompanyMasters);
        }

        // POST: CompanyMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompanyID,CompanyName,Address,ContactPerson,CompanyContactNo,EmailID,LocationID,Locality,OrderBy,CreatedBy,CreatedDate,Active")] M_CompanyMasters m_CompanyMasters)
        {
            if (ModelState.IsValid)
            {
                m_CompanyMasters.ModifiedBy = Session["LoginUserID"].ToString();
                m_CompanyMasters.ModifiedDate = DateTime.Now;

                db.Entry(m_CompanyMasters).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.M_LocationMasters, "LocationID", "State", m_CompanyMasters.LocationID);
            return View(m_CompanyMasters);
        }

        // GET: CompanyMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_CompanyMasters m_CompanyMasters = await db.M_CompanyMasters.FindAsync(id);
            if (m_CompanyMasters == null)
            {
                return HttpNotFound();
            }
            return View(m_CompanyMasters);
        }

        // POST: CompanyMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            M_CompanyMasters m_CompanyMasters = await db.M_CompanyMasters.FindAsync(id);
            db.M_CompanyMasters.Remove(m_CompanyMasters);
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
