using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sw_New_mvc.Models
{
    public class icdseccePara
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int ReportingMonth { get; set; }

        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int ReportingYear { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReportingDate { get; set; }

        public String startDateFormatted { get { return String.Format("{0:dd/MM/yyyy}", ReportingDate); } }

        public int DivId { get; set; }
        public int DistId { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int ProjID { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int AWCid { get; set; }
        //[Required(ErrorMessage = "<div class='alert alert-danger'><button type='button' data-dismiss='alert' aria-hidden='true' class='close'>×</button><i class='fa fa-times-circle sign'></i><strong>Error!</strong>AWC code is Required!</div>")]
        public string AWCcode { get; set; }


        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public string ECCEDay { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int iformedcomunity { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int AWW_present { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int AWH_present { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int total_no_AWC { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int total_no_outside { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int total_no_inside { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int Participant_children { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int Participant_parents { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int Participant_PRI { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int Participant_AWCMC { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int Participant_Health { get; set; }

        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int total_SNP { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int Materials_donated { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        [StringLength(200, ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert'>Maximum 200 characters allows.</i>")]
        public string Theme_of_ECCE { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int ExplainChild { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int child_risk { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int maintain_profile { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int maintain_dayrecord { get; set; }
        public int status { get; set; }
        public int cby { get; set; }
        public int approve_status { get; set; }
        [StringLength(200, ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert'>Maximum 200 characters allows.</i>")]
        public string remarks { get; set; }
        public int otp1 { get; set; }
        public int otp2 { get; set; }
        public int op { get; set; }
        public System.Web.Mvc.SelectList divisions { get; set; }
        public System.Web.Mvc.SelectList districts { get; set; }
        public System.Web.Mvc.SelectList projects { get; set; }
        public System.Web.Mvc.SelectList awcs { get; set; }
        public System.Web.Mvc.SelectList months { get; set; }
        public System.Web.Mvc.SelectList years { get; set; }
        public System.Web.Mvc.SelectList yesno { get; set; }

    }
}