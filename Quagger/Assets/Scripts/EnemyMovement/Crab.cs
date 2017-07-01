using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Crab : MonoBehaviour
{
    public List<GameObject> waypoints;

    Rigidbody2D rigid;
    public float speed = 1;

    GameObject nextWaypoint;

    int actualIndex = 0;
    bool waiting = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        nextWaypoint = waypoints[1];
    }


    void FixedUpdate()
    {
        float step = speed * Time.deltaTime;
        if (!waiting)
        {
            //float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, step);

            //transform.rotation = Quaternion.Lerp(transform.rotation, nextWaypoint.transform.rotation, step);

            if (transform.position.Equals(nextWaypoint.transform.position))
            {
                waiting = true;
                actualIndex++;
                actualIndex %= waypoints.Count;
                nextWaypoint = waypoints[actualIndex];
                StartCoroutine(crabWait());
            }
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, nextWaypoint.transform.rotation, step);
        }
    }

    IEnumerator crabWait()
    {
        yield return new WaitForSeconds(.5f);
        waiting = false;
    }
}
