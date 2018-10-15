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
using HRMWeb.App_Code;

namespace HRMWeb.Controllers
{
    public class T_EmployeeDocumentController : Controller
    {
        private HRM_DBEntities db = new HRM_DBEntities();

        // GET: T_EmployeeDocument
        public async Task<ActionResult> Index()
        {
            if (Session["LoginUserID"].ToString() == Resources.HRMResources.AdminUser)
            {
                var t_EmployeeDocument = db.T_EmployeeDocument.Include(t => t.M_EmployeeMasters);
                return View(await t_EmployeeDocument.ToListAsync());
            }
            else
            {
                string EmployeeCode = Session["LoginUserID"].ToString();
                var t_EmployeeDocument = db.T_EmployeeDocument.Where(x => x.EmployeeID == EmployeeCode).OrderByDescending(x => x.CreatedDate);
                return View(await t_EmployeeDocument.ToListAsync());
            }            
        }

        // GET: T_EmployeeDocument/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EmployeeDocument t_EmployeeDocument = await db.T_EmployeeDocument.FindAsync(id);
            if (t_EmployeeDocument == null)
            {
                return HttpNotFound();
            }
            return View(t_EmployeeDocument);
        }

        // GET: T_EmployeeDocument/Create
        public ActionResult Create()
        {
            if (Session["LoginUserID"] != null && Session["LoginUserID"].ToString() != Resources.HRMResources.AdminUser)
            {
                string EmployeeCode = Session["LoginUserID"].ToString();
                ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters.Where(x => x.EmployeeID == EmployeeCode), "EmployeeID", "EmployeeName");
                return View();
            }
            else
            {
                ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName");
                return View();
            }
        }

        // POST: T_EmployeeDocument/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeID,ReferenceDoc,FileName,FileDescription")] T_EmployeeDocument t_EmployeeDocument)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Request.Files["FileName"].FileName))
                {
                    string FolderPath = Server.MapPath(Resources.HRMResources.EmployeeDocumentPath);// + "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek;
                    string FullPathWithFileName = FolderPath + "\\" + Request.Files["FileName"].FileName;
                    string FolderPathForImage = Request.Files["FileName"].FileName;  //"\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\" + Request.Files["StdProfilePicPath"].FileName;
                    if (CommonFunction.IsFolderExist(FolderPath))
                    {
                        Request.Files["FileName"].SaveAs(FullPathWithFileName);
                        t_EmployeeDocument.FileName = FolderPathForImage;
                    }
                }
                t_EmployeeDocument.EmployeeID= Session["LoginUserID"].ToString();
                t_EmployeeDocument.CreatedBy = Session["LoginUserID"].ToString();
                t_EmployeeDocument.CreatedDate = DateTime.Now;
                t_EmployeeDocument.ModifiedBy = Session["LoginUserID"].ToString();
                t_EmployeeDocument.ModifiedDate = DateTime.Now;
                t_EmployeeDocument.Active = true;

                db.T_EmployeeDocument.Add(t_EmployeeDocument);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", t_EmployeeDocument.EmployeeID);
            return View(t_EmployeeDocument);
        }

        // GET: T_EmployeeDocument/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EmployeeDocument t_EmployeeDocument = await db.T_EmployeeDocument.FindAsync(id);
            if (t_EmployeeDocument == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", t_EmployeeDocument.EmployeeID);
            return View(t_EmployeeDocument);
        }

        // POST: T_EmployeeDocument/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DocumentID,EmployeeID,ReferenceDoc,FileName,FileDescription,CreatedBy,CreatedDate,Active")] T_EmployeeDocument t_EmployeeDocument)
        {
            if (ModelState.IsValid)
            {
                t_EmployeeDocument.ModifiedBy = Session["LoginUserID"].ToString();
                t_EmployeeDocument.ModifiedDate = DateTime.Now;

                db.Entry(t_EmployeeDocument).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.M_EmployeeMasters, "EmployeeID", "EmployeeName", t_EmployeeDocument.EmployeeID);
            return View(t_EmployeeDocument);
        }

        // GET: T_EmployeeDocument/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_EmployeeDocument t_EmployeeDocument = await db.T_EmployeeDocument.FindAsync(id);
            if (t_EmployeeDocument == null)
            {
                return HttpNotFound();
            }
            return View(t_EmployeeDocument);
        }

        // POST: T_EmployeeDocument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            T_EmployeeDocument t_EmployeeDocument = await db.T_EmployeeDocument.FindAsync(id);
            db.T_EmployeeDocument.Remove(t_EmployeeDocument);
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
