using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour
{
	GameMaster GM;
	Animator anim;
	public GameObject explotion;
	float timer;
	float fuseTime = 1.5f;

	bool Sprung;
	bool playerInRange;
	int numExplotions;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		anim = GetComponent<Animator> ();
	}

	void Update ()
	{
		if (GM.gameLoopActive && Sprung)
		{
			timer += Time.deltaTime;

			if (timer > fuseTime)
			{
				SpawnExplotions ();
				Destroy (this.gameObject, 0.5f);
				if (playerInRange)
				{
					GM.Player.LoseLife ();
				}
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			
			Sprung = true;
			playerInRange = true;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			
			playerInRange = false;
		}
	}

	void SpawnExplotions ()
	{
		if (numExplotions <= 9)
			for (int i = -1; i < 2; i++)
			{
				for (int o = -1; o < 2; o++)
				{
					Vector2 pos = new Vector2 (transform.position.x + i, transform.position.y + o);
					Instantiate (explotion, pos, Quaternion.identity);
					numExplotions++;
				}
			}
	}
}
