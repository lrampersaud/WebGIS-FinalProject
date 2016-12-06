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
            conn = new NpgsqlConnection(_connectionString);
        }

        ~DataProvider()
        {
            _connectionString = null;
            conn = null;
        }




        public List<Location> GetCloseLocations(double lat, double lng, int count)
        {
            string sqlStatement =
                $"select id, name, latitude, longitude, image, borough, openYearRound, handicap, " +
                $"st_distance(ST_GeomFromText('POINT({lng} {lat})',2263), geom) as distance, upVote, downVote, location " +
                $"from public.bathrooms " +
                $"order by distance limit {count}";
            List<Location> locations = new List<Location>();
            conn.Open();

            NpgsqlDataReader reader = SqlDataQueryEngine.ExecuteReader(conn, sqlStatement);
            while (reader.Read())
            {
                locations.Add(new Location
                {
                    id = Convert.ToInt32(reader[0]),
                    name = reader[1].ToString(),
                    latitude = Convert.ToDouble(reader[2]),
                    longitude = Convert.ToDouble(reader[3]),
                    image = reader[4].ToString(),
                    borough = reader[5].ToString(),
                    openYearRound = Convert.ToBoolean(reader[6]),
                    handicap = Convert.ToBoolean(reader[7]),
                    distance = Convert.ToDouble(reader[8]),
                    upVotes = Convert.ToInt32(reader[9]),
                    downVotes = Convert.ToInt32(reader[10]),
                    location = reader[11].ToString()
                });
            }
            reader.Close();
            conn.Close();
            return locations;
        }

        public List<Location> GetAllLocations()
        {
            string sqlStatement =
                $"select id, name, latitude, longitude, image, borough, openYearRound, handicap, 0 distance, upVote, downVote, location " +
                $"from public.bathrooms";
            List<Location> locations = new List<Location>();
            conn.Open();

            NpgsqlDataReader reader = SqlDataQueryEngine.ExecuteReader(conn, sqlStatement);
            while (reader.Read())
            {
                locations.Add(new Location
                {
                    id = Convert.ToInt32(reader[0]),
                    name = reader[1].ToString(),
                    latitude = Convert.ToDouble(reader[2]),
                    longitude = Convert.ToDouble(reader[3]),
                    image = reader[4].ToString(),
                    borough = reader[5].ToString(),
                    openYearRound = Convert.ToBoolean(reader[6]),
                    handicap = Convert.ToBoolean(reader[7]),
                    distance = Convert.ToDouble(reader[8]),
                    upVotes = Convert.ToInt32(reader[9]),
                    downVotes = Convert.ToInt32(reader[10]),
                    location = reader[11].ToString()
                });
            }
            reader.Close();
            conn.Close();
            return locations;
        }



        public Location GetBathroom(int id)
        {
            string sqlStatement =
                $"select id, name, latitude, longitude, image, borough, openYearRound, handicap, upVote, downVote, location " +
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
                    distance = 0d,
                    upVotes = Convert.ToInt32(reader[8]),
                    downVotes = Convert.ToInt32(reader[9]),
                    location = reader[10].ToString()
                };
            }
            reader.Close();

            sqlStatement = $"select id, location_id, description, starrating " +
                           $"from public.ratings " +
                           $"where location_id={0}";
            reader = SqlDataQueryEngine.ExecuteReader(conn, sqlStatement);
            while (reader.Read())
            {
                location.ratingList.Add(new Rating
                {
                    id = Convert.ToInt32(reader[0]),
                    location_id = Convert.ToInt32(reader[1]),
                    description = reader[2].ToString(),
                    starRating = Convert.ToInt32(reader[3])
                });
            }
            reader.Close();

            conn.Close();

            return location;
        }


        public void Vote(int location_id, bool upVote)
        {
            string sqlStatement =
                $"update public.bathrooms " +
                $"set upvote=coalesce(upvote, 0) + 1 " +
                $"where id={location_id}";

            if (!upVote)
            {
                sqlStatement =
                    $"update public.bathrooms " +
                    $"set downvote=coalesce(downvote, 0) + 1 " +
                    $"where id={location_id}";
            }
            conn.Open();

            SqlDataQueryEngine.ExecuteNonQuery(conn, sqlStatement);

            conn.Close();
        }


        public List<Rating> GetAllRatingsForBathroom(int id)
        {
            List<Rating> ratings = new List<Rating>();
            string sqlStatement = $"select id, location_id, description, starrating " +
                                  $"from public.ratings " +
                                  $"where location_id={0}";
            NpgsqlDataReader reader = SqlDataQueryEngine.ExecuteReader(conn, sqlStatement);
            while (reader.Read())
            {
                ratings.Add(new Rating
                {
                    id = Convert.ToInt32(reader[0]),
                    location_id = Convert.ToInt32(reader[1]),
                    description = reader[2].ToString(),
                    starRating = Convert.ToInt32(reader[3])
                });
            }
            reader.Close();

            conn.Close();
            return ratings;
        }

        public Rating GetRating(int id)
        {
            Rating rating = new Rating();
            string sqlStatement = $"select id, location_id, description, starrating " +
                                  $"from public.ratings " +
                                  $"where id={0}";
            NpgsqlDataReader reader = SqlDataQueryEngine.ExecuteReader(conn, sqlStatement);
            while (reader.Read())
            {
                rating = new Rating
                {
                    id = Convert.ToInt32(reader[0]),
                    location_id = Convert.ToInt32(reader[1]),
                    description = reader[2].ToString(),
                    starRating = Convert.ToInt32(reader[3])
                };
            }
            reader.Close();

            conn.Close();
            return rating;
        }


        public bool InsertRating(Rating rating)
        {

            try
            {
                conn.Open();
                string sqlStatement = $"insert into public.ratings(location_id, description, starrating) " +
                                      $"values({rating.location_id}, '{rating.description.Replace("'", "''")}', {rating.starRating})";
                SqlDataQueryEngine.ExecuteNonQuery(conn, sqlStatement);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }


            return true;
        }

        public List<Location> GetAllLocationsWithinBoundingBox(BathroomsInBoundingBox entity)
        {
            string sqlStatement =
                $"select id, name, latitude, longitude, image, borough, openYearRound, handicap, 0 distance, upVote, downVote, location " +
                $"from public.bathrooms " +
                $"where st_contains(ST_GeomFromText('POLYGON(({entity.northEastBoundLongitude} {entity.northEastBoundLatitude}," +
                $"{entity.northEastBoundLongitude} {entity.southWestBoundLatitude}," +
                $"{entity.southWestBoundLongitude} {entity.northEastBoundLatitude}," +
                $"{entity.southWestBoundLongitude} {entity.southWestBoundLatitude}," +
                $"{entity.northEastBoundLongitude} {entity.northEastBoundLatitude}))',2263), geom)";
            List<Location> locations = new List<Location>();
            conn.Open();

            NpgsqlDataReader reader = SqlDataQueryEngine.ExecuteReader(conn, sqlStatement);
            while (reader.Read())
            {
                locations.Add(new Location
                {
                    id = Convert.ToInt32(reader[0]),
                    name = reader[1].ToString(),
                    latitude = Convert.ToDouble(reader[2]),
                    longitude = Convert.ToDouble(reader[3]),
                    image = reader[4].ToString(),
                    borough = reader[5].ToString(),
                    openYearRound = Convert.ToBoolean(reader[6]),
                    handicap = Convert.ToBoolean(reader[7]),
                    distance = Convert.ToDouble(reader[8]),
                    upVotes = Convert.ToInt32(reader[9]),
                    downVotes = Convert.ToInt32(reader[10]),
                    location = reader[11].ToString()
                });
            }
            reader.Close();
            conn.Close();
            locations = locations.OrderBy(p => p.upVotes - p.downVotes).Take(entity.amountBathrooms).ToList();

            return locations;
        }

        public List<Location> GetAllLocationsWithinBoundingBoxByStars(BathroomsInBoundingBox entity, int stars)
        {
            string sqlStatement =
                $"select id, name, latitude, longitude, image, borough, openYearRound, handicap, 0 distance, upVote, downVote, location, coalesce((select avg(starrating) from ratings where location_id=bath.id),0) as stars " +
                $"from public.bathrooms bath " +
                $"where round(coalesce((select avg(starrating) from ratings where location_id=bath.id),0))={stars} and " +
                $"st_contains(ST_GeomFromText('POLYGON(({entity.northEastBoundLongitude} {entity.northEastBoundLatitude}," +
                $"{entity.northEastBoundLongitude} {entity.southWestBoundLatitude}," +
                $"{entity.southWestBoundLongitude} {entity.northEastBoundLatitude}," +
                $"{entity.southWestBoundLongitude} {entity.southWestBoundLatitude}," +
                $"{entity.northEastBoundLongitude} {entity.northEastBoundLatitude}))',2263), geom) " +
                $"order by stars" +
                $"limit {entity.amountBathrooms}";
            List<Location> locations = new List<Location>();
            conn.Open();

            NpgsqlDataReader reader = SqlDataQueryEngine.ExecuteReader(conn, sqlStatement);
            while (reader.Read())
            {
                locations.Add(new Location
                {
                    id = Convert.ToInt32(reader[0]),
                    name = reader[1].ToString(),
                    latitude = Convert.ToDouble(reader[2]),
                    longitude = Convert.ToDouble(reader[3]),
                    image = reader[4].ToString(),
                    borough = reader[5].ToString(),
                    openYearRound = Convert.ToBoolean(reader[6]),
                    handicap = Convert.ToBoolean(reader[7]),
                    distance = Convert.ToDouble(reader[8]),
                    upVotes = Convert.ToInt32(reader[9]),
                    downVotes = Convert.ToInt32(reader[10]),
                    location = reader[11].ToString(),
                    rating = Convert.ToDouble(reader[12])
                });
            }
            reader.Close();
            conn.Close();

            return locations;
        }

        public List<Location> GetLocationsByStarsOrderedBydistance(double latitude, double longitude, int stars, int amountBathrooms)
        {
            string sqlStatement =
                $"select id, name, latitude, longitude, image, borough, openYearRound, handicap, st_distance(ST_GeomFromText('POINT({longitude} {latitude})',2263), geom) as distance, upVote, downVote, location " +
                $"from public.bathrooms " +
                $"where round(coalesce((select avg(starrating) from ratings where location_id=bath.id),0))={stars} " +
                $"order by distance" +
                $"limit {amountBathrooms}";
            List<Location> locations = new List<Location>();
            conn.Open();

            NpgsqlDataReader reader = SqlDataQueryEngine.ExecuteReader(conn, sqlStatement);
            while (reader.Read())
            {
                locations.Add(new Location
                {
                    id = Convert.ToInt32(reader[0]),
                    name = reader[1].ToString(),
                    latitude = Convert.ToDouble(reader[2]),
                    longitude = Convert.ToDouble(reader[3]),
                    image = reader[4].ToString(),
                    borough = reader[5].ToString(),
                    openYearRound = Convert.ToBoolean(reader[6]),
                    handicap = Convert.ToBoolean(reader[7]),
                    distance = Convert.ToDouble(reader[8]),
                    upVotes = Convert.ToInt32(reader[9]),
                    downVotes = Convert.ToInt32(reader[10]),
                    location = reader[11].ToString()
                });
            }
            reader.Close();
            conn.Close();
            return locations;
        }

        public List<Location> GetBathroomsWithinRadius(double lat, double lng, int radius)
        {
            string sqlStatement =
                $"select id, name, latitude, longitude, image, borough, openYearRound, handicap, " +
                $"st_distance(ST_GeomFromText('POINT({lng} {lat})',2263), geom) as distance, upVote, downVote, location " +
                $"from public.bathrooms " +
                $"where st_contains(geom, st_buffer(ST_GeomFromText('POINT({lng} {lat})',2263)))" +
                $"order by distance";
            List<Location> locations = new List<Location>();
            conn.Open();

            NpgsqlDataReader reader = SqlDataQueryEngine.ExecuteReader(conn, sqlStatement);
            while (reader.Read())
            {
                locations.Add(new Location
                {
                    id = Convert.ToInt32(reader[0]),
                    name = reader[1].ToString(),
                    latitude = Convert.ToDouble(reader[2]),
                    longitude = Convert.ToDouble(reader[3]),
                    image = reader[4].ToString(),
                    borough = reader[5].ToString(),
                    openYearRound = Convert.ToBoolean(reader[6]),
                    handicap = Convert.ToBoolean(reader[7]),
                    distance = Convert.ToDouble(reader[8]),
                    upVotes = Convert.ToInt32(reader[9]),
                    downVotes = Convert.ToInt32(reader[10]),
                    location = reader[11].ToString()
                });
            }
            reader.Close();
            conn.Close();
            return locations;
        }

    }
}