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

	}
}
