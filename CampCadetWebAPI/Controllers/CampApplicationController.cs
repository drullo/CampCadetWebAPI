using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CampCadetWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CampApplicationController : ApiController
    {
        [HttpGet]
        [Route("api/applications/latest")]
        public IHttpActionResult GetLatest()
        {
            var root = HostingEnvironment.MapPath("~/");
            var path = HostingEnvironment.MapPath("~/applications");
            var directory = new DirectoryInfo(path);
            var latest = directory.GetFiles()
                .OrderByDescending(f => f.Name)
                .Select(f => f.FullName.Replace(root, null).Replace("\\", "/"))
                .FirstOrDefault();


            return Ok(latest);

            //return Ok("applications/2018.doc");
        }
    }
}
