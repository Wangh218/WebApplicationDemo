using ClassLibraryCom;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDemo.Model;

namespace WebApplicationDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserContext _userContext;

        public UserController(UserContext userContext)
        {
            _userContext = userContext;
        }
        [HttpGet("userNumber")]
        public async Task<ActionResult<UserTree>> UserGetTreeNodes(string userNumber)
        {
            var user = await _userContext.users.FindAsync(userNumber);
            if (user == null) 
            { 
                return NotFound(); 
            }
            else
            {
                UserTree userTree = UserOperateTree.UserGetTreeNodes(user);
                return userTree;
            }
        
        }
        [HttpPost]
        public ActionResult<bool> UserAddTreeNodes(UserTree userOperate)
        {
            return UserOperateTree.UserAddTreeNodes(userOperate);
        }
        [HttpPost]
        public ActionResult<bool> UserUpdateTreeNodes(UserTree userOperate)
        {
            return UserOperateTree.UserUpdateTreeNodes(userOperate);
        }
        [HttpPost]
        public ActionResult<bool> UserDelTreeNodes(UserTree userOperate)
        {
            return UserOperateTree.UserDelTreeNodes(userOperate);
        }
        [HttpPost]
        public ActionResult<bool> AddUser(User user)
        {
            var addUser= new UserManage(_userContext).AddUser(user.UserName, user.UserNumber, user.Password, user.Permission);
            if (addUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
