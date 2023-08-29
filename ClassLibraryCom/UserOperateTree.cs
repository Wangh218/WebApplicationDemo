using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ClassLibraryCom
{
    /// <summary>
    /// 用户操作节点
    /// </summary>
    public static class UserOperateTree
    {
        public static TreeNode treeNode;

        /// <summary>
        /// 用户获取可以查看的节点
        /// </summary>
        /// <param name="user"></param>
        /// <returns>用户和节点对应关系</returns>
        public static UserTree UserGetTreeNodes(User user)
        {
            UserTree userOperate = new UserTree 
            { 
                UserName=user.UserName,
                UserNumber=user.UserName,
                Password=user.Password,
                Permission=user.Permission
            };
            if (treeNode != null)
            {
                userOperate.ListNode = treeNode.getJuniors();
            }
            return userOperate;
        }
        /// <summary>
        /// 用户添加节点
        /// </summary>
        /// <param name="userOperate"></param>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static bool UserAddTreeNodes(UserTree userOperate)
        {
            bool ret = false;
            if (userOperate.Permission == EnumPermisson.Administrators && treeNode != null)
            {
                foreach (var node in userOperate.ListNode)
                {
                    ret = treeNode.insertJuniorNode(node);
                    if (!ret)
                    {
                        return ret;
                    }
                }
                
            }
            return ret;
        }
        /// <summary>
        /// 用户根据节点ID更新节点值
        /// </summary>
        /// <param name="userOperate"></param>
        /// <param name="nodeID"></param>
        /// <param name="nodeObj"></param>
        /// <returns></returns>
        public static bool UserUpdateTreeNodes(UserTree userOperate)
        {
            bool ret = false;
            if (userOperate.Permission == EnumPermisson.Administrators && treeNode != null)
            {
                foreach (var node in userOperate.ListNode)
                {
                    ret = treeNode.updateNode(node.getSelfId(), node.getObj());
                    if (!ret)
                    {
                        return ret;
                    }
                }
                
            }
            return ret;
        }
        /// <summary>
        /// 用户删除节点
        /// </summary>
        /// <param name="userOperate"></param>
        /// <param name="nodeID"></param>
        /// <param name="nodeObj"></param>
        /// <returns></returns>
        public static bool UserDelTreeNodes(UserTree userOperate)
        {
            bool ret = false;
            if (userOperate.Permission == EnumPermisson.Administrators && treeNode != null)
            {
                ret = treeNode.deleteNode();
            }
            return ret;
        }
    }
}

