using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

using MongoDB.Driver.Builders;
//using MongoDB.Driver.GridFS;
//using MongoDB.Driver.Linq;

//using MongoDB.Bson.IO;
//using MongoDB.Bson.Serialization;
//using MongoDB.Bson.Serialization.Attributes;
//using MongoDB.Bson.Serialization.Conventions;
//using MongoDB.Bson.Serialization.IdGenerators;
//using MongoDB.Bson.Serialization.Options;
//using MongoDB.Bson.Serialization.Serializers;
//using MongoDB.Driver.Wrappers;

public class Score {
	public int UserID { get; set; }
	public int Value { get; set; }
}

public class User {
	public int UserID { get; set; }
	public string Name { get; set; }
	public int HighScore { get; set; }
}

//public class User {
//	public int UserID { get; set; }
//	public string Name { get; set; }
//	public Score HighScore { get; set; }
//	private string PasswordHash { get; set; }
//	private bool CheckPassword(string password) {}
//}

public class Backend : MonoBehaviour {

	public bool ConnectedToDB = false;
	public bool BackendActive = true;
	
	private MongoClient mongoClient;
	private MongoServer mongoServer;
	private MongoDatabase database;
	private MongoCollection<User> users;
	private MongoCollection<Score> scores;

	public List<User> GetTop(int count)
	{
		List<User> result = new List<User>();
		int userCount;
		try
		{
			var sortBy = SortBy.Descending("Value");
			if(count<1) throw new Exception();
			MongoCursor<User> topUsers = users.FindAll().SetSortOrder(sortBy).SetLimit(count);
			foreach(User u in topUsers)
			{
				result.Add(u);
				++userCount;
			}
		}
		catch(Exception e)
		{
			return null;
		}
		return result;
	}

	public void SubmitScore(string userName, int scoreValue)
	{
		try
		{
			var query = Query.EQ("Name", userName);
			User u = users.FindOne(query);
			if(u == null)
			{
				User newUser = CreateUser(userName, scoreValue);
				users.Insert(newUser);
				SubmitScore(newUser.UserID, scoreValue);
			}
			else
			{
				SubmitScore(u.UserID, scoreValue);
			}
			//int count = 0;
			//foreach(User u in users.Find(query))
			//{
			//	SubmitScore(u.UserID, scoreValue);
			//	++count;
			//}
			//if(count == 0) 
			//{
			//	User u = CreateUser(userName, scoreValue);
			//	users.Insert(u);
			//	SubmitScore(u.UserID, scoreValue);
			//}
		}
		catch(Exception e)
		{
			return;
		}
		return;
	}

	public void SubmitScore(int userID, int scoreValue)
	{
		try
		{
			Score scoreObject = new Score {
				UserID = userID,
				Value = scoreValue
			};
			scores.Insert(scoreObject);
			var query = Query.EQ("UserID", userID);
			User u = users.FindOne(query);
			if(u.HighScore < scoreValue)
			{
				var update = Update.Set("HighScore", scoreValue);
				var sort = SortBy.Descending("UserID");
				users.FindAndModify(query, sort, update);
			}
		}
		catch(Exception e)
		{
			return;
		}
		return;
	}



	private int GetFreeUserID()
	{
		int newID = 0;
		bool quit = false;

		while(quit == false)
		{
			var query = Query.EQ("UserID", newID);
			User u = users.FindOne(query);
			if(u == null)
			{
				quit = true;
				return newID;
			}
			else
			{
				++newID;
			}
		}
		
		return -1;
	}

	
	private User CreateUser(string userName)
	{
		return CreateUser(userName, 0);
	}

	private User CreateUser(string userName, int score)
	{
		int freeID = GetFreeUserID();
		User u = new User {
			UserID = freeID,
			Name = userName,
			HighScore = score
		};
		return u;
	}

	private void InitScores()
	{
		database.CreateCollection("scores");
	}

	private void InitUsers()
	{
		database.CreateCollection("users");
	}

	private void Init()
	{
		BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;	// ???
		mongoClient = new MongoClient("mongodb://127.0.0.1");
		mongoServer = mongoClient.GetServer();
		mongoServer.Connect();
		database = mongoServer.GetDatabase("local");
		if(database.CollectionExists("scores") == false) InitScores();
		if(database.CollectionExists("users") == false) InitUsers();
		scores = database.GetCollection<Score>("scores");
		users = database.GetCollection<User>("users");
		// Debug.Log(database.CollectionExists("scores").ToString());
	}

	void Start () {
		Init();
	}
	
}
