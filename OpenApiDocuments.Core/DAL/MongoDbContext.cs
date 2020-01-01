using OpenApiDocuments.Core.Attributes;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using MongoDB.Driver.GridFS;

namespace OpenApiDocuments.Core.DAL
{
    /// <summary>
    /// Représente le DbContext permettant d'utiliser des données d'entités dans la base de donnée
    /// </summary>
    /// 
    public class MongoDbContext
    {
        private IMongoClient _client;
        public IMongoDatabase _db;
        private IGridFSBucket _bucket;

        public MongoDbContext(IConfiguration config)
        {
            string connectionString = config.GetConnectionString("Repository");
            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase(new MongoUrl(connectionString).DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _db.GetCollection<T>(((BsonCollectionAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(BsonCollectionAttribute)))?.CollectionName ?? typeof(T).Name.ToLower());
        }

        public IGridFSBucket GetBucket()
        {
            if (_bucket == null)
                _bucket = new GridFSBucket(_db, new GridFSBucketOptions
                {
                    BucketName = "documents",
                    ChunkSizeBytes = 1048576, // 1MB
                    WriteConcern = WriteConcern.WMajority,
                    ReadPreference = ReadPreference.Secondary
                });

            return _bucket;
        }


    }
}
