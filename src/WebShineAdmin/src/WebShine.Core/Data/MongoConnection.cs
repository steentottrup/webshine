using System;
using System.Configuration;
using MongoDB.Driver;

namespace WebShine.Core.Data {

	public class MongoConnection {
		protected readonly MongoDatabase database;

		public MongoConnection(String connectionString) {
			MongoUrl url = MongoUrl.Create(connectionString);
			String databaseName = url.DatabaseName;
			MongoClient client = new MongoClient(url);
			MongoServer server = client.GetServer();
			this.database = server.GetDatabase(databaseName);
		}

		public MongoCollection<T> GetCollection<T>() {
			return database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
		}

		public MongoCollection GetCollection(String name) {
			return database.GetCollection(name);
		}

		public static MongoConnection Create() {
			return Create("Main.MongoDB");
		}

		public static MongoConnection Create(String name) {
			return new MongoConnection(ConfigurationManager.ConnectionStrings[name].ConnectionString);
		}
	}
}
