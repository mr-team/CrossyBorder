using UnityEngine;
using System.Collections;

public class PlayerKickedFromWall : MonoBehaviour
{
	GameMaster GM;
	public GameObject player;

	//Anims
	Animator playerAnim;
	public Animator trumpAnim;


	public bool active;
	float timer;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		playerAnim = player.GetComponent<Animator> ();
		active = false;
	}

	void Update ()
	{
		if (active)
		{
			timer += Time.deltaTime;
			trumpAnim.SetBool ("Kick", true);

			playerAnim.SetBool ("GetKicked", true);	

			if (timer >= 1)
			{
				GM.NextRound ();
				playerAnim.SetBool ("GetKicked", false);
				trumpAnim.SetBool ("Kick", false);
				timer = 0;
				active = false;
			}
		}
	}
}
