using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {
    
    public GameObject fish;
    public float speed = 1;
    public int fishCount = 1;
    public float timeBetweenSpawn = 0.2f;
    public bool facingLeft = true;

    bool spawning = false;

    public List<GameObject> fishOutOfBoard = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(SpawnFish(UnityEngine.Random.Range(0f, 2f)));
        //SpawnFish();
        if (!facingLeft)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(SpawnFish());

        }


       // StartCoroutine(SpawnFish(UnityEngine.Random.Range(0f, 2f)));
    }
    
    IEnumerator SpawnFish()
    {
        int count = 0;
        while(count < fishCount)
        {
            GameObject fishObject;
            if (fishOutOfBoard.Count != 0)
            {
                fishObject = fishOutOfBoard[0];
                fishObject.transform.position = transform.position;
                fishOutOfBoard.RemoveAt(0);
            }
            else
            {
                fishObject = Instantiate(fish, transform.position, transform.rotation);
                fishObject.GetComponent<Fish>().Construct(speed, transform.position, this, facingLeft);
                fishObject.transform.parent = gameObject.transform;
                
                if (!facingLeft)
                {
                    Vector3 scale = new Vector3(fishObject.transform.localScale.x, -fishObject.transform.localScale.y, fishObject.transform.localScale.z);
                    fishObject.transform.localScale = scale;
                }
            }
            count++;
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
        yield return new WaitForSeconds(1.5f * timeBetweenSpawn);
        spawning = false;
    }
}
