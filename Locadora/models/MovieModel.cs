using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using Locadora.classes;
using MongoDB.Driver;

namespace Locadora.models
{
    public class MovieModel
    {
        [BsonId]
        public BsonObjectId Id = ObjectId.GenerateNewId();
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("originaltitle")]
        public string OriginalTitle { get; set; }
        [BsonElement("cast")]
        public CastModel Cast { get; set; }
        [BsonElement("provider")]
        public string Provider { get; set; }
        [BsonElement("release_date")]
        public DateTime ReleaseDate { get; set; }
        [BsonElement("language")]
        public string Language { get; set; }
        [BsonElement("hassubtitle")]
        public bool HasSubtitle { get; set; }
        [BsonElement("screen_format")]
        public string ScreenFormat { get; set; }
        [BsonElement("age_rating")]
        public byte AgeRating { get; set; }
        [BsonElement("rating")]
        public byte Rating { get; set; }
        [BsonElement("img_path")]
        public string ImgPath { get; set; }
        [BsonElement("genre")]
        public string Genre { get; set; }
        [BsonElement("units")]
        public int Units { get; set; }
        [BsonElement("location")]
        public LocationModel Location { get; set; }

        public override string ToString()
        {
            return Title;
        }

        public static void Save(MovieModel movie)
        {
            try
            {
                MongoConnection.moviecollection.InsertOneAsync(movie);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static bool Remove(ObjectId id)
        {
            try
            {
                var filter = Builders<MovieModel>.Filter.Eq(x => x.Id, id);
                MongoConnection.moviecollection.DeleteOne(filter);
                return true;
            }
            catch 
            {

                throw;
            }
        }

        public static bool Replace(MovieModel movie)
        {
            try
            {
                var filter = Builders<MovieModel>.Filter.Eq(x => x.Id, movie.Id);
                MongoConnection.moviecollection.ReplaceOne(filter, movie);
                return true;
            }
            catch 
            {

                throw;
            }
        }
    }
}
