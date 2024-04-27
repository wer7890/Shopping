using System;

namespace ShoppingWeb.Web
{
    public partial class Login : System.Web.UI.Page
    {
        public string cssVersion;
        public string jsVersion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string jsonFilePath = Server.MapPath("~/Version.json");
                string jsonText = System.IO.File.ReadAllText(jsonFilePath);
                dynamic versionData = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonText);

                cssVersion = versionData["cssVersion"];
                jsVersion = versionData["jsVersion"];
            }
        }

    }
}