using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiroCommunity_API.Models.DB.Log
{
    public class LoginLog
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 登录人
        /// </summary>
        public int UserId { get; set; }
    }
}