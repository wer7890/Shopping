namespace ShoppingWeb.Response
{
    public class GetUserPermissionResponse : BaseResponse
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 身分
        /// </summary>
        public int Roles { get; set; }
    }
}