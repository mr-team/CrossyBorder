using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetTextCutScene : MonoBehaviour
{
	public enum Cuts
	{
		cutOne,
		cutTwo,
		end,
	}

	Cuts cuts;


	public PlayerController playerControl;
	public GameObject textScreen;
	RawImage rawImage;
	public Texture[] textMessages = new Texture[7];


	//Anims
	public Animator phoneAnim;

	// Timers
	float timer;
	float timer2;
	float timer3;

	public bool active;

	void Start ()
	{
		rawImage = textScreen.GetComponent<RawImage> ();

		textScreen.SetActive (false);
		cuts = Cuts.end;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.T))
		{
			active = true;
			cuts = Cuts.cutOne;
		}
			
		if (active)
		{
			switch (cuts)
			{
				case Cuts.cutOne:
					CutOne ();
					break;

				case Cuts.cutTwo:
					CutTwo ();
					break;

				case Cuts.end:
					End ();
					break;
			}
		}

		if (!active)
		{
			reset ();
		}
	}

	void CutOne ()
	{
		playerControl.StunPlayer ();
		playerControl.Player.Imortal = true;

		timer += Time.deltaTime;
		phoneAnim.SetBool ("GoUp", true);
		if (timer >= 1f)
		{
			textScreen.SetActive (true);

		}
	}

	void CutTwo ()
	{
		timer += Time.deltaTime;
		phoneAnim.SetBool ("GoUp", false);
		if (timer >= 1f)
		{
			textScreen.SetActive (false);
			playerControl.DeStunPlayer ();
			playerControl.Player.Imortal = false;
			reset ();
		}
	}

	void End ()
	{
		
	}

	void reset ()
	{
		timer = 0;
		cuts = Cuts.end;
	}

	public void CloseText ()
	{
		timer = 0;
		cuts = Cuts.cutTwo;
	}

	void changeTexture (int index)
	{
		rawImage.texture = textMessages [index];
	}

	public void Activate (int index)
	{
		rawImage.texture = textMessages [index];
		active = true;
		cuts = Cuts.cutOne;
	}
}
