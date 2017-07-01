using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Rigidbody2D rigid;
    public float speed = 1;

    private Text timer;
    private Text hearts;

	// Use this for initialization
	void Start () {
        PlayerSingleton.getInstance().reset();
        timer = GameObject.Find("Timer").GetComponent<Text>();
        hearts = GameObject.Find("Hearts").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        PlayerSingleton.getInstance().incrementTimer(Time.deltaTime);
        timer.text = PlayerSingleton.getInstance().displayTime();
        hearts.text = new System.String('l', PlayerSingleton.getInstance().hearts);

        if (PlayerSingleton.getInstance().hearts == 0)
            SceneManager.LoadScene("Gameover");
    }

    private void FixedUpdate()
    {
        Vector2 move = rigid.position;

        if (Input.GetAxis("Horizontal") != 0)
            move.x += Input.GetAxis("Horizontal") * speed;
            
        if (Input.GetAxis("Vertical") != 0)
            move.y += Input.GetAxis("Vertical") * speed;

        rigid.position = move;
    }


}

public class PlayerSingleton
{

    public float time;
    public int hearts;

    private static PlayerSingleton instance;

    public static PlayerSingleton getInstance()
    {
        if (instance == null)
            instance = new PlayerSingleton();
        return instance;
    }

    private PlayerSingleton()
    {
        time = 0.0f;
        hearts = 3;
    }

    public void reset()
    {
        time = 0.0f;
        hearts = 3;
    }

    public void lose1Heart()
    {
        hearts = Mathf.Max(hearts - 1, 0);
    }

    // pass deltaTime here 
    public void incrementTimer(float amount)
    {
        time += amount;
    }

    public string displayTime()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int hundredths = Mathf.FloorToInt((time * 100) % 100);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }

}
