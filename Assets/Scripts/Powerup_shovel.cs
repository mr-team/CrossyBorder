using UnityEngine;
using System.Collections;

public class Powerup_shovel : MonoBehaviour
{
    public GameObject UIPICKUP;
    GameMaster GM;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			if (other.GetComponent<PlayerController> ().shovelCount < 3)
			{
                //other.GetComponent<PlayerController> ().shovelCount++;

                GameObject pickup = Instantiate(UIPICKUP, transform.position, Quaternion.identity) as GameObject;

            } else if (other.GetComponent<PlayerController> ().shovelCount >= 3)
			{
				GM.AddScore (50);
			}
			Destroy (this.gameObject);
		}
	}
}
