using UnityEngine;
using System.Collections;

public class EndZone : MonoBehaviour
{

	GameMaster GM;
	bool playerInEndzone;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();

	}

	void Update ()
	{
		if (playerInEndzone)
		{
			if (GM.ladderCount >= GM.maxLadder)
				GM.roundWon = true;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.transform.tag == ("Player"))
		{
			playerInEndzone = true;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.transform.tag == ("Player"))
		{
			playerInEndzone = false;
		}
	}
}
