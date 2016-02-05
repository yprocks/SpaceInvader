using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//public variables
	public float speed = 2f; 				//controls player speed
	public GameObject bullet;
	public Transform bulletSpot;
	public float fireRate;

	private float readyDelay;
	private float readyTime;
	private float nextFire;
	private float playerXInput;
	private float playerYInput;
	private Vector2 _currentPosition;
	private Transform _transform;

	// Use this for initialization
	void Start () {
		_transform = gameObject.GetComponent <Transform> ();
		readyDelay = 1F;
		StartCoroutine (Flash (readyDelay));	
	}

	public IEnumerator Flash(float readyDelay){

		if (GameController.CheckPlayerLife() < 3) {
			
			PlayerCollision.canTakeDamage = false;

			while (readyTime < readyDelay) {
				readyTime += 10 * Time.deltaTime;
				yield return new WaitForSeconds (0.4F);
				GetComponent<Renderer> ().enabled = !GetComponent<Renderer> ().enabled;
				//Debug.Log (readyTime);
				//Debug.Log(" " + GameController.playerLife);
			}

			yield return new WaitForSeconds (0.4F);

			GetComponent<Renderer> ().enabled = true;

			yield return new WaitForSeconds (readyDelay);
			//Debug.Log (PlayerCollision.canTakeDamage);
			PlayerCollision.canTakeDamage = true;
		}
		yield return new WaitForSeconds (1F);
	}


	// Update is called once per frame
	void Update(){
		
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			//GameObject clone = 
			Instantiate (bullet, bulletSpot.position, bulletSpot.rotation); //as GameObject;
			//GetComponent<AudioSource>().Play ();
		}
	}

	void FixedUpdate () {
		playerXInput = Input.GetAxis ("Horizontal");
		playerYInput = Input.GetAxis ("Vertical");

		//Debug.Log ("Log " + playerInput);

		this._currentPosition = this._transform.position;

		//move right if input positive
		if(playerXInput > 0){							// && this._currentPosition.x < 290f
			this._currentPosition += new Vector2(this.speed, 0);
		}
		//move left if input positive
		if(playerXInput < 0){ 							// && this._currentPosition.x > -290f
			this._currentPosition -= new Vector2(this.speed, 0);
		}
		if(playerYInput > 0){							// && this._currentPosition.y < 290f
			this._currentPosition += new Vector2(0, this.speed);
		}
		//move left if input positive
		if(playerYInput < 0){ 							// && this._currentPosition.y > -290f
			this._currentPosition -= new Vector2(0, this.speed);
		}

		CheckBounds ();

		this._transform.position = this._currentPosition;	


	}

	//private method to check boundry
	private void CheckBounds(){
		if (this._currentPosition.x < -350) {
			this._currentPosition.x = -350;
		}
		if (this._currentPosition.x > 0) {
			this._currentPosition.x = 0;
		}

		if (this._currentPosition.y < -200) {
			this._currentPosition.y = -200;
		}
		if (this._currentPosition.y > 200) {
			this._currentPosition.y = 200;
		}
	}
}
