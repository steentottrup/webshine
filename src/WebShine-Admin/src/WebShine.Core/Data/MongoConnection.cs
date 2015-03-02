using System;
using System.Configuration;
using MongoDB.Driver;
using Microsoft.Framework.ConfigurationModel;

namespace WebShine.Core.Data {

	public class MongoConnection {
		protected readonly IMongoDatabase database;

		public MongoConnection(IConfiguration config) {
			MongoUrl url = new MongoUrl("");
			this.database =  new MongoClient("").GetDatabase("");
			//MongoUrl url = MongoUrl.Create(connectionString);
			//String databaseName = url.DatabaseName;
			//MongoClient client = new MongoClient(url);
			//MongoServer server = client.GetServer();
			//this.database = server.GetDatabase(databaseName);
		}

		public IMongoCollection<T> GetCollection<T>() {
			return database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
		}

		//public IMongoCollection GetCollection(String name) {
		//	return database.GetCollection(name);
		//}
	}
}
