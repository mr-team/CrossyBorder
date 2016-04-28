using UnityEngine;
using System.Collections;

public class Powerup_shovel : MonoBehaviour
{


	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<PlayerController> ().shovelCount++;
			Destroy (this.gameObject);
		}
	}
}
