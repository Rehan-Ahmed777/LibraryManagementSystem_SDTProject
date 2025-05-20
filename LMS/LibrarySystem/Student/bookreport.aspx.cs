using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class bookreport : System.Web.UI.Page
{
    DS_PUBLICATION.PUBLICATION_SELECTDataTable PubDT = new DS_PUBLICATION.PUBLICATION_SELECTDataTable();
    DS_PUBLICATIONTableAdapters.PUBLICATION_SELECTTableAdapter PubAdapter = new DS_PUBLICATIONTableAdapters.PUBLICATION_SELECTTableAdapter();
  
    DS_BRANCH.BRANCH_SELECTDataTable BDT = new DS_BRANCH.BRANCH_SELECTDataTable();
    DS_BRANCHTableAdapters.BRANCH_SELECTTableAdapter BAdapter = new DS_BRANCHTableAdapters.BRANCH_SELECTTableAdapter();
    DS_BOOK.BOOK_SELECTDataTable BookDT = new DS_BOOK.BOOK_SELECTDataTable();
    DS_BOOKTableAdapters.BOOK_SELECTTableAdapter BookAdapter = new DS_BOOKTableAdapters.BOOK_SELECTTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblmsg0.Text = "";
        if (Page.IsPostBack == false)
        {
            BDT = BAdapter.SelectBranch();
            drpbranch.DataSource = BDT;
            drpbranch.DataTextField = "Branchname";
            drpbranch.DataValueField = "Branchid";
            drpbranch.DataBind();
            drpbranch.Items.Insert(0, "SELECT");
            MultiView1.ActiveViewIndex = -1;

            PubDT = PubAdapter.Select();
            drppublication.DataSource = PubDT;
            drppublication.DataTextField = "Publication";
            drppublication.DataValueField = "pid";
            drppublication.DataBind();
            drppublication.Items.Insert(0, "SELECT");
            
        }
    }
    protected void btnviewbranch_Click(object sender, EventArgs e)
    {
        if (drpbranch.SelectedIndex == 0)
        {
            lblmsg.Text = "Select Branch";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            GridView1.DataSource = null;
            GridView1.DataBind(); MultiView1.ActiveViewIndex = -1;
        }
        else
        {
            BookDT = BookAdapter.Select_By_Branch(drpbranch.SelectedItem.Text);
            GridView1.DataSource = BookDT;
            GridView1.DataBind();
            lblmsg0.Text = GridView1.Rows.Count.ToString()+ " - Records Found";
           
            MultiView1.ActiveViewIndex = 0;
        }
    }
    protected void btnviewpublication_Click(object sender, EventArgs e)
    {
        if (drppublication.SelectedIndex == 0)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            lblmsg.Text = "Select Publication";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            MultiView1.ActiveViewIndex = -1;
        }
        else
        {
            MultiView1.ActiveViewIndex = 0;

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["LibrarySystemConnectionString"].ConnectionString;
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("BOOK_SELECT_By_PUBLICATION", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PUB", drppublication.SelectedItem.Text);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
            lblmsg0.Text = GridView1.Rows.Count.ToString() + " - Records Found";
        }
    }

    protected void Button11_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;

        int bookId = Convert.ToInt32(e.CommandArgument.ToString());
        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["LibrarySystemConnectionString"].ConnectionString;
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("BOOK_SELECT_By_BID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bid", bookId);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }

        if (dt.Rows.Count > 0)
        {
            lblbname.Text = dt.Rows[0]["Bookname"].ToString();
            lblauthor.Text = dt.Rows[0]["author"].ToString();
            lblbran.Text = dt.Rows[0]["branch"].ToString();
            lblpub.Text = dt.Rows[0]["publication"].ToString();
            lblprice.Text = dt.Rows[0]["price"].ToString();
            lblqnt.Text = dt.Rows[0]["Quantities"].ToString();
            lblaqnt.Text = dt.Rows[0]["availableqnt"].ToString();
            lblrqnt.Text = dt.Rows[0]["rentqnt"].ToString();
            lbldetail.Text = dt.Rows[0]["Detail"].ToString();
            Image2.ImageUrl = dt.Rows[0]["Image"].ToString();
        }
        else
        {
            lblbname.Text = "Book not found.";
        }
    }

}