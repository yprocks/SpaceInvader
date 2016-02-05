using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float speed = 2F;
	public int scoreValue = 10;
	public int bonusValue = 2;
	public GameObject explosion;

	private Collider2D _other;

	private Vector2 current_Pos;
	private Transform _transform;
	private GameController gameController;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {

		audioSource = GetComponent<AudioSource> ();

		GameObject gameControllerObject = GameObject.Find ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}


		_transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.current_Pos = this._transform.position;
		this.current_Pos +=	new Vector2(this.speed, 0);
		this._transform.position = this.current_Pos;

		if (current_Pos.x >= 405f) {
			Destroy ();
		}
	}

	void Destroy(){
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){

		_other = other;

		if (other.gameObject.tag == "Player") {
			return;
		}
	
		if (other.gameObject.tag == "Spawn") {
			gameController.AddScore (scoreValue);
			InstantiateExplosion ();
		}

		if (other.gameObject.tag == "Boss") {
			return;
			//InstantiateExplosion ();;
		}

		if (other.gameObject.tag == "Asteroid") {
			InstantiateExplosion ();
		}

		if(other.gameObject.tag == "Kill-All" || other.gameObject.tag == "Life-Up" || other.gameObject.tag == "Sheild-Up"){
			return;
		}
			
		Destroy (this.gameObject);
		Destroy (other.gameObject);
			
	}

	void InstantiateExplosion (){

		GameObject newexplosion = (GameObject)Instantiate(explosion, _other.transform.position, _other.transform.rotation);
		Destroy(newexplosion, 1);
	}

	private void PlayAudio(){
		audioSource.Play ();
	}

		
}