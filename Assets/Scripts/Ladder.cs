using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour
{
	GameMaster GM;

	void Start ()
	{
		
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.transform.tag == "Player")
		{
			GM.ladderCount++;
			Destroy (this.gameObject);
		}
	}
}
