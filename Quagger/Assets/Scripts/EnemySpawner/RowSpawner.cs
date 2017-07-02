using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowSpawner : MonoBehaviour {

    public GameObject fish;
    public float speed = 0.1f;

    public int fishCount = 1;
    public float distanceBetweenFish = 1f;
    public float distanceBetweenGroup = 1f;
    
    public bool facingLeft = true;

    public List<GameObject> fishList = new List<GameObject>();
    public List<GameObject> fishOutOfBoard = new List<GameObject>();

    float deadzone;
    GameObject nearestFish;

    // Use this for initialization
    void Start()
    {
        deadzone = facingLeft ? -20 : 20;
        if (!facingLeft)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }

        startingFishSpawn();
    }

    void startingFishSpawn()
    {
        Vector3 position = transform.position;
        //Debug.Log("fishSpawn: " + position);
        bool first = true;

        while((facingLeft && position.x >= deadzone) ||
            (!facingLeft && position.x <= deadzone))
        {
            int count = 0;
            while (count < fishCount)
            {
                GameObject fishObject = Instantiate(fish, position, transform.rotation);
                fishObject.GetComponent<Fish>().Construct(speed, this, facingLeft);

                fishObject.transform.parent = gameObject.transform;

                if (!facingLeft)
                {
                    Vector3 scale = new Vector3(fishObject.transform.localScale.x, -fishObject.transform.localScale.y, fishObject.transform.localScale.z);
                    fishObject.transform.localScale = scale;
                }
                
                if (first)
                {
                    first = false;
                    nearestFish = fishObject;
                }

                fishList.Add(fishObject);
               // Debug.Log("fishInit: " + nearestFish.transform.position.x);

                count++;

                if(count < fishCount)
                    position.x = facingLeft ? position.x - distanceBetweenFish : position.x + distanceBetweenFish;

            }
            position.x = facingLeft ? position.x - distanceBetweenGroup : position.x + distanceBetweenGroup;
        }

        int loopcount = 0;
        while (loopcount < fishCount)
        {
            GameObject fishObject = Instantiate(fish, transform.position, transform.rotation);
            fishObject.GetComponent<Fish>().Construct(speed, this, facingLeft);

            fishObject.transform.parent = gameObject.transform;

            if (!facingLeft)
            {
                Vector3 scale = new Vector3(fishObject.transform.localScale.x, -fishObject.transform.localScale.y, fishObject.transform.localScale.z);
                fishObject.transform.localScale = scale;
            }
            
            fishOutOfBoard.Add(fishObject);

            loopcount++;
        }
    }

    void Update()
    {
        //Debug.Log("update: " + fishList.Count);
        if (fishOutOfBoard.Count >= fishCount)
        {
            //Debug.Log("fishSpawn: " + nearestFish.transform.position.x);
            if ((facingLeft && nearestFish.transform.position.x <= (transform.position.x - (distanceBetweenGroup)))|| 
                (!facingLeft && nearestFish.transform.position.x >= (transform.position.x + (distanceBetweenGroup))))
            {
                int count = 0;
                
                while (count < fishCount)
                {
                    float factor = facingLeft ? distanceBetweenFish * count : -distanceBetweenFish * count;
                    //Debug.Log("fishWhile: " + count);
                    fishOutOfBoard[0].transform.position = new Vector3(transform.position.x + factor, fishList[count].transform.position.y, fishList[count].transform.position.z);
                    fishOutOfBoard[0].gameObject.SetActive(true);
                    //Debug.Log("fishWhile2: " + count);
                    
                    nearestFish = fishOutOfBoard[0];
                    fishOutOfBoard.RemoveAt(0);
                    count++;

                }
            }
        }
    }

}
