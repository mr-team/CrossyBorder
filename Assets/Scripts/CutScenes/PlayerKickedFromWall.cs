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
	public bool invokeActive;
	float timer;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		playerAnim = player.GetComponent<Animator> ();
		active = false;
		holderMethod = ser;
	}

	void Update ()
	{
		if (active)
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

				active = false;
			}
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
