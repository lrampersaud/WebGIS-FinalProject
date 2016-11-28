using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebGIS.Models;

namespace WebGIS.Controllers
{
    public class GeocodeController : Controller
    {
        // GET: Geocode
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllBathrooms()
        {
            return View();
        }

        [HttpPost]
        public string UpdateBathroom(Location location)
        {
            var address = location.location;
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());

            var xElement = xdoc.Element("GeocodeResponse");
            var result = xElement?.Element("result");
            var element = result?.Element("geometry");
            var locationElement = element?.Element("location");
            if (locationElement != null)
            {
                var lat = locationElement.Element("lat");
                var lng = locationElement.Element("lng");

                return "success";
            }
            return "failure";
        }
    }
}