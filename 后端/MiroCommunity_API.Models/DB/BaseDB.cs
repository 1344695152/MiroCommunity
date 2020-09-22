using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiroCommunity_API.Models.DB
{
    public class BaseDB
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public int? EditUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? EditTime { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 返回新增数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="createUser"></param>
        /// <returns></returns>
        public static T Create<T>(int createUser) where T : BaseDB, new()
        {
            T dB = new T();
            dB.CreateUser = createUser;
            dB.CreateTime = DateTime.Now;
            dB.IsDelete = false;
            return dB;
        }
    }
}