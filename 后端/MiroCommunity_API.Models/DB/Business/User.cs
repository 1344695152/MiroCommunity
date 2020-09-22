using System;

namespace MiroCommunity_API.Models.DB
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : BaseDB
    {
        /// <summary>
        /// 用户代码 (用于登入)
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 角色代码
        /// </summary>
        public int RoleId { get; set; }
    }
}