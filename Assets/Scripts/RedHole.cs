using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHole : MonoBehaviour {

    public GameObject activeBall;
    public GameManager gm;
    public BallBehaviour ballbehav;

    private Transform hole;

    private Vector3 stopPos;

    public int score;
    private float maxSpeed = 40f;
    private float rand;



	void Start ()
    {
        if(gm == null)
        {
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        hole = gameObject.transform.GetChild(0).transform;
        stopPos = hole.position;
        activeBall = gm.GetActiveBall();
        ballbehav = activeBall.GetComponent<BallBehaviour>();
        rand = Random.Range(0.1f, 1.0f);

	}
	

    public bool CheckHoleStay(float speed)
    {
        activeBall = gm.GetActiveBall();
        float chance = 1.0f - (speed / maxSpeed);
         if (chance > rand)
             return true;
        print("chance = " + chance + ", rand = " +rand);
        rand = Random.Range(0.1f, 1.0f);
        return false;
    }

}
