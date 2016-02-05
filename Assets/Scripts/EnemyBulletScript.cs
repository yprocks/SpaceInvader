using UnityEngine;
using System.Collections;

public class EnemyBulletScript : MonoBehaviour {

	public float speed = 10F;

	public GameObject explosion;

	private Vector2 current_Pos;
	private Transform _transform;


	// Use this for initialization
	void Start () {
		_transform = GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		this.current_Pos = this._transform.position;
		this.current_Pos -=	new Vector2(this.speed, 0);
		this._transform.position = this.current_Pos;

		if (current_Pos.x <= -450f) {
			Destroy ();
		}

	}

	void Destroy(){
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "Asteroid") {
			Destroy (this.gameObject);
			Destroy (other.gameObject);

			GameObject newexplosion = (GameObject)Instantiate(explosion, other.transform.position, other.transform.rotation);
			Destroy(newexplosion, 1);

		}
	}
}
