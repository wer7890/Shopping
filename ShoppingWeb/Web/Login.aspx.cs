using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Web;

namespace ShoppingWeb.Web
{
    public partial class Login : System.Web.UI.Page
    {
        public string cssVersion;
        public string jsVersion;

        public string aaa;
        public string resourceFile;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string jsonFilePath = Server.MapPath("~/Version.json");
                string jsonText = System.IO.File.ReadAllText(jsonFilePath);
                dynamic versionData = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonText);

                cssVersion = versionData["cssVersion"];
                jsVersion = versionData["jsVersion"];


                resourceFile = "Resource" + (Request.Cookies["Language"] != null && Request.Cookies["Language"].Value == "TW" ? "TW" : "EN");
                string a = resourceFile;
                aaa = Resources.ResourceEN.titleLogin;
                
            }

        }

    }
}