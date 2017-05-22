using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Sw_New_mvc.Models
{
    public class GetData
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConContext"].ToString());

        public int getyear()
        {
            int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            return year;
        }
        public int insertData(icdseccePara par)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "icdbsas_sp_InsertECCE";
            cmd.Parameters.Add("@op", SqlDbType.VarChar, 300).Value = par.op;
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 300).Value = par.ID;
            cmd.Parameters.Add("@repmonth", SqlDbType.VarChar, 300).Value = par.ReportingMonth;
            cmd.Parameters.Add("@repyr", SqlDbType.VarChar, 300).Value = par.ReportingYear;
            cmd.Parameters.Add("@repdate", SqlDbType.DateTime).Value = par.ReportingDate;
            cmd.Parameters.Add("@divid", SqlDbType.VarChar, 300).Value = par.DivId;
            cmd.Parameters.Add("@distid", SqlDbType.VarChar, 300).Value = par.DistId;
            cmd.Parameters.Add("@projid", SqlDbType.VarChar, 300).Value = par.ProjID;
            cmd.Parameters.Add("@awcid", SqlDbType.VarChar, 300).Value = par.AWCid;
            cmd.Parameters.Add("@awccode", SqlDbType.VarChar, 300).Value = par.AWCcode;
            cmd.Parameters.Add("@dateofecc", SqlDbType.DateTime).Value = dateconvert(Convert.ToString(par.ECCEDay)); 
            cmd.Parameters.Add("@communityinformed", SqlDbType.VarChar, 300).Value = par.iformedcomunity;
            cmd.Parameters.Add("@awwpresent", SqlDbType.VarChar, 300).Value = par.AWW_present;
            cmd.Parameters.Add("@awhpresent", SqlDbType.VarChar, 300).Value = par.AWH_present;
            cmd.Parameters.Add("@totalawc", SqlDbType.VarChar, 300).Value = par.total_no_AWC;
            cmd.Parameters.Add("@totaloutside", SqlDbType.VarChar, 300).Value = par.total_no_outside;
            cmd.Parameters.Add("@totalinside", SqlDbType.VarChar, 300).Value = par.total_no_inside;
            cmd.Parameters.Add("@totchild", SqlDbType.VarChar, 300).Value = par.Participant_children;
            cmd.Parameters.Add("@totparents", SqlDbType.VarChar, 300).Value = par.Participant_parents;
            cmd.Parameters.Add("@totpri", SqlDbType.VarChar, 300).Value = par.Participant_PRI;
            cmd.Parameters.Add("@totawc", SqlDbType.VarChar, 300).Value = par.Participant_AWCMC;
            cmd.Parameters.Add("@tothealth", SqlDbType.VarChar, 300).Value = par.Participant_Health;
            cmd.Parameters.Add("@totsnp", SqlDbType.VarChar, 300).Value = par.total_SNP;
            cmd.Parameters.Add("@materialdonated", SqlDbType.VarChar, 300).Value = par.Materials_donated;
            cmd.Parameters.Add("@theme", SqlDbType.VarChar, 700).Value = par.Theme_of_ECCE;
            cmd.Parameters.Add("@explainchil", SqlDbType.VarChar, 300).Value = par.ExplainChild;
            cmd.Parameters.Add("@childrisk", SqlDbType.VarChar, 300).Value = par.child_risk;
            cmd.Parameters.Add("@profile", SqlDbType.VarChar, 300).Value = par.maintain_profile;
            cmd.Parameters.Add("@dayrec", SqlDbType.VarChar, 300).Value = par.maintain_dayrecord;
            cmd.Parameters.Add("@status", SqlDbType.VarChar, 300).Value = par.status;
            cmd.Parameters.Add("@appstatus ", SqlDbType.VarChar, 300).Value = par.approve_status;
            cmd.Parameters.Add("@remarks", SqlDbType.VarChar, 700).Value = par.remarks;
            cmd.Parameters.Add("@cby", SqlDbType.VarChar, 300).Value = par.cby;
            cmd.Parameters.Add("@sp_msg1", SqlDbType.VarChar, 30);
            cmd.Parameters.Add("@sp_msg2", SqlDbType.VarChar, 30);
            cmd.Parameters["@sp_msg1"].Direction = ParameterDirection.Output;
            cmd.Parameters["@sp_msg2"].Direction = ParameterDirection.Output;
            cmd.Connection = con;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                par.otp1 = Convert.ToInt32(cmd.Parameters["@sp_msg1"].Value.ToString());
                par.otp2 = Convert.ToInt32(cmd.Parameters["@sp_msg2"].Value.ToString());
                result = Convert.ToInt32(cmd.Parameters["@sp_msg1"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public string dateconvert(string cdate)
        {
            string date = "";
            string mnt = cdate.ToString().Substring(3, 2);
            string dat = cdate.ToString().Substring(0, 2);
            string yr = cdate.ToString().Substring(6, 4);
            //   date = mnt + "/" + dat + "/" + yr;
            date = yr + "-" + mnt + "-" + dat;
            return date;
        }
        public void getBlock(icdseccePara par)
        {
            par.otp1 = 0;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string s = "select * from view_icdsas_eccedays where status=1 and cby="+par.cby+" order by dateofecc desc";
            SqlCommand cmd = new SqlCommand(s, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                par.ReportingMonth = Convert.ToInt32(dt.Rows[0]["ReportingMonth"].ToString());
                par.ReportingYear = Convert.ToInt32(dt.Rows[0]["ReportingYear"].ToString());
                par.otp1 = 1;
            }
        }
        public int ApproveRec(icdseccePara par)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "icdbsas_sp_approve";
            cmd.Parameters.Add("@op", SqlDbType.VarChar, 300).Value = par.op;
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 300).Value = par.ID;
            cmd.Parameters.Add("@repdate", SqlDbType.DateTime).Value = par.ReportingDate;         
            cmd.Parameters.Add("@appstatus ", SqlDbType.VarChar, 300).Value = par.approve_status;
            cmd.Parameters.Add("@remarks", SqlDbType.VarChar, 700).Value = par.remarks;
            cmd.Parameters.Add("@cby", SqlDbType.VarChar, 300).Value = par.cby;
            cmd.Parameters.Add("@sp_msg1", SqlDbType.VarChar, 30);
            cmd.Parameters.Add("@sp_msg2", SqlDbType.VarChar, 30);
            cmd.Parameters["@sp_msg1"].Direction = ParameterDirection.Output;
            cmd.Parameters["@sp_msg2"].Direction = ParameterDirection.Output;
            cmd.Connection = con;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                par.otp1 = Convert.ToInt32(cmd.Parameters["@sp_msg1"].Value.ToString());
                par.otp2 = Convert.ToInt32(cmd.Parameters["@sp_msg2"].Value.ToString());
                result = Convert.ToInt32(cmd.Parameters["@sp_msg1"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public string getmonth(int i)
        {
            string mnth = "";

            if (i == 1)
            {
                mnth = "Jan";
            }
            else if (i == 2)
            {
                mnth = "Feb";
            }
            else if (i == 3)
            {
                mnth = "Mar";
            }
            else if (i == 4)
            {
                mnth = "Apr";
            }
            else if (i == 5)
            {
                mnth = "May";
            }
            else if (i == 6)
            {
                mnth = "Jun";
            }
            else if (i == 7)
            {
                mnth = "Jul";
            }
            else if (i == 8)
            {
                mnth = "Aug";
            }
            else if (i == 9)
            {
                mnth = "Sep";
            }
            else if (i == 10)
            {
                mnth = "Oct";
            }
            else if (i == 1)
            {
                mnth = "Nov";
            }
            else if (i == 12)
            {
                mnth = "Dec";
            }
            return mnth;
        }

        public int insertDatawifs(wifspara par)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "icdbsas_sp_InsertWIFS";
            cmd.Parameters.Add("@op", SqlDbType.VarChar, 300).Value = par.op;
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 300).Value = par.ID;
            cmd.Parameters.Add("@repmonth", SqlDbType.VarChar, 300).Value = par.ReportingMonth;
            cmd.Parameters.Add("@repyr", SqlDbType.VarChar, 300).Value = par.ReportingYear;
            cmd.Parameters.Add("@repdate", SqlDbType.DateTime).Value = par.ReportingDate;
            cmd.Parameters.Add("@distid", SqlDbType.VarChar, 300).Value = par.DistId;
            cmd.Parameters.Add("@projid", SqlDbType.VarChar, 300).Value = par.ProjID;
            cmd.Parameters.Add("@awcid", SqlDbType.VarChar, 300).Value = par.AWCid;
            cmd.Parameters.Add("@a", SqlDbType.VarChar, 300).Value = par.a_par;
            cmd.Parameters.Add("@b", SqlDbType.VarChar, 300).Value = par.b_par;
            cmd.Parameters.Add("@c", SqlDbType.VarChar, 300).Value = par.c_par;
            cmd.Parameters.Add("@d", SqlDbType.VarChar, 300).Value = par.d_par;
            cmd.Parameters.Add("@e", SqlDbType.VarChar, 300).Value = par.e_par;
            cmd.Parameters.Add("@f", SqlDbType.VarChar, 300).Value = par.f_par;
            cmd.Parameters.Add("@g", SqlDbType.VarChar, 300).Value = par.g_par;
            cmd.Parameters.Add("@h", SqlDbType.VarChar, 300).Value = par.h_par;
            cmd.Parameters.Add("@i", SqlDbType.VarChar, 300).Value = par.i_par;
            cmd.Parameters.Add("@j", SqlDbType.VarChar, 300).Value = par.j_par;
            cmd.Parameters.Add("@k", SqlDbType.VarChar, 300).Value = par.k_par;
            cmd.Parameters.Add("@l", SqlDbType.VarChar, 300).Value = par.l_par;
            cmd.Parameters.Add("@m", SqlDbType.VarChar, 300).Value = par.m_par;
            cmd.Parameters.Add("@n1", SqlDbType.VarChar, 300).Value = par.n1_par;
            cmd.Parameters.Add("@n2", SqlDbType.VarChar, 300).Value = par.n2_par;
            cmd.Parameters.Add("@n3", SqlDbType.VarChar, 700).Value = par.n3_par;
            cmd.Parameters.Add("@n4", SqlDbType.VarChar, 300).Value = par.n4_par;
            cmd.Parameters.Add("@o1", SqlDbType.VarChar, 300).Value = par.o1_par;
            cmd.Parameters.Add("@o2", SqlDbType.VarChar, 300).Value = par.o2_par;
            cmd.Parameters.Add("@o3", SqlDbType.VarChar, 300).Value = par.o3_par;
            cmd.Parameters.Add("@o4", SqlDbType.VarChar, 300).Value = par.o4_par;
            cmd.Parameters.Add("@status", SqlDbType.VarChar, 300).Value = par.status;
            cmd.Parameters.Add("@appstatus ", SqlDbType.VarChar, 300).Value = par.appstatus;
            cmd.Parameters.Add("@remarks", SqlDbType.VarChar, 700).Value = par.Remarks;
            cmd.Parameters.Add("@cby", SqlDbType.VarChar, 300).Value = par.cby;
            cmd.Parameters.Add("@con", SqlDbType.DateTime).Value = dateconvert(Convert.ToString(par.con));
            cmd.Parameters.Add("@sp_msg1", SqlDbType.VarChar, 30);
            cmd.Parameters.Add("@sp_msg2", SqlDbType.VarChar, 30);
            cmd.Parameters["@sp_msg1"].Direction = ParameterDirection.Output;
            cmd.Parameters["@sp_msg2"].Direction = ParameterDirection.Output;
            cmd.Connection = con;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                par.otp1 = Convert.ToInt32(cmd.Parameters["@sp_msg1"].Value.ToString());
                par.otp2 = Convert.ToInt32(cmd.Parameters["@sp_msg2"].Value.ToString());
                result = Convert.ToInt32(cmd.Parameters["@sp_msg1"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return result;
        }
    }
}