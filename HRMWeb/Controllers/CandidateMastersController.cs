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
    public class CandidateMastersController : Controller
    {
        private HRM_DBEntities db = new HRM_DBEntities();

        // GET: CandidateMasters
        public async Task<ActionResult> Index()
        {
            var m_CandidateMasters = db.M_CandidateMasters.Include(m => m.M_CommonMasterTable).Include(m => m.M_CompanyMasters).Include(m => m.M_LocationMasters).Include(m => m.M_RoleMaster);
            return View(await m_CandidateMasters.ToListAsync());
        }

        // GET: CandidateMasters/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_CandidateMasters m_CandidateMasters = await db.M_CandidateMasters.FindAsync(id);
            if (m_CandidateMasters == null)
            {
                return HttpNotFound();
            }
            return View(m_CandidateMasters);
        }

        // GET: CandidateMasters/Create
        public ActionResult Create()
        {
            ViewBag.CandidateStatusID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue");
            ViewBag.CompanyID = new SelectList(db.M_CompanyMasters, "CompanyID", "CompanyName");
            ViewBag.LocationID = new SelectList(db.M_LocationMasters, "LocationID", "State");
            ViewBag.RoleID = new SelectList(db.M_RoleMaster, "RoleID", "RoleText");
            return View();
        }

        // POST: CandidateMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CandidateID,Name,EmailID,ContactNo,RoleID,LocationID,DesiredCity,DesignationID,KeySkills,CompanyID,CurrentCTC,AspectedCTC,TotalExperience,CV,CandidateStatusID,Remarks,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active")] M_CandidateMasters m_CandidateMasters)
        {
            if (ModelState.IsValid)
            {
                db.M_CandidateMasters.Add(m_CandidateMasters);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CandidateStatusID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", m_CandidateMasters.CandidateStatusID);
            ViewBag.CompanyID = new SelectList(db.M_CompanyMasters, "CompanyID", "CompanyName", m_CandidateMasters.CompanyID);
            ViewBag.LocationID = new SelectList(db.M_LocationMasters, "LocationID", "State", m_CandidateMasters.LocationID);
            ViewBag.RoleID = new SelectList(db.M_RoleMaster, "RoleID", "RoleText", m_CandidateMasters.RoleID);
            return View(m_CandidateMasters);
        }

        // GET: CandidateMasters/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_CandidateMasters m_CandidateMasters = await db.M_CandidateMasters.FindAsync(id);
            if (m_CandidateMasters == null)
            {
                return HttpNotFound();
            }
            ViewBag.CandidateStatusID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", m_CandidateMasters.CandidateStatusID);
            ViewBag.CompanyID = new SelectList(db.M_CompanyMasters, "CompanyID", "CompanyName", m_CandidateMasters.CompanyID);
            ViewBag.LocationID = new SelectList(db.M_LocationMasters, "LocationID", "State", m_CandidateMasters.LocationID);
            ViewBag.RoleID = new SelectList(db.M_RoleMaster, "RoleID", "RoleText", m_CandidateMasters.RoleID);
            return View(m_CandidateMasters);
        }

        // POST: CandidateMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CandidateID,Name,EmailID,ContactNo,RoleID,LocationID,DesiredCity,DesignationID,KeySkills,CompanyID,CurrentCTC,AspectedCTC,TotalExperience,CV,CandidateStatusID,Remarks,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active")] M_CandidateMasters m_CandidateMasters)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_CandidateMasters).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CandidateStatusID = new SelectList(db.M_CommonMasterTable, "ID", "FieldValue", m_CandidateMasters.CandidateStatusID);
            ViewBag.CompanyID = new SelectList(db.M_CompanyMasters, "CompanyID", "CompanyName", m_CandidateMasters.CompanyID);
            ViewBag.LocationID = new SelectList(db.M_LocationMasters, "LocationID", "State", m_CandidateMasters.LocationID);
            ViewBag.RoleID = new SelectList(db.M_RoleMaster, "RoleID", "RoleText", m_CandidateMasters.RoleID);
            return View(m_CandidateMasters);
        }

        // GET: CandidateMasters/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_CandidateMasters m_CandidateMasters = await db.M_CandidateMasters.FindAsync(id);
            if (m_CandidateMasters == null)
            {
                return HttpNotFound();
            }
            return View(m_CandidateMasters);
        }

        // POST: CandidateMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            M_CandidateMasters m_CandidateMasters = await db.M_CandidateMasters.FindAsync(id);
            db.M_CandidateMasters.Remove(m_CandidateMasters);
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
