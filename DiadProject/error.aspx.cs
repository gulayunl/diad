using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiadProject
{
    public partial class error : System.Web.UI.Page
    {
        public string className;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.className = "MyContentPage";
        }

        dbLibrary db = new dbLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["code"] != null)
            {
                code.Text = Request.QueryString["code"].ToString() + " <br/> Bir Hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }
            else { code.Text = "Sayfa bulunamadı."; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            HtmlTextWriter htw = new HtmlTextWriter(new StringWriter(sb));
            base.Render(htw);
            string html = sb.ToString();
            html = html.Replace("<body", string.Format("<body class=\"content\""));
            writer.Write(html);
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            string viewStateID = (string)((Pair)base.LoadPageStateFromPersistenceMedium()).Second;
            string stateStr = (string)Cache[viewStateID];
            if (stateStr == null)
            {
                string fn = Server.MapPath("/storage/files/states/") + viewStateID;
                stateStr = File.ReadAllText(fn);
            }
            return new ObjectStateFormatter().Deserialize(stateStr);
        }

        protected override void SavePageStateToPersistenceMedium(object state)
        {
            string value = new ObjectStateFormatter().Serialize(state);
            string viewStateID = (DateTime.Now.Ticks + (long)this.GetHashCode()).ToString();
            string fn = Server.MapPath("/storage/files/states/") + viewStateID;
            //ThreadPool.QueueUserWorkItem(File.WriteAllText(fn, value));
            File.WriteAllText(fn, value);
            Cache.Insert(viewStateID, value);
            base.SavePageStateToPersistenceMedium(viewStateID);
        }
    }
}