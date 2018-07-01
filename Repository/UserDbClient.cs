using CoreWithAdo.net.Model;
using CoreWithAdo.net.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreWithAdo.net.Translator;
using System.Data.SqlClient;
using System.Data;

namespace CoreWithAdo.net.Repository
{
    public class UserDbClient
    {
        public List<UsersModel> GetAllUsers(string connString)
        {
            return SqlHelper.ExtecuteProcedureReturnData<List<UsersModel>>(connString, "Test_GetUsers", r => r.TranslateAsUsersList());
        }

        public string SaveUser(UsersModel model, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param =
            {
                new SqlParameter("@Id",model.Id),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@EmailId", model.EmailId),
                new SqlParameter("@Mobile", model.Mobile),
                new SqlParameter("@Address", model.Address),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "Test_SaveUser", param);
            return (string)outParam.Value;
        }
    }
}
