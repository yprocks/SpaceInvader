using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {

	public GameObject BossBullet;
	public Transform BossShot;
	public float fireRate;
	public float delay;
	public float speed = 2F;

	private Vector2 current_Pos;
	private Transform _transform;

	void Start ()
	{
		_transform = GetComponent<Transform> ();
		InvokeRepeating ("Fire", delay, fireRate);
		StartCoroutine(Move());
	}

	IEnumerator Move ()
	{
		yield return new WaitForSeconds (0.01f);
		while (true)
		{
					this.current_Pos = this._transform.position;
					this.current_Pos +=	new Vector2(0, this.speed);
					
					if (this.current_Pos.y >= 170f) {
						do {
							this.current_Pos -=	new Vector2(0, this.speed);
							yield return new WaitForSeconds (0.01f);
							this._transform.position = this.current_Pos;
						} while(current_Pos.y >= -170f);
					}
					yield return new WaitForSeconds (0.01f);

					this._transform.position = this.current_Pos;
		}
	}

	void Update(){

	}

	void Fire ()
	{
		Instantiate(BossBullet, BossShot.position, BossShot.rotation);
	}


}
