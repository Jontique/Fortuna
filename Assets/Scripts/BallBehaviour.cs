﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    //whitespace simulator 2018 aka variables
    private LaunchBall launcher;


    private Vector3 clickPos;
    public Vector3 releasePos;
    public Vector3 tempPos;

    public Rigidbody rb;

    public GameObject aimMarker;
    private GameObject dot;
    public GameManager gm;
   // private SphereCollider sphereCol;


    private bool canLaunch = false;
    private bool mouseHeldDown = false;
    public bool collidedToTop = false;
    private bool mouseHasBeenDragged = false;
    private bool ballOutOfSpawn = false;
    private bool failedLaunch = false;


    public float ballMaxSpeed = 25f; //TODO: adjust to fit the game physics
    [SerializeField]
    private float launchPowerMultiplier;

    void Start()
    {
        launcher = gameObject.GetComponent<LaunchBall>();
        rb = gameObject.GetComponent<Rigidbody>();
        //sphereCol = gameObject.GetComponent<SphereCollider>();
        mouseHeldDown = false;
        if(!gm)
        {
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }


    void Update()
    {
        if (CheckIfStopped()) //if ball stopped
        {
            if (ballOutOfSpawn)
            {
                //print("ball out");
               //launch = true;
                gm.NextBall();
                ZeroPositions();
            }
            else
            {
                if (failedLaunch)
                {
                    //print("failed launch");
                    canLaunch = true;
                }
            }
        }


        if (ballOutOfSpawn)
        {
            LimitBallSpeed(rb.velocity.x, rb.velocity.y);
        }


        if(canLaunch)
        {
            if ((CheckIfStopped() == true) && gm.activeBall < 5 && !ballOutOfSpawn)
            {
                if (Input.GetMouseButtonDown(0)) //mouse (or touch) click
                {

                    Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));
                    clickPos = new Vector3(ray.origin.x, ray.origin.y, 0);
                    if (!mouseHeldDown)
                    {
                        //print("mouse click at " + clickPos);
                        dot = Instantiate(aimMarker, new Vector3(clickPos.x, clickPos.y, 0), Quaternion.identity);
                    }
                    mouseHeldDown = true;
                }
                if (Input.GetMouseButton(0)) //mouse hold & drag, returns the current mouse position in real time for the launch aim line whateverthingymagic
                {

                    Ray tempRay = (Camera.main.ScreenPointToRay(Input.mousePosition));
                    tempPos = new Vector3(tempRay.origin.x, tempRay.origin.y, 0);

                    float deltaY = tempPos.y - clickPos.y;
                    float deltaX = tempPos.x - clickPos.x;

                    if (tempPos != clickPos)
                    {
                        mouseHasBeenDragged = true; //ball attempted to launch
                    }

                    Vector3 tempSpot = new Vector3(((transform.position.x - deltaX)), (transform.position.y - deltaY), 0);
                    mouseHeldDown = false;
                }
                if (Input.GetMouseButtonUp(0)) //mouse release
                {
                    Ray dragRay = (Camera.main.ScreenPointToRay(Input.mousePosition));
                    releasePos = new Vector3(dragRay.origin.x, dragRay.origin.y, 0);


                    launcher.Launch(CalculateLaunchPower(clickPos, releasePos));
                    Destroy(dot);
                }

            }

        }

        }


        public void LimitBallSpeed(float vel_x, float vel_y)
    {
        if(vel_x > ballMaxSpeed)
        {
            rb.velocity = new Vector3(ballMaxSpeed, rb.velocity.y);
            //print("x speed limited");
        }

        if (vel_y > ballMaxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, ballMaxSpeed);
           // print("y speed limited");
        }
    }

    public Vector3 CalculateLaunchPower(Vector3 dragStart, Vector3 dragEnd)
    {

        //use fuuuuucking advanced elementary school mathematics to magnify the launch power. you won't believe how advanced this shit is

        float distance = (Mathf.Sqrt(Mathf.Pow((dragEnd.x - dragStart.x), 2f) + (Mathf.Pow((dragEnd.y - dragStart.y), 2f)))); //accepting x 
                                                                                                                                //coordinate input as well

        Vector3 launchVector = new Vector3(0, distance * launchPowerMultiplier, 0); //see I fukkin told ya 8)

        return launchVector;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Box001")
        {
            collidedToTop = true;
        }
        else collidedToTop = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.name == "Box001")
        {
            collidedToTop = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "SpawnBottom")
        {
            ballOutOfSpawn = false;
            failedLaunch = true;
            canLaunch = true;
           // print("bottom enter");

        }
        if (other.name == "LaunchChecker")
        {
            ballOutOfSpawn = false;
            canLaunch = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "SpawnBottom")
        {
            failedLaunch = false;
        }
        if (other.name == "LaunchChecker" && !failedLaunch)
        {
            ballOutOfSpawn = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "LaunchChecker")
        {
            ballOutOfSpawn = false;
            canLaunch = false;
        }

        if (other.name == "SpawnBottom")
        {
            ballOutOfSpawn = false;
            failedLaunch = true;
            canLaunch = true;
           // print("bottom stay");

        }
    }

    public bool CheckIfStopped()
    {
        if(rb.velocity.x <= 0.1f && rb.velocity.y <= 0.1f)
        {
            return true;
        }

        return false;
    }

    public void ZeroPositions() //resets after each shot
    {

        clickPos = Vector3.zero;
        releasePos = Vector3.zero;
        tempPos = Vector3.zero;
        mouseHasBeenDragged = false;
        ballOutOfSpawn = false;
        failedLaunch = false;
       // launch = false;
        if(dot)    //in edge cases reason it can get glitched and remain on the field
            Destroy(dot); 
    }

}
//TODO: add a limit to the length of the line




