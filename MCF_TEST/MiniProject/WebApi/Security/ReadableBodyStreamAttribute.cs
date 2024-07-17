using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace WebApi.Security
{
    public class ReadableBodyStreamAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userName = context.HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            var userId = context.HttpContext.User.Claims.First(claim => claim.Type == "Id").Value;
            var biodata_id = context.HttpContext.User.Claims.First(claim => claim.Type == "biodataid").Value;
            var customer_id = context.HttpContext.User.Claims.First(claim => claim.Type == "customerid").Value;
            var doctor_id = context.HttpContext.User.Claims.First(claim => claim.Type == "doctorid").Value;
            new ClaimContext(userName, long.Parse(userId), long.Parse(biodata_id), (long)long.Parse(customer_id), (long)long.Parse(doctor_id));
        }
    }

    public class ClaimContext
    {
        private static string _userName;
        private static long _userId;
        private static long _biodata_id;
        private static long _customer_id;
        private static long _doctor_id;
        
        public ClaimContext(string userName, long id, long biodataid, long customerid, long doctorid)
        {
            _userName = userName;
            _userId = id;
            _biodata_id = biodataid;
            _customer_id = customerid;
            _doctor_id = doctorid;
        }

        public static string userName()
        {
            return _userName;
        }

        public static long userId()
        {
            return _userId;
        }

        public static long biodata_id()
        {
            return _biodata_id;
        }

        public static long customer_id()
        {
            return _customer_id;
        }

        public static long doctor_id()
        {
            return _doctor_id;
        }

    }
}
