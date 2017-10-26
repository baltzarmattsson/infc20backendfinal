using INFC20BackendFinal.DataAccessLayer;
using INFC20BackendFinal.Models;
using System;
using System.Collections.Generic;
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
        public IHttpActionResult GetUserByEmail([FromBody]string email)
        {
            return Ok(UserDAL.GetUser(email));
        }

        // POST: api/User
        [HttpPost]
        public IHttpActionResult Post([FromBody]User user) 
        {
            if (user != null)
            {
                UserDAL.AddUser(user);
                return Ok(user);
            }   
            else
            {
                return BadRequest();
            }

        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
