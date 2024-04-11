﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingWeb.Ajax
{
    public class UserInfo
    {
        public UserInfo() { }

        #region 全域變數

        private int iUid;           //使用者代號
        private int iRoles;         //使用者身分
        private string sSessionID;  //使用者登入的SessionID

        #endregion

        #region 屬性

        /// <summary>
        /// 取得或設定使用者代號
        /// </summary>
        public int UID { set { this.iUid = value; } get { return this.iUid; } }

        /// <summary>
        /// 取得或設定使用者身分
        /// </summary>
        public int Roles { set { this.iRoles = value; } get { return this.iRoles; } }

        /// <summary>
        /// 取得或設定SessionID
        /// </summary>
        public string SessionID { set { this.sSessionID = value; } get { return this.sSessionID; } }

        #endregion
    }
}