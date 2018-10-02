using System;

namespace Mins.QuarkDoc.DataEntities
{
    public class Application
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 导航属性
        /// </summary>
        public User User { get; set; }
    }
}
