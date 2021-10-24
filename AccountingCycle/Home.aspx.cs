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
    public partial class Home : System.Web.UI.Page
    {
        public string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public string status;
        protected void Page_Load(object sender, EventArgs e)
        {
            closeAccount();
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        public void closeAccount()
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from Closing where Status = 'Open' ", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                status = reader["bool"].ToString();
            }
            Session["status"] = status;
            reader.Close();
            con.Close();
        }
        public void BindGrid()
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Date, AccountTitle, EntryType, Amount from journal");
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            DataSet dt = new DataSet();
            sda.Fill(dt);
            int debit = 0;
            int credit = 0;
            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                if(Convert.ToString(dr["EntryType"]) == "Debit")
                {
                    debit += Convert.ToInt32(dr["Amount"]);
                }
                else
                {
                    credit += Convert.ToInt32(dr["Amount"]);
                }
            }

            GridView1.Columns[1].FooterText = "Total";
            GridView1.Columns[2].FooterText = debit.ToString();
            GridView1.Columns[3].FooterText = credit.ToString();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if(status == "false")
            {
                SqlConnection con = new SqlConnection(strcon);
                con.Open();
                SqlCommand cmd2 = new SqlCommand("Update Closing Set bool = 'true' where Status = 'Open' ", con);
                cmd2.ExecuteNonQuery();

                SqlCommand q5 = new SqlCommand("SELECT AccountTitle, AccountType from TAccount", con);
                SqlDataReader reader = q5.ExecuteReader();
                while (reader.Read())
                {
                    string acc = reader["AccountType"].ToString();
                    if (acc == "4" || acc == "5" || acc == "6")
                    {
                        SqlCommand q6 = new SqlCommand("Drop table " + reader["AccountTitle"].ToString() + "", con);
                        q6.ExecuteNonQuery();
                        SqlCommand q7 = new SqlCommand("Delete from TAccount where AccountTitle =  '" + reader["AccountTitle"].ToString() + "'", con);
                        q7.ExecuteNonQuery();
                        SqlCommand q8 = new SqlCommand("Delete from TrialBalance where AccountTitle =  '" + reader["AccountTitle"].ToString() + "'", con);
                        q8.ExecuteNonQuery();
                    }
                }
                reader.Close();
            }
            Response.Redirect("Create.aspx");
        }

        protected void Trial_ServerClick(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT AccountTitle, AccountType from TAccount", con);
            int credit = 0, debit = 0;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string name, type;
                name = reader["AccountTitle"].ToString();
                type = reader["AccountType"].ToString();
                SqlCommand cmddebit = new SqlCommand("SELECT SUM(Debit) from " + name + "", con);
                SqlCommand cmdcredit = new SqlCommand("SELECT SUM(Credit) from " + name + "", con);

                if (cmddebit.ExecuteScalar() is DBNull && cmdcredit.ExecuteScalar() is DBNull)
                {
                    debit = 0;
                    credit = 0;
                }
                else
                {
                    debit = Convert.ToInt32(cmddebit.ExecuteScalar());
                    credit = Convert.ToInt32(cmdcredit.ExecuteScalar());
                }

                int total = credit - debit;
                if (total > 0 || (total == 0 && type == "5"))
                {
                    string query2 = "IF NOT EXISTS(SELECT * from TrialBalance WHERE AccountTitle = '" + name + "')";
                    query2 += "BEGIN ";
                    query2 += "Insert into TrialBalance Values ('" + name + "','" + type + "','Credit','" + total + "')";
                    query2 += " END Else BEGIN Update TrialBalance Set Amount = '"+total+"', EntryType = 'Credit' Where AccountTitle = '"+name+"' END";
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.ExecuteNonQuery();
                }
                else
                {
                    string query2 = "IF NOT EXISTS(SELECT * from TrialBalance WHERE AccountTitle = '" + name + "')";
                    query2 += "BEGIN ";
                    query2 += "Insert into TrialBalance Values ('" + name + "','" + type + "','Debit','" + -1 * (total) + "')";
                    query2 += " END Else BEGIN Update TrialBalance Set Amount = '" + -1 * (total) + "', EntryType = 'Debit' Where AccountTitle = '" + name + "' END";
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.ExecuteNonQuery();
                }
            }
            con.Close();
            Response.Redirect("TrialBalance.aspx");
        }

        protected void Fin_ServerClick(object sender, EventArgs e)
        {
            if(status == "true")
            {
                Income();
                Equity();
                Balance();
                Response.Redirect("FinancialStatement.aspx");
            }
            else
            {
                Response.Write("<script>alert('Account Close');</script>");
            }
            
        }
        public void Income()
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT AccountTitle, EntryType, Amount from TrialBalance Where AccountType = 5 OR AccountType = 6", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string name, entrytype, amount;
                name = reader["AccountTitle"].ToString();
                entrytype = reader["EntryType"].ToString();
                amount = reader["Amount"].ToString();
                if(entrytype == "Debit")
                {
                    string query2 = "IF NOT EXISTS(SELECT * from IncomeSummary WHERE AccountTitle = '" + name + "')";
                    query2 += "BEGIN ";
                    query2 += "Insert into IncomeSummary Values ('" + name + "','" + amount + "','0')";
                    query2 += " END Else BEGIN Update IncomeSummary Set Debit = '" + amount + "' Where AccountTitle = '" + name + "' END";
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.ExecuteNonQuery();
                }
                else if (entrytype == "Credit")
                {
                    string query2 = "IF NOT EXISTS(SELECT * from IncomeSummary WHERE AccountTitle = '" + name + "')";
                    query2 += "BEGIN ";
                    query2 += "Insert into IncomeSummary Values ('" + name + "','0','" + amount + "')";
                    query2 += " END Else BEGIN Update IncomeSummary Set Credit = '" + amount + "' Where AccountTitle = '" + name + "' END";
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.ExecuteNonQuery();
                }
            }
            reader.Close();
            con.Close();
        }
        public void Equity()
        {
            Session["withdraw"] = "0";
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Amount, AccountType from TrialBalance Where AccountType = 3 OR AccountType = 4", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string cap = reader["AccountType"].ToString() ;
                if (cap == "3")
                {
                    Session["capital"] = Convert.ToInt32(reader["Amount"]);

                }
                else if(cap == "4")
                {
                    Session["withdraw"] = Convert.ToInt32(reader["Amount"]);
                }
            }
            reader.Close();
            con.Close();
        }
        public void Balance()
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT AccountTitle, AccountType, EntryType, Amount from TrialBalance Where AccountType = 1 OR AccountType = 2", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string name, entrytype, amount, type;
                name = reader["AccountTitle"].ToString();
                entrytype = reader["EntryType"].ToString();
                amount = reader["Amount"].ToString();
                type = reader["AccountType"].ToString();

                string query2 = "IF NOT EXISTS(SELECT * from BalanceSheet WHERE AccountTitle = '" + name + "')";
                query2 += "BEGIN ";
                query2 += "Insert into BalanceSheet Values ('" + name + "','" + type + "','" + entrytype + "','" + amount + "')";
                query2 += " END Else BEGIN Update BalanceSheet Set Amount = '" + amount + "', EntryType = '"+entrytype+"' Where AccountTitle = '" + name + "' END";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                cmd2.ExecuteNonQuery();
            }
            reader.Close();
            con.Close();
        }
        protected void Closing_ServerClick(object sender, EventArgs e)
        {
            closeAccount();
            if (Session["Rev"] == null && status == "true")
            {
                Response.Write("<script>alert('Create Financial Statements First');</script>");
            }
            else
            {
                SqlConnection con = new SqlConnection(strcon);
                con.Open();
                if (status == "true")
                {
                    if (Session["asset"] == Session["liability"])
                    {
                        Response.Write("<script>alert('Balance Sheet is Not Balanced');</script>");
                    }
                    else
                    {
                        SqlCommand q1 = new SqlCommand("Delete from BalanceSheet", con);
                        q1.ExecuteNonQuery();
                        SqlCommand q2 = new SqlCommand("Delete from IncomeSummary", con);
                        q2.ExecuteNonQuery();
                        SqlCommand q3 = new SqlCommand("Delete from Capital", con);
                        q3.ExecuteNonQuery();
                        SqlCommand q4 = new SqlCommand("Insert into Capital Values ('0','" + Convert.ToInt32(Session["equity"]) + "')", con);
                        q4.ExecuteNonQuery();
                        SqlCommand q5 = new SqlCommand("SELECT AccountTitle, AccountType from TAccount", con);
                        SqlDataReader reader = q5.ExecuteReader();
                        while (reader.Read())
                        {
                            string acc = reader["AccountType"].ToString();
                            if (acc == "4" || acc == "5" || acc == "6")
                            {
                                SqlCommand q6 = new SqlCommand("Delete from " + reader["AccountTitle"].ToString() + "", con);
                                q6.ExecuteNonQuery();
                            }
                        }
                        reader.Close();
                        SqlCommand q7 = new SqlCommand("Delete from journal", con);
                        q7.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("Update Closing Set bool = 'false' where Status = 'Open' ", con);
                        cmd2.ExecuteNonQuery();
                        Response.Write("<script>alert('Account Close');</script>");
                        Response.Redirect("Home.aspx");
                    }
                }
                else if (status == "false")
                {
                    Response.Write("<script>alert('Closing done for this month already');</script>");
                }
                con.Close();
            }
        }
    }
}