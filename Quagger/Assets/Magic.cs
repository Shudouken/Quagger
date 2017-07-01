using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour {
    bool active = false;
    float speed = 12;
    GameObject crab;
    // Use this for initialization
    void Start () {
        crab = GameObject.Find("crab");
        crab.GetComponent<Renderer>().enabled = false;
        GameObject.Find("deadcowboy").GetComponent<Renderer>().enabled = false;
        StartCoroutine(magic());
    }
	
	// Update is called once per frame
	void Update () {
        if (active == true)
        {
            float step = speed * Time.deltaTime;
            GameObject.Find("crab").transform.position = Vector3.MoveTowards(GameObject.Find("crab").transform.position, GameObject.Find("cowboy").transform.position, step);
        }
        if(GameObject.Find("crab").transform.position == GameObject.Find("cowboy").transform.position)
        {
            GameObject.Find("cowboy").GetComponent<Renderer>().enabled = false;
            GameObject.Find("deadcowboy").GetComponent<Renderer>().enabled = true;
        }
    }

    IEnumerator magic()
    {
        yield return new WaitForSeconds(2.0f);
        crab.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(1.0f);
        active = true;
    }
}
