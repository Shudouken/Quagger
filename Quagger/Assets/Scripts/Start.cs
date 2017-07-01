using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour {
	void Update () {
        if (Input.GetButtonUp("Submit"))
        {
            SceneManager.LoadScene("Stage 1");

            PlayerPrefs.SetInt("KillCount", 0);
            PlayerPrefs.SetInt("ContinueCount", 0);
        }

        if (Input.GetButtonUp("Select"))
        {
            if(SceneManager.GetActiveScene().buildIndex == 5)
                SceneManager.LoadScene("Highscore");
            if(SceneManager.GetActiveScene().buildIndex == 0)
                SceneManager.LoadScene("Leaderboard");
        }

	}
}
