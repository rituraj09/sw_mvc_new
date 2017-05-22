using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sw_New_mvc.Models
{
    public class wifspara
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int ReportingMonth { get; set; }

        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public string ReportingYear { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReportingDate { get; set; }

        public int DistId { get; set; }
        public int ProjID { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int AWCid { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int a_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int b_par { get; set; }

        public decimal c_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int d_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int e_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int f_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public decimal g_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int h_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int i_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int j_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int k_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int l_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int m_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int n1_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int n2_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int n3_par { get; set; }
        public int n4_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int o1_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int o2_par { get; set; }
        [Required(ErrorMessage = "<i class='fa fa-exclamation-circle alert-danger' data-dismiss='alert' ></i>")]
        public int o3_par { get; set; }
        public int o4_par { get; set; }
        public int status { get; set; }
        public int cby { get; set; }
        public DateTime con { get; set; }
        public int appstatus { get; set; }
        public string Remarks { get; set; }
        public int op { get; set; }
        public int otp1 { get; set; }
        public int otp2 { get; set; }
        public System.Web.Mvc.SelectList awcs { get; set; }
    }
}