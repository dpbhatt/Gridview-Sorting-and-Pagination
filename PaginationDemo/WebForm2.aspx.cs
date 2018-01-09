using MyEntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PaginationDemo
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int totalRows = 0;
                GridView1.DataSource = EmployeeRepository.
                    GetEmployees(0, 5, GridView1.Attributes["CurrentSortField"], GridView1.Attributes["CurrentSortDirection"], out totalRows);

                GridView1.DataBind();

                Databind_DDLPageNumbers(0, 5, totalRows);
            }
        }
        private void Databind_DDLPageNumbers(int pageIndex, int pageSize, int totalRows)
        {
            int totalPages = totalRows / pageSize;
            if ((totalRows % pageSize) != 0)
            {
                totalPages += 1;
            }

            if (totalPages > 1)
            {
                ddlPageNumbers.Enabled = true;
                ddlPageNumbers.Items.Clear();
                for (int i = 1; i <= totalPages; i++)
                {
                    ddlPageNumbers.Items.Add(new
                        ListItem(i.ToString(), i.ToString()));
                }
            }
            else
            {
                ddlPageNumbers.SelectedIndex = 0;
                ddlPageNumbers.Enabled = false;
            }
        }

        private void SortGridview(GridView gridView, GridViewSortEventArgs e, out SortDirection sortDirection, out string sortField)
        {
            sortField = e.SortExpression;
            sortDirection = e.SortDirection;

            if (gridView.Attributes["CurrentSortField"] != null &&
                gridView.Attributes["CurrentSortDirection"] != null)
            {
                if (sortField == gridView.Attributes["CurrentSortField"])
                {
                    if (gridView.Attributes["CurrentSortDirection"] == "ASC")
                    {
                        sortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Ascending;
                    }
                }

                gridView.Attributes["CurrentSortField"] = sortField;
                gridView.Attributes["CurrentSortDirection"] =
                    (sortDirection == SortDirection.Ascending ? "ASC" : "DESC");
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string sortField = string.Empty;

            SortGridview(GridView1, e, out sortDirection, out sortField);
            string strSortDirection =
                sortDirection == SortDirection.Ascending ? "ASC" : "DESC";

            int totalRows = 0;

            int pageSize = int.Parse(ddlPageSize.SelectedValue);
            int pageNumber = int.Parse(ddlPageNumbers.SelectedValue) - 1;

            GridView1.DataSource = EmployeeRepository.GetEmployees(pageNumber, pageSize, e.SortExpression, strSortDirection, out totalRows);
            GridView1.DataBind();
            ddlPageNumbers.SelectedValue = (pageNumber + 1).ToString();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int totalRows = 0;

            int pageSize = int.Parse(ddlPageSize.SelectedValue);
            int pageIndex = 0;

            GridView1.PageSize = pageSize;

            GridView1.DataSource = EmployeeRepository.
                GetEmployees(pageIndex, pageSize,
                GridView1.Attributes["CurrentSortField"],
                GridView1.Attributes["CurrentSortDirection"],
                out totalRows);
            GridView1.DataBind();

            Databind_DDLPageNumbers(pageIndex, pageSize, totalRows);
        }
        protected void ddlPageNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int totalRows = 0;

            int pageSize = int.Parse(ddlPageSize.SelectedValue);
            int pageIndex = int.Parse(ddlPageNumbers.SelectedValue) - 1;

            GridView1.PageSize = pageSize;

            GridView1.DataSource = EmployeeRepository.
                GetEmployees(pageIndex, pageSize,
                GridView1.Attributes["CurrentSortField"],
                GridView1.Attributes["CurrentSortDirection"],
                out totalRows);
            GridView1.DataBind();
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (GridView1.Attributes["CurrentSortField"] != null && GridView1.Attributes["CurrentSortDirection"] != null)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    foreach (TableCell tableCell in e.Row.Cells)
                    {
                        if (tableCell.HasControls())
                        {
                            LinkButton sortLinkButton = null;
                            if (tableCell.Controls[0] is LinkButton)
                            {
                                sortLinkButton = (LinkButton)tableCell.Controls[0];
                            }

                            if (sortLinkButton != null && GridView1.Attributes["CurrentSortField"] == sortLinkButton.CommandArgument)
                            {
                                Image image = new Image();
                                if (GridView1.Attributes["CurrentSortDirection"] == "ASC")
                                {
                                    image.ImageUrl = "~/Images/up_arrow.png";
                                }
                                else
                                {
                                    image.ImageUrl = "~/Images/down_arrow.png";
                                }
                                tableCell.Controls.Add(new LiteralControl("&nbsp;"));
                                tableCell.Controls.Add(image);
                            }
                        }
                    }
                }
            }
        }
    }
}