using EntityController.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Application
{
    public partial class Post : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var controller = new PostController(); // bạn sẽ tạo class này trong EntityController
                dataRepeater.DataSource = controller.SelectAll(); // lấy danh sách blog
                dataRepeater.DataBind(); // gắn dữ liệu vào Repeater
            }
        }
    }
}