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

	public Transform[] movePoints = new Transform[2];

	Cuts cuts;
	public GameObject mobile;
	public bool active;
	public GameObject textScreen;
	RawImage rawImage;
	public Texture[] textMessages = new Texture[7];
	public bool exitPhone;

	void Start ()
	{
		rawImage = textScreen.GetComponent<RawImage> ();
		mobile.transform.position = movePoints [0].position;
		textScreen.SetActive (false);
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
		if (!exitPhone)
			StartCoroutine (MovePhoneUp (1));
		if (exitPhone)
			StartCoroutine (MovePhoneUp (2));
	}

	void CutTwo ()
	{
		
	}

	void End ()
	{
		
	}


	IEnumerator MovePhoneUp (float id)
	{
		if (id == 1)
		{
			mobile.transform.position = Vector3.MoveTowards (mobile.transform.position, movePoints [1].position, 3.2f);

			yield return new WaitUntil (() => mobile.transform.position == movePoints [1].position);
			yield return new WaitForSeconds (1f);

			textScreen.SetActive (true);

		}

		if (id == 2)
		{	
			textScreen.SetActive (false);
			yield return new WaitForSeconds (0.5f);
			mobile.transform.position = Vector3.MoveTowards (mobile.transform.position, movePoints [0].position, 3.2f);
			yield return new WaitUntil (() => mobile.transform.position == movePoints [0].position);
			active = false;
		}
	}

	void reset ()
	{
		textScreen.SetActive (false);
		cuts = Cuts.end;
		mobile.transform.position = movePoints [0].position;
		exitPhone = false;
	}

	public void CloseText ()
	{
		exitPhone = true;
	}

	void changeTexture (int index)
	{
		rawImage.texture = textMessages [index];
	}
}
