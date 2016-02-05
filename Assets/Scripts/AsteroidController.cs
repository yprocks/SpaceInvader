using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {


	public float speed = 2F;

	private Vector2 current_Pos;
	private Transform _transform;

	void Start ()
	{
		_transform = GetComponent<Transform> ();
		//Reset ();
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

	void Destroy(){
		Destroy (this.gameObject);
	}

}
