using UnityEngine;
using System.Collections;

public class BossManager : MonoBehaviour {

	public GameObject explosion;

	private GameController gameController;
	private int damage;


	void Start (){
		damage = 1;
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

		if (other.gameObject.tag == "BossBullet") {
			return;
		}

		if(other.gameObject.tag == "Kill-All" || other.gameObject.tag == "Life-Up" || other.gameObject.tag == "Sheild-Up"){
			return;
		}

			gameController.BossLife (damage);
			GetComponent <AudioSource> ().Play ();
			Destroy (other.gameObject);

		if (gameController.GetBossLife () <= 0) {
			Destroy (this.gameObject);

			GameObject newexplosion = (GameObject)Instantiate (explosion, transform.position, transform.rotation);
			Destroy (newexplosion, 1);

			gameController.AddScore (100);

		}

		
	}
		
}