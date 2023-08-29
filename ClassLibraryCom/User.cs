using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCom
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        [Key]
        [Column("UserName")]
        public string UserName { get; set; }//用户姓名
        [Column("UserNumber")]
        public string UserNumber { get; set; }//用户工号
        [Column("Password")]
        public string Password { get; set; }//用户密码
        [Column("Permission")]
        public EnumPermisson Permission { get; set; }//用户权限
    }
    public enum EnumPermisson
    {
        /// <summary>
        /// 管理员
        /// </summary>
        Administrators = 0,
        /// <summary>
        /// 普通用户
        /// </summary>
        CommonUser = 1
    }
    /// <summary>
    /// 用户与节点对应关系
    /// </summary>
    public class UserTree:User
    {
        public List<TreeNode> ListNode { get; set; }//用户可操作的节点集合
    }
}
