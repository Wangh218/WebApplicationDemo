using ClassLibraryCom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using WebApplicationDemo;
using WebApplicationDemo.Model;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// 测试用户增加
        /// </summary>
        [TestMethod]
        public void TestMethod_A()
        {
            User user = new User
            {
                UserName = "管理员",
                UserNumber = "1",
                Password = "123456",
                Permission = EnumPermisson.Administrators
            };
            var ret=HttpPost("https://localhost:44390/User/AddUser", JsonConvert.SerializeObject(user));
            Assert.AreEqual(JsonConvert.SerializeObject(user), ret);
        }

        private string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(postDataStr);
            request.ContentLength = payload.Length;

            Stream myRequestStream = request.GetRequestStream();
            myRequestStream.Write(payload, 0, payload.Length);
            myRequestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        /// <summary>
        /// 测试树节点的增删
        /// </summary>
        [TestMethod]
        public void TestMethod_B()
        {
            TreeNode treeNode1 = new TreeNode();
            treeNode1.setParentId(0);
            treeNode1.setSelfId(1);
            treeNode1.setObj("1");

            TreeNode treeNode2 = new TreeNode();
            treeNode2.setParentId(1);
            treeNode2.setSelfId(2);
            treeNode2.setObj("2");
            treeNode2.setParentNode(treeNode1);
            treeNode1.insertJuniorNode(treeNode2);
            //验证子节点是否插入成功
            Assert.AreEqual(treeNode2, treeNode1.getChildList()[0]);

            TreeNode treeNode3 = new TreeNode();
            treeNode3.setParentId(2);
            treeNode3.setSelfId(3);
            treeNode3.setObj("3");
            treeNode3.setParentNode(treeNode1);
            treeNode1.insertJuniorNode(treeNode3);

            treeNode2.deleteNode();

            //验证子节点是否删除成功
            Assert.AreEqual(0, treeNode1.getChildList().Count);
        }

        /// <summary>
        /// 测试用户操作树节点的查看权限
        /// </summary>
        [TestMethod]
        public void TestMethod_C()
        {
            TreeNode treeNode1 = new TreeNode();
            treeNode1.setParentId(0);
            treeNode1.setSelfId(1);
            treeNode1.setObj("1");

            TreeNode treeNode2 = new TreeNode();
            treeNode2.setParentId(1);
            treeNode2.setSelfId(2);
            treeNode2.setObj("2");
            treeNode2.setParentNode(treeNode1);
            treeNode1.insertJuniorNode(treeNode2);

            TreeNode treeNode3 = new TreeNode();
            treeNode3.setParentId(2);
            treeNode3.setSelfId(3);
            treeNode3.setObj("3");
            treeNode3.setParentNode(treeNode1);
            treeNode1.insertJuniorNode(treeNode3);

            User user = new User { UserName = "管理员", UserNumber = "1", Password = "123456", Permission = EnumPermisson.Administrators };
            UserOperateTree.treeNode = treeNode1;
            UserTree userTree = UserOperateTree.UserGetTreeNodes(user);

            //验证用户可查看的节点是不是存在节点的所有子节点
            Assert.AreEqual(treeNode1.getJuniors().Count, userTree.ListNode.Count);
        }

        /// <summary>
        /// 测试用户操作树节点的查看权限--通过WebAPI接口
        /// </summary>
        [TestMethod]
        public void TestMethod_D()
        {
            TreeNode treeNode1 = new TreeNode();
            treeNode1.setParentId(0);
            treeNode1.setSelfId(1);
            treeNode1.setObj("1");

            TreeNode treeNode2 = new TreeNode();
            treeNode2.setParentId(1);
            treeNode2.setSelfId(2);
            treeNode2.setObj("2");
            treeNode2.setParentNode(treeNode1);
            treeNode1.insertJuniorNode(treeNode2);

            TreeNode treeNode3 = new TreeNode();
            treeNode3.setParentId(2);
            treeNode3.setSelfId(3);
            treeNode3.setObj("3");
            treeNode3.setParentNode(treeNode1);
            treeNode1.insertJuniorNode(treeNode3);

            User user = new User { UserName = "管理员", UserNumber = "1", Password = "123456", Permission = EnumPermisson.Administrators };
            UserOperateTree.treeNode = treeNode1;
            UserTree userTree = JsonConvert.DeserializeObject<UserTree>(HttpPost("https://localhost:44390/User/UserGetTreeNodes", JsonConvert.SerializeObject(user))); 

            //验证用户可查看的节点是不是存在节点的所有子节点
            Assert.AreEqual(treeNode1.getJuniors().Count, userTree.ListNode.Count);
        }

    }
}
