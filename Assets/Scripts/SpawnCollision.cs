using UnityEngine;
using System.Collections;

public class SpawnCollision : MonoBehaviour {

	public GameObject explosion;	

		void OnTriggerEnter2D(Collider2D other){
		
		if (other.gameObject.tag == "Spawn" || other.gameObject.tag == "Boss") {
			return;
		}

		if (other.gameObject.tag == "Asteroid" ) {


			GameObject newexplosion = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
			Destroy(newexplosion, 1F);

			Destroy (this.gameObject);
			Destroy (other.gameObject);
		}

		if(other.gameObject.tag == "Player"){
			if (PlayerCollision.canTakeDamage) {
				GameObject newexplosion = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
				Destroy(newexplosion, 1F);

				Destroy (this.gameObject);
				Destroy (other.gameObject);
			}
		}


	}
}