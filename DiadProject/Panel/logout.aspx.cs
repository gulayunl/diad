using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiadProject
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie adminCookie = new HttpCookie("Diad");
            adminCookie["SessID"] = "";
            adminCookie["Name"] = "";
            adminCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(adminCookie);
            Response.Redirect("login.aspx");
        }
    }
}