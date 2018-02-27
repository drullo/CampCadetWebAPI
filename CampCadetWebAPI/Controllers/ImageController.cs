using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Hosting;
using System.Web.Http.Cors;
using System;

namespace CampCadetWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/images")]
    public class ImageController : ApiController
    {
        #region Get
        [HttpGet]
        [Route("banner")]
        public IHttpActionResult Get()
        {
            var root = HostingEnvironment.MapPath("~/");
            var path = HostingEnvironment.MapPath("~/Images/Banner");
            var directory = new DirectoryInfo(path);
            var list = directory.GetFiles()
                .Select(f => f.FullName.Replace(root, null).Replace("\\", "/"));
            var randomOrderList = list.OrderBy(f => Guid.NewGuid()).ToList();

            return Ok(randomOrderList);
        }

        [HttpGet]
        [Route("banner/{orientation}")]
        public IHttpActionResult Get(string orientation)
        {
            var root = HostingEnvironment.MapPath("~/");
            var path = HostingEnvironment.MapPath($"~/Images/Banner/{orientation}");

            var directory = new DirectoryInfo(path);
            var list = directory.GetFiles()
                .Select(f => f.FullName.Replace(root, null).Replace("\\", "/"));
            var randomOrderList = list.OrderBy(f => Guid.NewGuid()).ToList();

            return Ok(randomOrderList);

            /*var images = orientation.ToLower() == "landscape" ?
                "Images/Banner/landscape/k9.jpg;Images/Banner/landscape/armor.jpg;Images/Banner/landscape/handcuffs.jpg;Images/Banner/landscape/marchine.jpg;Images/Banner/landscape/car.jpg;Images/Banner/landscape/water-bottle.jpg;Images/Banner/landscape/dog-smile.jpg;Images/Banner/landscape/med-evac.jpg;Images/Banner/landscape/boy-ball.jpg;Images/Banner/landscape/girl-ball.jpg;Images/Banner/landscape/radar.jpg" :
                "Images/Banner/portrait/bird.jpg;Images/Banner/portrait/gym-formation.jpg;Images/Banner/portrait/bearcat-side.jpg;Images/Banner/portrait/kid-cert.jpg;Images/Banner/portrait/psp-cert.jpg;Images/Banner/portrait/bearcat-front.jpg;Images/Banner/portrait/psp-hellicopter.jpg;Images/Banner/portrait/cert-carry.jpg;Images/Banner/portrait/fire-gear.jpg;Images/Banner/portrait/bear-kid.jpg;Images/Banner/portrait/fire-gear2.jpg";

            return Ok(images.Split(';'));*/
        }
        #endregion
    }
}