using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AccountingCycle
{
    public partial class Create : System.Web.UI.Page
    {
        public string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void submit_click(object sender, EventArgs e)
        {
            if(Debit_Account_Title.Value == string.Empty || Debit_Account_Type.Value == "0" || Credit_Account_Title.Value == string.Empty || Credit_Account_Type.Value == "0" || Amount.Value == string.Empty || Date.Value == string.Empty)
            {
                Response.Write("<script>alert('Please Fill All Fields');</script>");
            }
            else
            {
                SqlConnection con = new SqlConnection(strcon);
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into journal values ('" + Debit_Account_Title.Value + "','" + Debit_Account_Type.Value + "','" + Debit.Value + "','" + Amount.Value + "','" + Date.Value + "') ", con);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    SqlCommand cmd2 = new SqlCommand("insert into journal values ('" + Credit_Account_Title.Value + "','" + Credit_Account_Type.Value + "','" + Credit.Value + "','" + Amount.Value + "','" + Date.Value + "') ", con);
                    int i2 = cmd2.ExecuteNonQuery();
                    if (i2 > 0)
                    {
                        createTable(Debit_Account_Title.Value, Amount.Value, "Debit",Debit_Account_Type.Value);
                        createTable(Credit_Account_Title.Value, Amount.Value, "Credit",Credit_Account_Type.Value);
                        Response.Write("<script>alert('Data Inserted');</script>");
                        Response.Redirect("Home.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Error in Inserting data');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Error in Inserting data');</script>");
                }
                con.Close();
            }
        }

        public void createTable(string name,string amount, string type, string acctype)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            string query = "IF OBJECT_ID('dbo."+name+"', 'U') IS NULL ";
            query += "BEGIN ";
            query += "CREATE TABLE [dbo].["+name+"](";
            query += "[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT pk"+name+" PRIMARY KEY,";
            query += "[Debit] INT NOT NULL,";
            query += "[Credit] INT NOT NULL,";
            query += ")";
            query += " END";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();

            string query2 = "IF NOT EXISTS(SELECT * from TAccount WHERE AccountTitle = '" + name + "')";
            query2 += "BEGIN ";
            query2 += "Insert into TAccount Values('" + name + "','" + acctype + "')";
            query2 += " END";
            SqlCommand cmdd = new SqlCommand(query2, con);
            cmdd.ExecuteNonQuery();

            if (type == "Debit")
            {
                SqlCommand cmd2 = new SqlCommand("Insert into "+name+" values('"+amount+"', '0') ", con);
                cmd2.ExecuteNonQuery();
            }
            else if(type == "Credit")
            {
                SqlCommand cmd2 = new SqlCommand("Insert into " + name + " values('0','" + amount + "') ", con);
                cmd2.ExecuteNonQuery();
            }
            con.Close();
        }
        protected void Home_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}