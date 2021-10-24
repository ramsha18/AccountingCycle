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
    public partial class FinancialStatement : System.Web.UI.Page
    {
        public string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        public int income = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int rev = BindGrid(Income1, 5, "TrialBalance");
                int exp = BindGrid(Income2, 6, "TrialBalance");
                Session["Rev"] = rev;
                Session["Exp"] = exp;
                int total = rev - exp;
                income = total;
                if (total > 0)
                {
                    netincome.Text = Convert.ToString(total);
                }
                else
                {
                    netincome.Text = "(" + Convert.ToString(-1 * total) + ")";
                }
                owner();
                bal();
                Session["asset"] = BindGrid(asset, 1, "BalanceSheet");
                Session["liability"] = BindGrid(liability, 2, "BalanceSheet");
                if(Session["asset"] == Session["liability"])
                {
                    Response.Write("<script>alert('Balance Sheet is Not Balanced');</script>");
                }
            }
        }

        public void bal()
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            string query3 = "IF NOT EXISTS(SELECT * from BalanceSheet WHERE AccountTitle = 'Owner Equity')";
            query3 += "BEGIN ";
            query3 += "Insert into BalanceSheet Values ('Owner Equity','2','Credit','" + Convert.ToInt32(Session["equity"]) + "')";
            query3 += " END Else BEGIN Update BalanceSheet Set Amount = '" + Convert.ToInt32(Session["equity"]) + "' Where AccountTitle = 'Owner Equity' END";
            SqlCommand cmd3 = new SqlCommand(query3, con);
            cmd3.ExecuteNonQuery();
        }

        public int BindGrid(GridView name, int type, string table)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT AccountTitle,EntryType, Amount from "+ table + " Where AccountType = " + type + "");
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            DataSet dt = new DataSet();
            sda.Fill(dt);
            int rev = 0;
            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                if (dr["EntryType"].ToString() == "Credit" && type == 1)
                {
                    rev -= Convert.ToInt32(dr["Amount"]);
                }
                else
                {
                    rev += Convert.ToInt32(dr["Amount"]);
                }
            }
            name.Columns[0].FooterText = "Total";
            name.Columns[1].FooterText = rev.ToString();
            name.DataSource = dt;
            name.DataBind();
            return rev;
        }

        public void owner()
        {
            lblBal.Text = Session["capital"].ToString();
            lblIncome.Text = income.ToString();
            lblWith.Text = "(" + Session["withdraw"].ToString() + ")";
            Session["equity"] = (Convert.ToInt32(Session["capital"]) + income - Convert.ToInt32(Session["withdraw"])).ToString();
            endbal.Text = Session["equity"].ToString();
        }
        protected void Home_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}