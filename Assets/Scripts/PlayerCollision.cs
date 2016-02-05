using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	//public variables
	public static bool canTakeDamage;
	public GameObject explosion;

	//private variables
	private int spawnCount;
	private GameController gameController;
	private AudioSource audioSource;
	private PlayerController playerController;

	void Start(){

		spawnCount = 0;
		audioSource = GetComponent<AudioSource> ();
		playerController = gameObject.GetComponent<PlayerController> ();

		GameObject gameControllerObject = GameObject.Find ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}


	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "Bullet" ) {
			return;
		}

		GameObject[] bullet = GameObject.FindGameObjectsWithTag("Bullet");
		foreach (GameObject target in bullet) {
			GameObject.Destroy(target);
		}

		if(other.gameObject.tag == "Kill-All"){

			PlayAudio();

			GameObject[] spawn = GameObject.FindGameObjectsWithTag("Spawn");
			foreach (GameObject target in spawn) {
				spawnCount++;
				GameObject.Destroy(target);
			}

			GameObject[] spawnBullet = GameObject.FindGameObjectsWithTag("SpawnBullet");
			foreach (GameObject target in spawnBullet) {
				GameObject.Destroy(target);
			}

			GameObject[] asteroid = GameObject.FindGameObjectsWithTag("Asteroid");
			foreach (GameObject target in asteroid) {
				GameObject.Destroy(target);
			}

			gameController.AddScore (spawnCount * 10);

			Destroy (other.gameObject);
			return;
		}

		if(other.gameObject.tag == "Life-Up"){

			if (GameController.CheckPlayerLife () < 3) {
				PlayAudio ();
				gameController.IncreaseLife();
			}

			Destroy (other.gameObject);
			return;
		}
			
		if(other.gameObject.tag == "Sheild-Up"){
			PlayAudio();
			StartCoroutine(playerController.Flash (5F));
			Destroy (other.gameObject);
			return;
		}
			
		if (canTakeDamage) {

			GameObject newexplosion = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
			Destroy(newexplosion, 1);

			gameController.DecreaseLife ();

			Destroy (this.gameObject);
			Destroy (other.gameObject);
		}
	}

	private void PlayAudio(){
		audioSource.Play ();
	}


}