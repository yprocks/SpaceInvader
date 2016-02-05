using UnityEngine;
using System.Collections;

public class RedArmyController : MonoBehaviour {

	public GameObject spawn2Bullet;
	public Transform spawn2Shot;
	public float fireRate;
	public float delay;
	public float speed = 2F;

	private Vector2 current_Pos;
	private Transform _transform;

	void Start ()
	{
		_transform = GetComponent<Transform> ();
		//Reset ();
		InvokeRepeating ("Fire", delay, fireRate);
	}

	void FixedUpdate(){
		this.current_Pos = this._transform.position;
		this.current_Pos -=	new Vector2(this.speed, 0);
		this._transform.position = this.current_Pos;

		if (current_Pos.x <= -450f) {
			Destroy ();
			//Reset ();
		}
	}

	void Fire ()
	{
		Instantiate(spawn2Bullet, spawn2Shot.position, spawn2Shot.rotation);
		//GetComponent<AudioSource>().Play();
	}

	void Destroy(){
		Destroy (this.gameObject);
	}

	//	void Reset(){
	//		this._transform.position = new Vector2 (450f, Random.Range(-180f, 180f));
	//	}
}