using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public GameObject prefab;
    bool spawned = false;
    GameObject wave;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       // Debug.Log("Update: " + spawned);

        if(!spawned && wave == null)
        {
            spawned = true;
            StartCoroutine(SpawnWave());
        }

    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(1f);
        wave = GameObject.Instantiate(prefab, transform.position, transform.rotation);
        print("Spawned");
        spawned = false;
    }
}
