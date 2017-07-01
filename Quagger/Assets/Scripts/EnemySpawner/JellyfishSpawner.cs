using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishSpawner : MonoBehaviour {


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

        startingFishSpawn();
    }

    void startingFishSpawn()
    {
        int count = 0;
        while (count < fishCount)
        {
            Vector3 position;
            float factor = 10 * (count-1) + UnityEngine.Random.Range(0,4);

            if (!facingLeft)
            {
                if (count == 0 && transform.position.x + factor >= 20)
                {
                    break;
                }
                position = new Vector3(transform.position.x + factor, transform.position.y, transform.position.z);
            }
            else
            {
                if (count == 0 && transform.position.x - factor <= -20)
                {
                    break;
                }
                position = new Vector3(transform.position.x - factor, transform.position.y, transform.position.z);
            }
            /*
            GameObject fishObject = Instantiate(fish, position, transform.rotation);
                
            if (fishObject.GetComponent<Jellyfish>() != null)
            {
                fishObject.GetComponent<Jellyfish>().Construct(speed+ UnityEngine.Random.Range(0, 1), transform.position, this, facingLeft);
            }

            fishObject.transform.parent = gameObject.transform;

            if (!facingLeft)
            {
                Vector3 scale = new Vector3(fishObject.transform.localScale.x, -fishObject.transform.localScale.y, fishObject.transform.localScale.z);
                fishObject.transform.localScale = scale;
            }*/
            StartCoroutine(SpawnRandom(position));
            count++;
        }

    }

    IEnumerator SpawnRandom(Vector3 position)
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f,1f));
        GameObject fishObject = Instantiate(fish, position, transform.rotation);

        if (fishObject.GetComponent<Jellyfish>() != null)
        {
            fishObject.GetComponent<Jellyfish>().Construct(speed + UnityEngine.Random.Range(0, 1), transform.position, this, facingLeft);
        }

        fishObject.transform.parent = gameObject.transform;

        if (!facingLeft)
        {
            Vector3 scale = new Vector3(fishObject.transform.localScale.x, -fishObject.transform.localScale.y, fishObject.transform.localScale.z);
            fishObject.transform.localScale = scale;
        }
    }
    void Update()
    {/*
        if (spawning)
        {
            spawning = true;
            StartCoroutine(SpawnFish());

        }*/
    }
    /*
    IEnumerator SpawnFish()
    {
        yield return new WaitForSeconds(timeBetweenSpawn);
        int count = 0;
        while (count < fishCount)
        {
            GameObject fishObject;
            if (fishOutOfBoard.Count != 0)
            {
                fishObject = fishOutOfBoard[0];
                fishObject.SetActive(true);
                fishObject.transform.position = transform.position;
                fishOutOfBoard.RemoveAt(0);
            }
            else
            {
                fishObject = Instantiate(fish, transform.position, transform.rotation);
                if (fishObject.GetComponent<Jellyfish>() != null)
                {
                    fishObject.GetComponent<Jellyfish>().Construct(speed, transform.position, this, facingLeft);
                }
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
    }*/

    IEnumerator SpawnFish()
    {
        yield return new WaitForSeconds(timeBetweenSpawn);
        int count = 0;
        while (count < fishCount)
        {
            GameObject fishObject;
            if (fishOutOfBoard.Count != 0)
            {
                fishObject = fishOutOfBoard[0];
                fishObject.SetActive(true);
                fishObject.transform.position = transform.position;
                fishOutOfBoard.RemoveAt(0);
            }
            else
            {
                fishObject = Instantiate(fish, transform.position, transform.rotation);
                if (fishObject.GetComponent<Jellyfish>() != null)
                {
                    fishObject.GetComponent<Jellyfish>().Construct(speed, transform.position, this, facingLeft);
                }
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
