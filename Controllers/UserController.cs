using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWithAdo.net.Model;
using CoreWithAdo.net.Repository;
using CoreWithAdo.net.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CoreWithAdo.net.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IOptions<MySettingsModel> appSettings;

        public UserController(IOptions<MySettingsModel> app)
        {
            appSettings = app;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var data = DbClientFactory<UserDbClient>.Instance.GetAllUsers(appSettings.Value.DbConnection);
            return Ok(data);
        }

        [HttpPost]
        [Route("SaveUser")]
        public IActionResult SaveUser([FromBody] UsersModel model)
        {
            var msg = new Message<UsersModel>();
            var data = DbClientFactory<UserDbClient>.Instance.SaveUser(model, appSettings.Value.DbConnection);
            
            if(data == "C200")
            {
                msg.IsSuccess = true;
                if (model.Id != 0)
                    msg.ReturnMessage = "User saved successfully";
                else
                    msg.ReturnMessage = "User updated successfully";
            }
            else if(data == "C201")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Email Id Aready exists";
            }
            else if (data == "C202")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Mobile Number already exist";
            }

            return Ok(msg);
        }
    }
}