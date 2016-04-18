using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
	GameMaster GM;
	public Text timeText;
	public Text livesLeft;

	float timeLeft;
	float timeLimit = 200;
	float timeDelta = 10;

	public bool debugStopCount;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
	}

	void Update ()
	{
		
		if (GM.gameLoopActive)
		{
			timeText.text = ("Time left: " + ParseSeconds (timeLimit - timeLeft));
			livesLeft.text = ("Lives left: " + GM.Player.Lives);
		}

		if (GM.gameLoopActive && !GM.gamePaused && !debugStopCount)
		{
			timeLeft += Time.deltaTime * timeDelta;

			if (timeLeft >= timeLimit)
			{
				GM.Player.LoseLife ();
				timeLeft = 0;
			}
		}

	}

	public string ParseSeconds (float time)
	{
		string[] tid = time.ToString ().Split (".".ToCharArray ());
		return tid [0];
	}

	public void ResetTimer ()
	{
		timeLeft = 0;
	}
}
