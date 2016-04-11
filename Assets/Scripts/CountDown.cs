using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
	GameMaster GM;
	public Text timeText;
	public Text livesLeft;

	float timeLeft;
	float timeLimit = 150;
	float timeDelta = 10;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
	}

	void Update ()
	{
		Debug.Log (timeLeft);
		if (GM.gameLoopActive)
		{
			timeText.text = ("Time left: " + (timeLimit - timeLeft));
			livesLeft.text = ("Lives left: " + GM.Player.Lives);
		}

		if (GM.gameLoopActive)
		{
			timeLeft += Time.deltaTime * timeDelta;

			if (timeLeft >= timeLimit)
			{
				GM.Player.LoseLife ();
				timeLeft = 0;
			}
		}
	}
}
