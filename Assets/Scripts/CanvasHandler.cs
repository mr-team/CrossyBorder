using UnityEngine;
using System.Collections;

public class CanvasHandler : MonoBehaviour
{
	GameMaster GM;
	public Canvas mainMenu;
	public Canvas pauseMenu;
	public Canvas ui;
	public Canvas deathScreen;
	public Canvas winScreen;
    public Canvas tutorial;

    public GameObject mainCamera;

    void Start ()
	{
		
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		deathScreen.enabled = false;
		ui.enabled = false;
		winScreen.enabled = false;
	}

	void Update ()
	{
		//main menu handler / UI handler
		if (GM.gameLoopActive)
		{
			ui.enabled = true;
			mainMenu.enabled = false;
		}

		//death screen handler
		if (!GM.Player.Alive)
		{
			deathScreen.enabled = true;
			ui.enabled = false;

            //Hacky place to make it play the lose music. Sorry :(
            CustomAudioSource cas = deathScreen.GetComponent<CustomAudioSource>();
            if(!cas.isPlaying()) {
                mainCamera.GetComponent<CustomAudioSource>().Stop();
                cas.Play();
            }

        } else
			deathScreen.enabled = false;

		//pasue menu handler
		if (GM.gamePaused)
			pauseMenu.enabled = true;
		else
			pauseMenu.enabled = false;

        tutorial.enabled = GM.inTutorial;

        if (GM.roundWon)
		{
			winScreen.enabled = true;
		}
	}
}
