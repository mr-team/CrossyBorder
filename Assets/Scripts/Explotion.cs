using UnityEngine;
using System.Collections;

public class Explotion : MonoBehaviour
{

	void Update ()
	{
		
		Destroy (this.gameObject, 0.9f);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.transform.tag == "Car")
		{
			Destroy (other.gameObject);
		}
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.transform.tag == "Car")
		{
			Destroy (other.gameObject);
		}
	}
}
