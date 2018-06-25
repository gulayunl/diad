using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiadProject.Panel
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        dbLibrary db = new dbLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow dr = db.getValueWhere1("select * from tbl_users", "usr_user_id", "=", db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1], "", "ConString");
                if (dr["usr_rank"].ToString() == "Admin")
                {
                    foradmin.Visible = true;
                }
                else if (dr["usr_rank"].ToString() == "Marketing")
                {
                    foradmin.Visible = false;
                }
            }
        }
    }
}