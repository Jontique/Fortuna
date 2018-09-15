using System;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

public class User {
	public int UserID { get; set; }
	public float HighScore { get; set; }
}

public class Score {
	public int UserID { get; set; }
	public float score { get; set; }
}

public class Backend : MonoBehaviour {

	public bool ConnectedToDB = false;
	public bool BackendActive = true;
	
	private MongoDatabase db;
	private MongoCollection<User> users;
	private MongoCollection<Score> scores;

	void Start () {
		
	}
	
	void Update () {
		
	}
}
