using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

public class Backend : MonoBehaviour {

	public bool ConnectedToDB = false;
	public bool BackendActive = true;
	
	private MongoClient client;
	private MongoServer server;
	private MongoDatabase db;
	private MongoCollection<Order> orders;
	
	void Start () {
		
	}
	
	void Update () {
		
	}
}
