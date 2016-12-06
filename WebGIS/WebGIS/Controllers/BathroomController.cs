using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebGIS.Models;

namespace WebGIS.Controllers
{
    public class BathroomController : ApiController
    {


        // GET: api/Bathroom
        /// <summary>
        /// Call this to get all bathrooms
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Location> Get()
        {
            DataProvider provider = new DataProvider();
            return provider.GetAllLocations();
        }



        /// <summary>
        /// Send latitude and longitude to  obtain public restrooms
        /// </summary>
        /// <param name="Latitude">your current latutide</param>
        /// <param name="Longitude">your current longitude</param>
        /// <param name="AmountBathrooms">max number of bathrooms to return</param>
        /// <returns></returns>
        public IEnumerable<Location> Get(double latitude, double longitude, int amountBathrooms=30)
        {
            DataProvider provider = new DataProvider();
            return provider.GetCloseLocations(latitude, longitude, amountBathrooms);

        }

        /// <summary>
        /// Get bathrooms by star rating ordered by closest
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="stars">star rating required</param>
        /// <param name="amountBathrooms"></param>
        /// <returns></returns>
        public IEnumerable<Location> Get(double latitude, double longitude, int stars, int amountBathrooms)
        {
            DataProvider provider = new DataProvider();
            return provider.GetLocationsByStarsOrderedBydistance(latitude, longitude, stars, amountBathrooms);
        }

        /// <summary>
        /// get all bathrooms within a radius (Km) ordered by distance
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="stars"></param>
        /// <param name="radius"></param>
        /// <param name="action">pass 'R' here</param>
        /// <returns></returns>
        public IEnumerable<Location> Get(double latitude, double longitude, int radius, char action)
        {
            DataProvider provider = new DataProvider();
            return provider.GetBathroomsWithinRadius(latitude, longitude, radius);
        }


        // GET: api/Bathroom/5
        /// <summary>
        /// Call this to get a specific bathroom
        /// </summary>
        /// <param name="id">send the bathroom id</param>
        /// <returns></returns>
        public Location Get(int id)
        {
            DataProvider provider = new DataProvider();
            return provider.GetBathroom(id);
        }

        /// <summary>
        /// Get bathrooms within a bounding box having a star rating (android version)
        ///  </summary>
        /// <param name="northEastBoundLatitude"></param>
        /// <param name="northEastBoundLongitude"></param>
        /// <param name="southWestBoundLatitude"></param>
        /// <param name="southWestBoundLongitude"></param>
        /// <param name="stars">star rating required</param>
        /// <param name="amountBathrooms">number of bathrooms with that star rating</param>
        /// <returns></returns>
        public IEnumerable<Location> Get(double northEastBoundLatitude, double northEastBoundLongitude, double southWestBoundLatitude, double southWestBoundLongitude, int stars, int amountBathrooms)
        {
            BathroomsInBoundingBox entity = new BathroomsInBoundingBox
            {
                southWestBoundLongitude = southWestBoundLongitude,
                southWestBoundLatitude = southWestBoundLatitude,
                amountBathrooms = amountBathrooms,
                northEastBoundLongitude = northEastBoundLongitude,
                northEastBoundLatitude = northEastBoundLatitude
            };
            DataProvider provider = new DataProvider();
            return provider.GetAllLocationsWithinBoundingBoxByStars(entity, stars);
        }


        /// <summary>
        /// Get all bathrooms within a bounding box ordered by VOTING rating (web only)
        /// </summary>
        /// <param name="northEastBoundLatitude">Y1</param>
        /// <param name="northEastBoundLongitude">X1</param>
        /// <param name="southWestBoundLatitude">Y2</param>
        /// <param name="southWestBoundLongitude">X2</param>
        /// <param name="amountBathrooms"></param>
        /// <returns></returns>
        public IEnumerable<Location> Get(double northEastBoundLatitude, double northEastBoundLongitude, double southWestBoundLatitude, double southWestBoundLongitude, int amountBathrooms)
        {
            BathroomsInBoundingBox entity = new BathroomsInBoundingBox
            {
                southWestBoundLongitude = southWestBoundLongitude,
                southWestBoundLatitude = southWestBoundLatitude,
                amountBathrooms = amountBathrooms,
                northEastBoundLongitude = northEastBoundLongitude,
                northEastBoundLatitude = northEastBoundLatitude
            };
            DataProvider provider = new DataProvider();
            return provider.GetAllLocationsWithinBoundingBox(entity);
        }

        /// <summary>
        /// Get all bathrooms within a bounding box ordered by rating
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Location> Post([FromBody]BathroomsInBoundingBox entity)
        {
            DataProvider provider = new DataProvider();
            return provider.GetAllLocationsWithinBoundingBox(entity);
        }

        // PUT: api/Bathroom/5
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bathroom/5
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Delete(int id)
        {
        }
    }
}
