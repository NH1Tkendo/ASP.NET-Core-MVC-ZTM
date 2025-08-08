using EntityController.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Application
{
    public partial class PostList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string blogIdStr = Request.QueryString["BlogID"];

                if (!string.IsNullOrEmpty(blogIdStr))
                {
                    string condition = $"BlogID = {blogIdStr}";

                    PostController controller = new PostController();
                    var data = controller.SelectWhere(condition);

                    rptBlogs.DataSource = data;
                    rptBlogs.DataBind();
                }
            }
        }
    }
}