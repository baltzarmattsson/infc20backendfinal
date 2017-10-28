using INFC20BackendFinal.DataAccessLayer;
using INFC20BackendFinal.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace INFC20BackendFinal.Controllers
{
    public class UserController : ApiController
    {

        // GET: api/User/5
        [HttpPost]
        [Route("api/User/GetUserByEmail")]
        public HttpResponseMessage GetUserByEmail([FromBody]string email)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, UserDAL.GetUser(email));
            }
            catch (SqlException sqle)
            {
                string message = ExceptionHandler.HandleSqlException(sqle);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        // POST: api/User
        [HttpPost]
        public HttpResponseMessage Post([FromBody]User user)
        {
            if (user != null)
            {
                try
                {
                    UserDAL.AddUser(user);
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
                catch (SqlException sqle)
                {
                    string message = ExceptionHandler.HandleSqlException(sqle);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }

        }

        [HttpPost]
        [Route("api/User/IsUserLoginOK/{password}")]
        public bool IsUserLoginOK([FromBody]string userEmail, string password)
        {
            if (userEmail != null)
            {
                try
                {
                    User user = UserDAL.GetUser(userEmail);
                    if (user != null)
                    {
                        return user.Password == password;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (SqlException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // PUT: api/User/5
        [HttpPut]
        public HttpResponseMessage Put([FromBody]User user)
        {
            if (user != null)
            {
                try
                {
                    UserDAL.UpdateUser(user);
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
                catch (SqlException sqle)
                {
                    string message = ExceptionHandler.HandleSqlException(sqle);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }

        }
    }
}
