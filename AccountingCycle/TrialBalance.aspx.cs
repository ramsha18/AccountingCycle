using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AccountingCycle
{
    public partial class TrialBalance : System.Web.UI.Page
    {
        public string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindGrid();
            }
        }
        protected void Home_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
        public void BindGrid()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from TrialBalance ORDER BY AccountType");
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            DataSet dt = new DataSet();
            sda.Fill(dt);
            int debit = 0;
            int credit = 0;
            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                if (Convert.ToString(dr["EntryType"]) == "Debit")
                {
                    debit += Convert.ToInt32(dr["Amount"]);
                }
                else
                {
                    credit += Convert.ToInt32(dr["Amount"]);
                }
            }
           
            GridView1.Columns[0].FooterText = "Total";
            GridView1.Columns[1].FooterText = debit.ToString();
            GridView1.Columns[2].FooterText = credit.ToString();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

    }
}