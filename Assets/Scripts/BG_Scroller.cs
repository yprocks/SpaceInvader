using UnityEngine;
using System.Collections;

public class BG_Scroller : MonoBehaviour
{
	public float scrollSpeed;

	private Renderer render;

	void Start ()
	{
		render = GetComponent<Renderer> ();
	}

	void Update ()
	{

		Vector2 offset = new Vector2( (Time.time * scrollSpeed) / 2f, 0);
		render.material.mainTextureOffset = offset;

	}
}