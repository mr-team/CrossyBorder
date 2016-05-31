using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour
{
	public GameObject UIPICKUP;
	public GameObject smoke;
	public Sprite pointSprite;

	GameMaster GM;
	private bool CHECKED = false;
	private bool Once = false;

	void Start ()
	{
		
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
	}

	void Update ()
	{
		if (GM.ladderCount >= GM.maxLadder && !Once)
		{
			Instantiate (smoke, transform.position, Quaternion.identity);
			GetComponent<SpriteRenderer> ().sprite = pointSprite;
			Once = true;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.transform.tag == "Player")
		{
			if (!CHECKED)
			{
				CHECKED = true;
				if (GM.ladderCount < GM.maxLadder)
				{
					//GM.ladderCount++;
					GetComponent<CustomAudioSource> ().PlayOnce ();
					GetComponent<SpriteRenderer> ().enabled = false;

					GameObject pickup = Instantiate (UIPICKUP, transform.position, Quaternion.identity) as GameObject;

					Destroy (this.gameObject, 0.204f);

				} else if (GM.ladderCount >= GM.maxLadder)
				{
					GM.AddScore (10);
					GetComponent<CustomAudioSource> ().PlayOnce ();
					GetComponent<SpriteRenderer> ().enabled = false;
					Destroy (this.gameObject, 0.204f);
				}

			}
            
		}
	}
}
