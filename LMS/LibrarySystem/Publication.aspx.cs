using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Publication : System.Web.UI.Page
{
    DS_PUBLICATION.PUBLICATION_SELECTDataTable PubDT = new DS_PUBLICATION.PUBLICATION_SELECTDataTable();
    DS_PUBLICATIONTableAdapters.PUBLICATION_SELECTTableAdapter PubAdapter = new DS_PUBLICATIONTableAdapters.PUBLICATION_SELECTTableAdapter();

    DS_BOOK.BOOK_SELECTDataTable BDT = new DS_BOOK.BOOK_SELECTDataTable();
    DS_BOOKTableAdapters.BOOK_SELECTTableAdapter BAdapter = new DS_BOOKTableAdapters.BOOK_SELECTTableAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (Page.IsPostBack == false)
        {

            PubDT = PubAdapter.Select();
            GridView1.DataSource = PubDT;
            GridView1.DataBind();
        }

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        PubAdapter.Insert(txtpub.Text);
        lblmsg.Text = "Publication Inserted";

        PubDT = PubAdapter.Select();
        GridView1.DataSource = PubDT;
        GridView1.DataBind();
        txtpub.Text = "";
        txtpub.Focus();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int pid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value); // Publication ID
        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["LibrarySystemConnectionString"].ConnectionString;
        string publicationName = "";
        bool hasBooks = false;

        using (SqlConnection con = new SqlConnection(connStr))
        {
            con.Open();

            // Get Publication Name by PID
            using (SqlCommand cmd = new SqlCommand("SELECT Publication FROM PublicationMst WHERE PID = @PID", con))
            {
                cmd.Parameters.AddWithValue("@PID", pid);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    publicationName = result.ToString();
                }
            }

            // Check if any books exist under this publication
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM BookMst WHERE Publication = @Publication", con))
            {
                cmd.Parameters.AddWithValue("@Publication", publicationName);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                hasBooks = count > 0;
            }

            if (hasBooks)
            {
                lblmsg.Text = "Please delete books under this publication first.";
            }
            else
            {
                // Delete the publication
                using (SqlCommand cmd = new SqlCommand("DELETE FROM PublicationMst WHERE PID = @PID", con))
                {
                    cmd.Parameters.AddWithValue("@PID", pid);
                    cmd.ExecuteNonQuery();
                    lblmsg.Text = "Publication Deleted";
                }
            }

            con.Close();
        }

        // Refresh GridView
        DataTable pubDT = new DataTable();
        using (SqlConnection con = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM PublicationMst", con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(pubDT);
                }
            }
        }

        GridView1.DataSource = pubDT;
        GridView1.DataBind();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        PubDT = PubAdapter.Select();
        GridView1.DataSource = PubDT;
        GridView1.DataBind();

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        PubDT = PubAdapter.Select();
        GridView1.DataSource = PubDT;
        GridView1.DataBind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int pid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        TextBox pname = GridView1.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox;

        PubAdapter.Update(pid, pname.Text);
        lblmsg.Text = "Publication Updated";
        GridView1.EditIndex = -1;
        PubDT = PubAdapter.Select();
        GridView1.DataSource = PubDT;
        GridView1.DataBind();

    }
}