using UnityEngine;
using System.Collections;

public class CanvasHandler : MonoBehaviour
{
	GameMaster GM;
	public Canvas mainMenu;
	public Canvas pauseMenu;
	public Canvas ui;
	public Canvas deathScreen;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		deathScreen.enabled = false;
	}

	void Update ()
	{
		//main menu handler
		if (GM.gameLoopActive)
			mainMenu.enabled = false;

		//death screen handler
		if (!GM.Player.Alive)
			deathScreen.enabled = true;
		else
			deathScreen.enabled = false;

		//pasue menu handler
		if (GM.gamePaused)
			pauseMenu.enabled = true;
		else
			pauseMenu.enabled = false;
	}
}
