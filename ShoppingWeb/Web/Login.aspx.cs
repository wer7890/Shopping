using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data;
using System.Web.Services;

namespace ShoppingWeb.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        




        /// <summary>
        /// 判斷是否有非法字元
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool IsSpecialChar(string userName, string pwd)
        {
            bool regUserName = Regex.IsMatch(userName, @"^[A-Za-z0-9]+$");
            bool regPwd = Regex.IsMatch(pwd, @"^[A-Za-z0-9]+$");

            if (regUserName & regPwd)
            {
                return true;
            }
            return false;

        }

    }
}