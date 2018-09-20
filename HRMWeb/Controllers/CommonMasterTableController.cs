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
    public class CommonMasterTableController : Controller
    {
        private HRM_DBEntities db = new HRM_DBEntities();

        // GET: CommonMasterTable
        public async Task<ActionResult> Index()
        {
            return View(await db.M_CommonMasterTable.OrderByDescending(x=>x.ID).ToListAsync());
        }

        // GET: CommonMasterTable/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_CommonMasterTable m_CommonMasterTable = await db.M_CommonMasterTable.FindAsync(id);
            if (m_CommonMasterTable == null)
            {
                return HttpNotFound();
            }
            return View(m_CommonMasterTable);
        }

        // GET: CommonMasterTable/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommonMasterTable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,FieldValue,TableName,OrderNo")] M_CommonMasterTable m_CommonMasterTable)
        {
            if (ModelState.IsValid)
            {
                m_CommonMasterTable.CreatedBy = "Admin";
                m_CommonMasterTable.CreatedDate = DateTime.Now;
                m_CommonMasterTable.ModifiedBy = "Admin";
                m_CommonMasterTable.ModifiedDate = DateTime.Now;
                m_CommonMasterTable.Active = true;
                db.M_CommonMasterTable.Add(m_CommonMasterTable);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(m_CommonMasterTable);
        }

        // GET: CommonMasterTable/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_CommonMasterTable m_CommonMasterTable = await db.M_CommonMasterTable.FindAsync(id);
            if (m_CommonMasterTable == null)
            {
                return HttpNotFound();
            }
            return View(m_CommonMasterTable);
        }

        // POST: CommonMasterTable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FieldValue,TableName,OrderNo,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active")] M_CommonMasterTable m_CommonMasterTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_CommonMasterTable).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(m_CommonMasterTable);
        }

        // GET: CommonMasterTable/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_CommonMasterTable m_CommonMasterTable = await db.M_CommonMasterTable.FindAsync(id);
            if (m_CommonMasterTable == null)
            {
                return HttpNotFound();
            }
            return View(m_CommonMasterTable);
        }

        // POST: CommonMasterTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            M_CommonMasterTable m_CommonMasterTable = await db.M_CommonMasterTable.FindAsync(id);
            db.M_CommonMasterTable.Remove(m_CommonMasterTable);
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
