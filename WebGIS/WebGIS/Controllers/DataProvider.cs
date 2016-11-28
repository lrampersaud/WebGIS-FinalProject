using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Npgsql;
using WebGIS.DataLayer;
using WebGIS.Models;

namespace WebGIS.Controllers
{
    public sealed class DataProvider
    {
        private string _connectionString;
        private NpgsqlConnection conn;
        public DataProvider()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            conn= new NpgsqlConnection(_connectionString);
        }

        ~DataProvider()
        {
            _connectionString = null;
            conn = null;
        }




        public List<Location> GetCloseLocations(double lat, double lng, int count)
        {
            string sqlStatement =
                $"select id, name, latitude, longitude, image, borough, openYearRound, handicap, st_distance(ST_GeomFromText('POINT({lat} {lng})',4326), geom) as distance " +
                $"from public.bathrooms " +
                $"order by distance limit {count}";
            List<Location>locations = new List<Location>();
            conn.Open();
            
            NpgsqlDataReader reader = SqlDataQueryEngine.ExecuteReader(conn, sqlStatement);
            while (reader.Read())
            {
                locations.Add(new Location
                {
                    id=Convert.ToInt32(reader[0]),
                    name=reader[1].ToString(),
                    latitude = Convert.ToDouble(reader[2]),
                    longitude = Convert.ToDouble(reader[3]),
                    image=reader[4].ToString(),
                    borough=reader[5].ToString(),
                    openYearRound = Convert.ToBoolean(reader[6]),
                    handicap = Convert.ToBoolean(reader[7]),
                    distance = Convert.ToDouble(reader[8])
                });
            }
            reader.Close();
            conn.Close();
            return locations;
        }



        public Location GetBathroom(int id)
        {
            string sqlStatement =
                $"select id, name, latitude, longitude, image, borough, openYearRound, handicap " +
                $"from public.bathrooms " +
                $"where id={id}";

            Location location = new Location();
            conn.Open();

            NpgsqlDataReader reader = SqlDataQueryEngine.ExecuteReader(conn, sqlStatement);
            if (reader.Read())
            {
                location = new Location
                {
                    id = Convert.ToInt32(reader[0]),
                    name = reader[1].ToString(),
                    latitude = Convert.ToDouble(reader[2]),
                    longitude = Convert.ToDouble(reader[3]),
                    image = reader[4].ToString(),
                    borough = reader[5].ToString(),
                    openYearRound = Convert.ToBoolean(reader[6]),
                    handicap = Convert.ToBoolean(reader[7]),
                    distance = 0d
                };
            }
            reader.Close();

            sqlStatement = $"select id, location_id, description, starrating " +
                           $"from public.ratings " +
                           $"where location_id={0}";
            reader = SqlDataQueryEngine.ExecuteReader(conn, sqlStatement);
            while(reader.Read())
            {
                location.ratingList.Add(new Rating
                {
                    id=Convert.ToInt32(reader[0]),
                    location_id = Convert.ToInt32(reader[1]),
                    description = reader[2].ToString(),
                    starRating = Convert.ToInt32(reader[3])
                });
            }
            reader.Close();
            
            conn.Close();
            
            return location;
        }





    }
}