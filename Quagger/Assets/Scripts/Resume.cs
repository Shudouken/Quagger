using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour {
	void Update () {
        if (Input.GetButtonUp("Submit"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Stage"));

            PlayerPrefs.SetInt("ContinueCount", PlayerPrefs.GetInt("ContinueCount")+1);
        }
	}
}
