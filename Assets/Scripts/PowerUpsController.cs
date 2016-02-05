using UnityEngine;
using System.Collections;

public class PowerUpsController : MonoBehaviour {

	public float rotateFactor;
	public float speed;

	private Transform _transform;
	private Vector2 current_Pos;
	private Vector3 current_Rot;


	void Start(){
		_transform = gameObject.GetComponent <Transform> ();
	}

	void Update(){
		this.current_Pos = this._transform.position;
		this.current_Pos -=	new Vector2(this.speed, 0);
		this._transform.position = this.current_Pos;

		if (current_Pos.x <= -450f) {
			Destroy ();
		}

		_transform.Rotate(Vector3.forward * rotateFactor);
	}

	void Destroy(){
		Destroy (this.gameObject);
	}
		
}
