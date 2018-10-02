using System;
namespace Mins.QuarkDoc.DataEntities
{
    public class VersionHistory
    {
        public string Id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 用户主键
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 历史文档内容
        /// </summary>
        public string Document { get; set; }
    }
}
