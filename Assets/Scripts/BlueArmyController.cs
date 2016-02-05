using UnityEngine;
using System.Collections;

public class BlueArmyController : MonoBehaviour {

	public GameObject spawn1Bullet;
	public Transform spawn1Shot;
	public float fireRate;
	public float delay;
	public float speed = 2F;

	private Vector2 current_Pos;
	private Transform _transform;
	private float randm;

	void Start ()
	{
		_transform = GetComponent<Transform> ();
		//Reset ();
		InvokeRepeating ("Fire", delay, fireRate);
		StartCoroutine(Drift());
	}


	IEnumerator Drift ()
	{
		yield return new WaitForSeconds (0.01f);
		while (true) {
			
			randm = Random.Range( -350f, 250f );


			this.current_Pos = this._transform.position;
			this.current_Pos -=	new Vector2 (this.speed, 0);

			if (current_Pos.y > 0 && current_Pos.x <= Random.Range(randm - 20F, randm + 20F)) {
				//Debug.Log (Random.Range(randm - 20F, randm + 20F));

				while (current_Pos.y >= -180f) {
					this.current_Pos -=	new Vector2(this.speed, this.speed);
					yield return new WaitForSeconds (0.01f);
					CheckBounds ();
					if (this._transform.position.x <= -450f) {
						Destroy ();
					}
					this._transform.position = this.current_Pos;
				}
			}
			if (current_Pos.y < 0 && current_Pos.x <= Random.Range(randm - 20F, randm + 20F)) {
				//Debug.Log (Random.Range(randm - 20F, randm + 20F));

				while (current_Pos.y <= 180f) {

					this.current_Pos -=	new Vector2(this.speed, -this.speed);
					yield return new WaitForSeconds (0.01f);
					CheckBounds ();
					if (this._transform.position.x <= -450f) {
						Destroy ();
					}
					this._transform.position = this.current_Pos;
				}
			}
			yield return new WaitForSeconds (0.01f);
			CheckBounds ();
			if (this._transform.position.x <= -450f) {
				Debug.Log ("In");
				Destroy ();
			}
			this._transform.position = this.current_Pos;
		}
	}

	void Fire ()
	{
		Instantiate(spawn1Bullet, spawn1Shot.position, spawn1Shot.rotation);
		//GetComponent<AudioSource>().Play();
	}

	private void CheckBounds(){
		if (this.current_Pos.y < -180f) {
			this.current_Pos.y = -180f;
		}
		if (this.current_Pos.y > 180f) {
			this.current_Pos.y = 180f;
		}
	}

	void Destroy(){
		Destroy (this.gameObject);
	}

//	void Reset(){
//		this._transform.position = new Vector2 (450f, Random.Range(-180f, 180f));
//	}
}
