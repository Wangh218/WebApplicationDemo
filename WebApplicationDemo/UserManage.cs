using ClassLibraryCom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDemo.Model;

namespace WebApplicationDemo
{
    public class UserManage
    {
        private UserContext _userContext;

        public UserManage(UserContext userContext)
        {
            _userContext = userContext;
        }

        //新增用户
        public User AddUser(string userName, string userNumber, string password, EnumPermisson permission)
        {
            User user = new User
            {
                UserName = userName,
                UserNumber = userNumber,
                Password = password,
                Permission = permission
            };
            _userContext.users.Add(user);
            return user;
        }
        //查询用户
        public  User FindUser(string userNumber)
        {
            User user = _userContext.users.Find(userNumber);
            return user;
        }
        //更新用户
        public  bool UpdateUser(User user)
        {
           _userContext.users.Update(user);
            return true;
        }
        //删除用户
        public  bool DeleteUser(User user)
        {
            _userContext.users.Remove(user);
            return true;
        }
    }

  
}
