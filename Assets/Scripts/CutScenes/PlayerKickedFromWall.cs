using UnityEngine;
using System.Collections;

public class PlayerKickedFromWall : MonoBehaviour
{
	delegate void HolderMethod ();

	HolderMethod holderMethod;
	GameMaster GM;
	public GameObject player;

	//Sound
	public CustomAudioSource customAudioSource;
	public AudioClip KickedFromWallSong;
	public AudioClip standardSong;

	//Anims
	Animator playerAnim;
	public Animator trumpAnim;

	public bool active;
	public bool kicked;
	public bool invokeActive;
	float timer;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		playerAnim = player.GetComponent<Animator> ();
		kicked = false;
		holderMethod = ser;
		player.GetComponent<SpriteRenderer> ().enabled = false;
	}

	void Update ()
	{
		if (active)
		{
			player.GetComponent<SpriteRenderer> ().enabled = true;
		
			if (kicked)
			{
				timer += Time.deltaTime;
				trumpAnim.SetBool ("Kick", true);

				playerAnim.SetBool ("GetKicked", true);	

				if (customAudioSource.source.clip != KickedFromWallSong)
				{
					customAudioSource.source.clip = KickedFromWallSong;
					customAudioSource.Play (KickedFromWallSong);
					invokeActive = true;
				}

				if (timer >= 1)
				{
					GM.NextRound ();
					playerAnim.SetBool ("GetKicked", false);
					trumpAnim.SetBool ("Kick", false);
					timer = 0;

					kicked = false;
				}
			} 
		} else if (!active)
		{
			player.GetComponent<SpriteRenderer> ().enabled = false;
		}
		if (invokeActive)
		{

			Invoke ("ser", KickedFromWallSong.length);
		}
	}

	void ser ()
	{
		if (invokeActive)
		{
			customAudioSource.Play (standardSong);
			//Debug.Log ("hei");
			invokeActive = false;
		}
	}
}
