using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCom
{
    public class TreeNode
    {
        private int parentId;//父节点ID
        private int selfId;//当前节点ID
        protected Object obj;//当前节点值
        protected TreeNode parentNode;//父节点
        protected List<TreeNode> childList;//当前节点的所有子节点

        public TreeNode()
        {
            initChildList();
        }

        public TreeNode(TreeNode parentNode)
        {
            this.getParentNode();
            initChildList();
        }


        #region 树的增删改查
        #endregion

        #region 节点的增删改查
        /* 找到一颗树中某个节点 */
        public TreeNode findTreeNodeById(int id)
        {
            if (this.selfId == id)
                return this;
            if (childList?.Count == 0)
            {
                return null;
            }
            else
            {
                int childNumber = childList.Count();
                for (int i = 0; i < childNumber; i++)
                {
                    TreeNode child = childList[i];
                    TreeNode resultNode = child.findTreeNodeById(id);
                    if (resultNode != null)
                    {
                        return resultNode;
                    }
                }
                return null;
            }
        }

        /* 动态的插入一个新的节点到当前树中 */
        public bool insertJuniorNode(TreeNode treeNode)
        {
            if (this.selfId == treeNode.getParentId())
            {
                addChildNode(treeNode);
                return true;
            }
            else
            {
                int childNumber = this.childList.Count;
                bool insertFlag;

                for (int i = 0; i < childNumber; i++)
                {
                    TreeNode childNode = this.childList[i];
                    insertFlag = childNode.insertJuniorNode(treeNode);
                    if (insertFlag == true)
                        return true;
                }
                return false;
            }
        }

        /* 插入一个child节点到当前节点中 */
        public void addChildNode(TreeNode treeNode)
        {
            initChildList();
            if (!childList.Exists(p => p.selfId == treeNode.selfId))
            {
                childList.Add(treeNode);
            }
        }

        /* 删除节点和它下面的子节点 */
        public bool deleteNode()
        {
            bool ret = false;
            TreeNode parentNode = this.getParentNode();
            int id = this.getSelfId();

            if (parentNode != null)//根节点不删
            {
                ret = parentNode.deleteChildNode(id);
            }
            return ret;
        }

        /* 删除当前节点的某个子节点 */
        public bool deleteChildNode(int childId)
        {
            int childNumber = childList.Count;
            for (int i = 0; i < childNumber; i++)
            {
                TreeNode child = childList[i];
                if (child.getSelfId() == childId)
                {
                    childList.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /* 更新节点值 */
        public bool updateNode(int id, object obj)
        {
            TreeNode treeNode = findTreeNodeById(id);
            if (treeNode == null)
            {
                return false;
            }
            treeNode.setObj(obj);
            return true;
        }
        #endregion

        /* 返回当前节点的所有子节点*/
        public List<TreeNode> getJuniors()
        {
            List<TreeNode> juniorList = new List<TreeNode>();
            if (childList == null)
            {
                return juniorList;
            }
            else
            {
                int childNumber = childList.Count;
                for (int i = 0; i < childNumber; i++)
                {
                    TreeNode junior = childList[i];
                    juniorList.Add(junior);
                    juniorList.AddRange(junior.getJuniors());
                }
                return juniorList;
            }
        }


        public void initChildList()
        {
            if (childList == null)
                childList = new List<TreeNode>();
        }

        public List<TreeNode> getChildList()
        {
            return childList;
        }
        public void setChildList(List<TreeNode> childList)
        {
            this.childList = childList;
        }

        public int getParentId()
        {
            return parentId;
        }

        public void setParentId(int parentId)
        {
            this.parentId = parentId;
        }

        public int getSelfId()
        {
            return selfId;
        }

        public void setSelfId(int selfId)
        {
            this.selfId = selfId;
        }

        public TreeNode getParentNode()
        {
            return parentNode;
        }

        public void setParentNode(TreeNode parentNode)
        {
            this.parentNode = parentNode;
        }

        public Object getObj()
        {
            return obj;
        }

        public void setObj(Object obj)
        {
            this.obj = obj;
        }
    }

    public class Tree
    {
        public TreeNode Root { get; private set; }
        public Tree()
        {
            Root = null;
        }
    }
}
