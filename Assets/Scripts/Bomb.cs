using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
	Animator bombAnim;

	float timer;
	float fuseTime = 1;

	bool explode;

	void Start ()
	{
		bombAnim = GetComponent<Animator> ();
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if (timer >= fuseTime)
		{
			explode = true;
			Destroy (this.gameObject, 1f);
		}

		HandleAnimation ();
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (explode && other.transform.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerController> ().Player.LoseLife ();
		}
		if (explode && other.transform.tag == "Car")
		{
			Destroy (other.gameObject);
		}
	}

	void HandleAnimation ()
	{
		if (explode)
		{
			bombAnim.SetBool ("Explode", true);
		}
	}
}
