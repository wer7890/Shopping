using System;
using System.Globalization;
using System.Threading;
using System.Web;

namespace ShoppingWeb
{
    public class BasePage : System.Web.UI.Page
    {
        public string cssVersion;
        public string jsVersion;
        public string cookieLanguage;

        public BasePage() 
        {
            this.Init += new EventHandler(BasePage_Init);  //EventHandler: 委派事件  Init:頁面初始化階段
            this.Load += new EventHandler(BasePage_Load);
        }

        /// <summary>
        /// 未登入時，不可進入其他頁面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BasePage_Init(object sender, EventArgs e)
        {
            if (Session["userInfo"] == null)
            {
                Response.Write("<script>window.parent.location.href = 'Login.aspx';</script>");
            }
        }

        /// <summary>
        /// 取得Version.json檔中所記錄js和css的版本號和cookie中的語言
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BasePage_Load(object sender, EventArgs e) 
        {
            if (!IsPostBack)
            {
                string jsonFilePath = Server.MapPath("~/Version.json");
                string jsonText = System.IO.File.ReadAllText(jsonFilePath);
                dynamic versionData = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonText);

                cssVersion = versionData["cssVersion"];
                jsVersion = versionData["jsVersion"];

                //判斷Cookies["language"]是否存在以及Value是不是符合enum
                if (HttpContext.Current.Request.Cookies["language"] == null || !Enum.TryParse(Request.Cookies["language"].Value.ToString(), out Language language))
                {
                    HttpCookie cookie = new HttpCookie("language")
                    {
                        Value = "TW",
                        Expires = DateTime.Now.AddDays(30)
                    };
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    cookieLanguage = "TW";
                    Response.Write("<script>parent.location.reload();</script>");
                }
                else
                {
                    cookieLanguage = Request.Cookies["language"].Value.ToString();
                }

                //設定resx檔
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cookieLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookieLanguage);
            }
        }

    }
}