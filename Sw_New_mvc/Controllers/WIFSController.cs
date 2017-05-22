using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sw_New_mvc.Models;

namespace Sw_New_mvc.Controllers
{
    public class WIFSController : Controller
    {
        private Con db = new Con();


        static int userid = 0;
        public ActionResult Index(string ReportingMonth, string ReportingYear, string search, string save, string cancel, int pagenumber = 1)
        {
            Session["userId"] = 1;
            userid = Convert.ToInt16(Session["userId"]);
            try
            {
                if (!string.IsNullOrEmpty(save))
                {
                    ViewBag.Page = 0;
                    List<int> Years = new List<int>();
                    GetData obj = new GetData();
                    int yr2 = obj.getyear();
                    for (int i = yr2; i > 2009; i--)
                    {
                        Years.Add(i);
                    }
                    ViewBag.Years = Years;
                    ViewBag.Page = 1;
                    getrec(pagenumber);

                    int mnth = Convert.ToInt32(ReportingMonth);
                    int yrs = Convert.ToInt32(ReportingYear);
                    return View(db.WIFS.Where(x => x.cby == userid && x.ReportingMonth == mnth && x.Reportingyear == yrs).ToList().OrderByDescending(x => x.Reportingdate));

                }
                else
                {
                    ViewBag.Page = 0;
                    List<int> Years = new List<int>();
                    GetData obj = new GetData();
                    icdseccePara par = new icdseccePara();
                    par.cby = Convert.ToInt16(Session["userId"]);
                    obj.getBlock(par);
                    int mnth = 1;
                    int yrs = 2010;
                    if (par.otp1 == 1)
                    {
                        mnth = Convert.ToInt32(par.ReportingMonth);
                        ViewBag.mn = mnth;
                        yrs = Convert.ToInt32(par.ReportingYear);
                        ViewBag.yr = yrs;
                    }
                    int yr2 = obj.getyear();
                    for (int i = yr2; i > 2009; i--)
                    {
                        Years.Add(i);
                    }
                    ViewBag.Years = Years;

                    ViewBag.Page = 1;
                    getrec(pagenumber);

                    return View(db.WIFS.Where(x => x.cby == userid && x.ReportingMonth == mnth && x.Reportingyear == yrs).ToList().OrderByDescending(x => x.Reportingdate));

                }
            }
            catch
            {

                ViewBag.Page = 0;
                List<int> Years = new List<int>();
                GetData obj = new GetData();
                icdseccePara par = new icdseccePara();
                par.cby = Convert.ToInt16(Session["userId"]);
                obj.getBlock(par);
                int mnth = 1;
                int yrs = 2010;
                if (par.otp1 == 1)
                {
                    mnth = Convert.ToInt32(par.ReportingMonth);
                    ViewBag.mn = mnth;
                    yrs = Convert.ToInt32(par.ReportingYear);
                    ViewBag.yr = yrs;
                }
                int yr2 = obj.getyear();
                for (int i = yr2; i > 2009; i--)
                {
                    Years.Add(i);
                }
                ViewBag.Years = Years;

                ViewBag.Page = 1;
                getrec(pagenumber);
                return View(db.WIFS.Where(x => x.cby == userid && x.ReportingMonth == mnth && x.Reportingyear == yrs).ToList().OrderByDescending(x => x.Reportingdate));

            }
        }
        int firstcount = 0;
        int endcount = 0;
        public void getrec(int pagenumber = 1)
        {
            int pageno = 0;
            int pagecount = 0;
            int pageCount = 0;
            int totalpage = 0;
            double dblPageCount = 0;
            pageno = pagenumber - 1;
            firstcount = pageno * 20;
            endcount = firstcount + 20;
            pagecount = db.EcceDays.Count();
            dblPageCount = (double)((decimal)pagecount / Convert.ToDecimal(20));
            pageCount = (int)Math.Ceiling(dblPageCount);
            totalpage = pageCount;
            ViewBag.Pagetotal = pageCount;
            if (pageCount > 0)
            {
                ViewBag.pagecount = pageCount;

                int d = 0;
                d = pagenumber;
                if (d < 9)
                {
                    d = 1;
                }
                else
                {
                    d = pagenumber - 4;
                }
                ViewBag.startpont = d;
                ViewBag.endpoinnt = d + 9;

            }
            ViewBag.pgno = pagenumber;
        }
        
        public ActionResult Details(int id = 0)
        {
            WIFS wifs = db.WIFS.Single(w => w.id == id);
            if (wifs == null)
            {
                return HttpNotFound();
            }
            return View(wifs);
        }

        
        static int projid = 0;
        static int distid = 0;
        public ActionResult Create()
        {
            wifspara par = new wifspara();
            ViewBag.ErrorMsg = "";
            Session["ProjectId"] = 1;
            Session["distid"] = distid;
            ViewBag.District = "Kamrup (M)";
            ViewBag.Project = "Nalchha";
            par.DistId = Convert.ToInt32(Session["distid"]);
            par.ProjID = Convert.ToInt32(Session["ProjectId"]);
            projid = Convert.ToInt32(Session["ProjectId"]);
            GetData obj = new GetData();
            List<int> Years = new List<int>();
            int yr2 = obj.getyear();
            for (int i = yr2; i > 2009; i--)
            {
                Years.Add(i);
            }
            ViewBag.Year = Years;
            par.awcs = new SelectList(db.AWCMsts.Where(c => c.Proj_ID == projid).OrderBy(x => x.Ang_Name), "Ang_ID", "Ang_Name");
            return View(par);
        }
        [HttpPost]
        public ActionResult Create(wifspara par)
        {
            ViewBag.ErrorMsg = "";
            Session["userId"] = 1;
            userid = Convert.ToInt16(Session["userId"]);
            if (ModelState.IsValid)
            {
                par.op = 1;
                par.DistId = 1; 
                par.status = 1;
                par.appstatus = 0;
                par.ReportingDate = Convert.ToDateTime(Convert.ToString(par.ReportingYear)+"-"+Convert.ToString(par.ReportingMonth)+"-01");
                par.cby = Convert.ToInt16(Session["userId"]);
                par.con = DateTime.Now;
                GetData obj = new GetData();
                int res = obj.insertDatawifs(par);
                if (par.otp1 == 1)
                {
                    Session["alt"] = "1";
                    Session["succ"] = "Records has been added successfully to the database.";
                    return RedirectToAction("Create");
                }
                else
                {
                    Session["alt"] = "2";
                    Session["succ"] = "Duplicate Record found on same reporting month and year for this AWC!";
                    List<int> Years = new List<int>();
                    int yr2 = obj.getyear();
                    for (int i = yr2; i > 2009; i--)
                    {
                        Years.Add(i);
                    }
                    ViewBag.Year = Years;
                    ViewBag.District = "Kamrup (M)";
                    ViewBag.Project = "Nalchha";
                    par.DistId = Convert.ToInt32(Session["distid"]);
                    par.ProjID = Convert.ToInt32(Session["ProjectId"]);
                    projid = Convert.ToInt32(Session["ProjectId"]);
                    par.awcs = new SelectList("", "---Select---");

                    return View(par);
                }
            }
            return RedirectToAction("Create","WIFS");
        }
        public ActionResult GetCodes(int angid)
        {
            var result = db.AWCMsts.Single(c => c.Ang_ID == angid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /WIFS/Edit/5

        public ActionResult Edit(int id = 0)
        {
             Session["userId"] = 1;
            userid = Convert.ToInt16(Session["userId"]);
            List<int> Years = new List<int>();
            GetData obj = new GetData();
            int yr2 = obj.getyear();
            for (int i = yr2; i > 2009; i--)
            {
                Years.Add(i);
            }
            ViewBag.Years = Years;
            ViewBag.ErrorMsg = "";
            wifspara par = new wifspara();


            WIFS wifs = db.WIFS.Single(d => d.id == id && d.status == 1 && (d.approve_status == 0 || d.approve_status == 3) && d.cby == userid);
            par.ID = id;
            ViewBag.District = wifs.DistrictMst.Dis_Name;
            ViewBag.Project = wifs.ProjectMst.Proj_Name;

            par.DistId = Convert.ToInt16(wifs.dist_id);
            par.ReportingMonth = Convert.ToInt16(wifs.ReportingMonth);
            par.ReportingYear = Convert.ToString(wifs.Reportingyear);
            par.ProjID = Convert.ToInt16(wifs.proj_id);
            par.a_par = Convert.ToInt16(wifs.a);
            par.b_par = Convert.ToInt16(wifs.b);
            par.c_par = Convert.ToInt16(wifs.c);
            par.d_par = Convert.ToInt16(wifs.d);
            par.e_par = Convert.ToInt16(wifs.e);
            par.f_par = Convert.ToInt16(wifs.f);
            par.g_par = Convert.ToInt16(wifs.g);
            par.h_par = Convert.ToInt16(wifs.h);
            par.i_par = Convert.ToInt16(wifs.i);
            par.j_par = Convert.ToInt16(wifs.j);
            par.k_par = Convert.ToInt16(wifs.k);
            par.l_par = Convert.ToInt16(wifs.l);
            par.m_par = Convert.ToInt16(wifs.m);
            par.n1_par = Convert.ToInt16(wifs.n_opening);
            par.n2_par = Convert.ToInt16(wifs.n_received);
            par.n3_par = Convert.ToInt16(wifs.n_utilized);
            par.n4_par = Convert.ToInt16(wifs.n_balance);
            par.o1_par = Convert.ToInt16(wifs.o_opening);
            par.o2_par = Convert.ToInt16(wifs.o_received);
            par.o3_par = Convert.ToInt16(wifs.o_utilized);
            par.o4_par = Convert.ToInt16(wifs.o_balance);
            par.appstatus = Convert.ToInt16(wifs.approve_status);
            par.Remarks = wifs.remarks;


            par.AWCid = Convert.ToInt16(wifs.AWC_id);
            ViewBag.Project = "Nalchha";
            par.awcs = new SelectList(db.AWCMsts.Where(c => c.Proj_ID == wifs.proj_id).OrderBy(x => x.Ang_Name), "Ang_ID", "Ang_Name", par.AWCid);
            return View(par);
        }

        //
        // POST: /WIFS/Edit/5


        [HttpPost]
        public ActionResult Edit(wifspara par)
        {
            Session["userId"] = 1;
            userid = Convert.ToInt16(Session["userId"]);
            GetData obj = new GetData();
            if (ModelState.IsValid)
            {
                par.op = 2;
                par.DistId = 1;
                par.status = 1;
                par.cby = Convert.ToInt16(Session["userId"]);
                par.ReportingDate = Convert.ToDateTime(Convert.ToString(par.ReportingYear) + "-" + Convert.ToString(par.ReportingMonth) + "-01");
                par.con = DateTime.Now;
                int res = obj.insertDatawifs(par);
                if (par.otp1 == 1)
                {
                    Session["alt"] = "1";
                    Session["succ"] = "Records has been added successfully to the database.";
                    return RedirectToAction("Edit", "WIFS", new { id = par.ID });
                }
                else
                {
                    Session["alt"] = "2";
                    Session["succ"] = "Duplicate Record found on same reporting month and year for this AWC!";
                    Session["userId"] = 1;
                    return RedirectToAction("Edit", "WIFS", new { id = par.ID });
                }
            }

            return View();
        }
        //
        // GET: /WIFS/Delete/5

        public ActionResult Delete(int id = 0)
        {
            WIFS wifs = db.WIFS.Single(w => w.id == id);
            if (wifs == null)
            {
                return HttpNotFound();
            }
            return View(wifs);
        }

        //
        // POST: /WIFS/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            WIFS wifs = db.WIFS.Single(w => w.id == id);
            db.WIFS.DeleteObject(wifs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}