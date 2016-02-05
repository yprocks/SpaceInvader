using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn");
		foreach (GameObject target in spawns) {
			target.gameObject.SetActive (true);
		}

		GameObject[] asteroid = GameObject.FindGameObjectsWithTag("Asteroid");
		foreach (GameObject target in asteroid) {
			target.gameObject.SetActive (true);
		}
			if(Input.GetKeyDown(KeyCode.Space)){
				SceneManager.LoadScene("Level1");
			}
	}
}
