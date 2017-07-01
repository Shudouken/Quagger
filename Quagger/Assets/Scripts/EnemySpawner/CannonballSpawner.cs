using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballSpawner : MonoBehaviour {

    public GameObject cannonball;
    public float verticalDirection = 0;
    public float horizontalDirection = 0;

    // Use this for initialization
    void Start () {
        SpawnCannonball();

    }
	
	// Update is called once per frame
	void Update () {
	}

    public void SpawnCannonball()
    {
        Vector3 position = transform.position;
        GameObject ball = Instantiate(cannonball, position, Quaternion.identity);
        ball.GetComponent<Cannonball>().Construct(verticalDirection, horizontalDirection, position);
        ball.transform.parent = gameObject.transform;
    }
}
