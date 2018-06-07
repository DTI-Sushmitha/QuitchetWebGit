using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Script.Services;

namespace Quitchet.Web
{
    /// <summary>
    /// Summary description for QuitchetSOAPService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class QuitchetSOAPService : System.Web.Services.WebService
    {
        SqlConnection Connection = new SqlConnection(ConfigurationSettings.AppSettings["SOAPConnectionString"].ToString());

        [ScriptMethod(UseHttpGet = true)]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod] 
        public DataTable SOAPUserAuthentication(string username, string password)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Login";

            dt.Columns.Add("UserId");
            dt.Columns.Add("UserName");
            dt.Columns.Add("DisplayName");
            dt.Columns.Add("RoleId");

            DataRow newRow = dt.NewRow();

            DataSet ds = new DataSet();
            SqlCommand cmdcnt = new SqlCommand("spCommonSelectStmt", Connection);
            cmdcnt.CommandType = CommandType.StoredProcedure;
            cmdcnt.Parameters.Add("@TableName", SqlDbType.NVarChar).Value = "tbmUsers";
            cmdcnt.Parameters.Add("@Condition", SqlDbType.NVarChar).Value = "UserName='" + username + "' and Password='" + password + "'";

            SqlDataAdapter dapcnt = new SqlDataAdapter(cmdcnt);
            DataSet dscnt = new DataSet();
            dapcnt.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    newRow[0] = ds.Tables[0].Rows[0]["UserId"].ToString();
                    newRow[1] = ds.Tables[0].Rows[0]["UserName"].ToString();
                    newRow[2] = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                    newRow[3] = "1";
                    dt.Rows.Add(newRow);
                }
                else
                {
                    newRow[0] = "0";
                    newRow[1] = "0";
                    newRow[2] = "0";
                    newRow[3] = "0";
                    dt.Rows.Add(newRow);
                }
            }

            return dt;
        }
        [WebMethod ]
        public DataTable SOAPGetAllHomes()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Homes"; 

            DataSet ds = new DataSet();
            SqlCommand cmdcnt = new SqlCommand("spCommonSelectStmtColumns", Connection);
            cmdcnt.CommandType = CommandType.StoredProcedure;
            cmdcnt.Parameters.Add("@TableName", SqlDbType.NVarChar).Value = "tbmRETSFeedDataHome";
            cmdcnt.Parameters.Add("@ColumnsList", SqlDbType.NVarChar).Value = "MLSId,GeoLatitude,GeoLongitude,HomeType"; 
            cmdcnt.Parameters.Add("@Condition", SqlDbType.NVarChar).Value = "RecId=RecId";

            SqlDataAdapter dapcnt = new SqlDataAdapter(cmdcnt);
            DataSet dscnt = new DataSet();
            dapcnt.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    dt = ds.Tables[0]; 
                } 
            }

            return dt;
        }
    }
}
