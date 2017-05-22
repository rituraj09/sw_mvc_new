using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sw_New_mvc.Models;
using System.Web.Script.Serialization;

namespace Sw_New_mvc.Controllers
{
    public class ECCEController : Controller
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

                    return View(db.EcceDays.Where(x => x.cby == userid && x.ROW_NUM > firstcount && x.ROW_NUM <= endcount && x.ReportingMonth == mnth && x.Reportingyear == yrs).ToList().OrderByDescending(x => x.Reportingdate));

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

                    return View(db.EcceDays.Where(x => x.cby == userid && x.ReportingMonth == mnth && x.Reportingyear == yrs).ToList().OrderByDescending(x => x.Reportingdate));

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
                return View(db.EcceDays.Where(x => x.cby == userid && x.ReportingMonth == mnth && x.Reportingyear == yrs).ToList().OrderByDescending(x => x.Reportingdate));

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
        public ActionResult Create()
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
            int distid = 1;
            ViewBag.Division = "Kamrup";
            ViewBag.District = "Kamrup (M)";
            ViewBag.ErrorMsg = "";
            icdseccePara par = new icdseccePara();
            par.divisions = new SelectList(db.DivisionMsts.ToList(), "Div_ID", "Div_Name");
            par.districts = new SelectList(db.DistrictMsts.ToList(), "Dis_ID", "Dis_Name");
            //par.projects = new SelectList(db.ProjectMsts.Where(c => c.Proj_DisID == distid), "Proj_ID", "Proj_Name", "1");
            ViewBag.Project = "Nalchha";
            par.ProjID = 1;
            par.awcs = new SelectList(db.AWCMsts.Where(c => c.Proj_ID == 1).OrderBy(x => x.Ang_Name), "Ang_ID", "Ang_Name", par.AWCid);
            return View(par);
        }
        [HttpPost]
        public ActionResult Create(icdseccePara par)
        {
            Session["userId"] = 1;
            userid = Convert.ToInt16(Session["userId"]);
            if (ModelState.IsValid)
            {
                par.op = 1;
                par.DistId = 1;
                par.DivId = 1;
                par.status = 1;
                par.approve_status = 0;
                par.cby = Convert.ToInt16(Session["userId"]);
                par.ReportingDate = DateTime.Now;
                GetData obj = new GetData();
                int res = obj.insertData(par);
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
                    ViewBag.Years = Years;
                    int distid = 1;
                    @ViewBag.Division = "Kamrup";
                    @ViewBag.District = "Kamrup (M)";
                    ViewBag.ErrorMsg = "";
                    par.divisions = new SelectList(db.DivisionMsts.ToList(), "Div_ID", "Div_Name");
                    par.districts = new SelectList(db.DistrictMsts.ToList(), "Dis_ID", "Dis_Name");
                  //  par.projects = new SelectList(db.ProjectMsts.Where(c => c.Proj_DisID == distid), "Proj_ID", "Proj_Name","12");
                    ViewBag.Project = "Nalchha";
                    par.ProjID = 1;
                    par.awcs = new SelectList(db.AWCMsts.Where(c => c.Proj_ID == 1).OrderBy(x => x.Ang_Name), "Ang_ID", "Ang_Name", par.AWCid);

                    return View(par);
                }
            }

            return View();
        }
        public ActionResult ProjectData(int projectid)
        {
            return Json(new SelectList(db.AWCMsts.Where(c => c.Proj_ID == projectid).OrderBy(x => x.Ang_Name), "Ang_Id", "Ang_Name"), JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetCodes(int angid)
        {
            var result = db.AWCMsts.Single(c => c.Ang_ID == angid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Details(string Isapprove, string finalsubmit, string save, string Remarks, int id = 0)
        {
         
            if (Isapprove == "false")
            {
                ViewBag.alt = "9";
                Session["succ"] = "Please confirm by clicked in checkbox!";
            }
            else
            {
                if (finalsubmit == "")
                {
                    ViewBag.alt = "9";
                    Session["succ"] = "Please select, what do you want to do?";
                }
                else
                {
                    Session["userId"] = 1;
                    userid = Convert.ToInt16(Session["userId"]);
                    try
                    {
                        bool s = Convert.ToBoolean(Isapprove);
                        if (s == true)
                        {
                            icdseccePara par = new icdseccePara();
                            par.op = 1;
                            par.ID = id;
                            par.status = 1;
                            int status = 0;
                            string app = "";
                            if (finalsubmit == "1")
                            {
                                status = 1;
                                app = "Approved";
                            }
                            else if (finalsubmit == "2")
                            {
                                status = 3;
                                app = "suggested to alter";
                            }
                            else if (finalsubmit == "3")
                            {
                                status = 4;
                                app = "Rejected";
                            }
                            par.approve_status = Convert.ToInt16(status);
                            par.remarks = Remarks;
                            par.cby = Convert.ToInt16(Session["userId"]);
                            par.ReportingDate = DateTime.Now;
                            GetData obj = new GetData();
                            int res = obj.ApproveRec(par);

                            if (par.otp1 == 1)
                            {
                                Session["alt"] = "1";
                                Session["succ"] = "Records has been successfully " + app;
                            }
                        }
                    }
                    catch { }
                }

            }
            ViewBag.admintype = "1";
            EcceDays eccedays = db.EcceDays.Single(i => i.id == id);

            if (eccedays == null)
            {
                return HttpNotFound();
            }
            return View(eccedays);


        }


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
            icdseccePara par = new icdseccePara();


            EcceDays eccd = db.EcceDays.Single(d => d.id == id && d.status == 1 && (d.approve_status == 0 || d.approve_status == 3) && d.cby == userid);
            par.ID = id;
            ViewBag.Division = eccd.Div_Name;
            ViewBag.District = eccd.Dis_Name;

            par.DistId = Convert.ToInt16(eccd.Dist_id);
            par.DivId = Convert.ToInt16(eccd.Div_id);
            par.ReportingMonth = Convert.ToInt16(eccd.ReportingMonth);
            par.ReportingYear = Convert.ToInt16(eccd.Reportingyear);
            par.AWCcode = eccd.Awc_code;
            ViewBag.awccode = eccd.Awc_code;
            par.ECCEDay = Convert.ToString(eccd.dateofecc.Value.ToString("dd-MM-yyyy"));
            par.iformedcomunity = Convert.ToInt16(eccd.community_was_informed);
            par.AWW_present = Convert.ToInt16(eccd.AWW_present);
            par.AWH_present = Convert.ToInt16(eccd.AWH_present);
            par.total_no_AWC = Convert.ToInt16(eccd.total_no_AWC);
            par.total_no_outside = Convert.ToInt16(eccd.total_no_outside);
            par.total_no_inside = Convert.ToInt16(eccd.total_no_inside);
            par.Participant_children = Convert.ToInt16(eccd.Participant_children);
            par.Participant_parents = Convert.ToInt16(eccd.Participant_parents);
            par.Participant_PRI = Convert.ToInt16(eccd.Participant_PRI);
            par.Participant_Health = Convert.ToInt16(eccd.Participant_Health);
            par.Participant_AWCMC = Convert.ToInt16(eccd.Participant_AWCMC);

            par.total_SNP = Convert.ToInt16(eccd.total_SNP);
            par.Materials_donated = Convert.ToInt16(eccd.Materials_donated);
            par.Theme_of_ECCE = eccd.Theme_of_ECCE;
            par.ExplainChild = Convert.ToInt16(eccd.ExplainChild);
            par.child_risk = Convert.ToInt16(eccd.child_risk);
            par.maintain_profile = Convert.ToInt16(eccd.maintain_profile);
            par.maintain_dayrecord = Convert.ToInt16(eccd.maintain_dayrecord);
            par.approve_status = Convert.ToInt16(eccd.approve_status);
            par.remarks = eccd.Remarks;


            par.divisions = new SelectList(db.DivisionMsts.ToList(), "Div_ID", "Div_Name");
            par.districts = new SelectList(db.DistrictMsts.ToList(), "Dis_ID", "Dis_Name");
            par.ProjID = Convert.ToInt16(eccd.proj_id);
            par.AWCid = Convert.ToInt16(eccd.awc_id);
            ViewBag.Project = "Nalchha";
            par.awcs = new SelectList(db.AWCMsts.Where(c => c.Proj_ID == eccd.proj_id).OrderBy(x => x.Ang_Name), "Ang_ID", "Ang_Name", par.AWCid);
            return View(par);
        }
        [HttpPost]
        public ActionResult Edit(icdseccePara par)
        {
            Session["userId"] = 1;
            userid = Convert.ToInt16(Session["userId"]);
            GetData obj = new GetData();
            if (ModelState.IsValid)
            {
                par.op = 2;
                par.DistId = 1;
                par.DivId = 1;
                par.status = 1;
                par.approve_status = 0;
                par.cby = Convert.ToInt16(Session["userId"]);
                par.ReportingDate = DateTime.Now;
                int res = obj.insertData(par);
                if (par.otp1 == 1)
                {
                    Session["alt"] = "1";
                    Session["succ"] = "Records has been added successfully to the database.";
                    return RedirectToAction("Edit", "ECCE", new { id = par.ID });
                }
                else
                {
                    Session["alt"] = "2";
                    Session["succ"] = "Duplicate Record found on same reporting month and year for this AWC!";
                    Session["userId"] = 1;
                    return RedirectToAction("Edit", "ECCE", new { id = par.ID });
                }
            }

            return View();
        }
    }
}