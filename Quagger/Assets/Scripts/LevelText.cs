using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelText : MonoBehaviour {
	void Start () {
        GetComponent<Text>().text = SceneManager.GetActiveScene().name;
    }
}
