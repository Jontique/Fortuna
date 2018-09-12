using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaunchProjectile : MonoBehaviour {

    //  public Rigidbody rb; //nope.avi
    public Transform projectileTransform;

    public Vector3 dragStart;
    public Vector3 dragEnd;
    public Vector3 dragDirection;
    private Vector3 launchVector;
    public float distance;

    public float launchPower = 7f; //INCREASE THIS TO LAUNCH FASTER
    public float maxLaunchSpeed = 37f; //LIMITS THE MAXIMUM LAUNCH SPEEDs


    private float gravity;
    private Vector3 velocity;

    private bool isLaunched;  //temporary debug variable


    private Rigidbody rb;

  

    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        projectileTransform = gameObject.GetComponent<Transform>();
       // gravity = (-2) / Mathf.Pow(gravityWeakness, 2);

    }

    private void Update()
    {
        if(isLaunched)
        {
           // Move();
            //print(velocity);
            Deburgeri();
        }

    }


    private void OnMouseDown()
    {
        // print("clicked the projectile");
        dragStart = new Vector3(transform.position.x, transform.position.y, 0);
        /*dragStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z);
        dragStart = Camera.main.ScreenToWorldPoint(dragStart);*/
        Vector3 test = Input.mousePosition;
        test.z = transform.position.z - Camera.main.transform.position.z;
        test = Camera.main.ScreenToWorldPoint(test);



        //print(test);

    }

    private void OnMouseDrag()
    {
        //Debug.DrawLine(dragStart, Camera.main.ScreenToViewportPoint(Input.mousePosition), Color.green, 2f);
        Vector3 posVec = Input.mousePosition;
        posVec.z = transform.position.z - Camera.main.transform.position.z;
        posVec = Camera.main.ScreenToWorldPoint(posVec);
        Debug.DrawLine(dragStart, posVec, Color.green);

    }

    private void OnMouseUp()
    {
        Vector3 dragEnd = Input.mousePosition;
        dragEnd.z = transform.position.z - Camera.main.transform.position.z;
        dragEnd = Camera.main.ScreenToWorldPoint(dragEnd);
        Debug.DrawLine(dragStart, dragEnd, Color.red, 3f);

        CalculateForceAndDirection(dragStart, dragEnd);
    }


    private void CalculateForceAndDirection(Vector3 start, Vector3 end) {

        //print("positions, start = " + start + ", end = " + end);

        //Distance between two points 
        distance = (Mathf.Sqrt(Mathf.Pow((end.x - start.x), 2f) + (Mathf.Pow((end.y - start.y), 2f))));

        distance = distance * launchPower;
        if(distance > maxLaunchSpeed)
        {
            distance = maxLaunchSpeed;
        }


       //print("dist =" + distance); /*****Uncomment to find out launch power*****/



        //Direction of the drag vector
        dragDirection = (end - start).normalized;
        
        launchVector = new Vector3(dragDirection.x, dragDirection.y, 0);



        if(distance > 3f) //allows players to cancel a shot
        {
            LaunchPlayer(launchVector, distance);
            distance = 0f;
            print("distance zeroed");
            dragDirection = Vector3.zero;
            launchVector = Vector3.zero;
        }
        else
        {
            return;
        }
    }
    

    private void LaunchPlayer(Vector3 dir, float power)
    {
        isLaunched = true;
        //power = launchPower * power;
        //velocity.x += (-dir.x * power); X velocity not used atm

     //   Vector3 vel = rb.velocity;
        rb.velocity = new Vector3(rb.velocity.x, (rb.velocity.y -dir.y * power), rb.velocity.z);
    }

    void Deburgeri()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            dragStart = Vector3.zero;
            dragEnd = Vector3.zero;
            velocity.y = 0f;

            isLaunched = false;
            velocity = Vector3.zero;
            transform.position = new Vector3(0, 1, 0);
        }
    }



}