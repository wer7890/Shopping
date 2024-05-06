using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;

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

                HttpCookie cookie = new HttpCookie("language")
                {
                    Value = "TW",
                    Expires = DateTime.Now.AddDays(1)
                };
                HttpContext.Current.Response.Cookies.Add(cookie);

                string selectedLanguage = Request.Cookies["language"].Value;
                Thread.CurrentThread.CurrentCulture = new CultureInfo(selectedLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage);
            }
            

        }

    }
}