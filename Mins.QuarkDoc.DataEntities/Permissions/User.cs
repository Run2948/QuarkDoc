using System;

namespace Mins.QuarkDoc.DataEntities
{
    /// <summary>
    /// 用户表
    /// Author:http://www.cnblogs.com/jonins
    /// Time:2018年8月15日
    /// </summary>
    public class User
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; } = false;
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool IsEnabled { get; set; } = true;
    }
}
