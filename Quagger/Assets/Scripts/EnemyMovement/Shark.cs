using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Shark : MonoBehaviour {

    public List<GameObject> waypoints;

    float speed = 5;

    GameObject nextWaypoint;

    int actualIndex = 0;

    private void Awake()
    {
        nextWaypoint = waypoints[1];
    }
   

    void FixedUpdate()
    {

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, step);

        transform.rotation = Quaternion.Lerp(transform.rotation, nextWaypoint.transform.rotation, step);
        
        if (transform.position.Equals(nextWaypoint.transform.position))
        {
            actualIndex++;
            actualIndex %= 8;
            nextWaypoint = waypoints[actualIndex];
        }

    }

}
