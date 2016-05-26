using UnityEngine;
using System.Collections;

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

	public bool exitPhone;

	void Start ()
	{
		mobile.transform.position = movePoints [0].position;
		textScreen.SetActive (false);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.T))
			active = true;
		
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
		
		StartCoroutine ("MovePhoneUp");
	}

	void CutTwo ()
	{
		
	}

	void End ()
	{
		
	}

	void reset ()
	{
		mobile.transform.position = movePoints [0].position;
	}

	IEnumerator MovePhoneUp ()
	{
		if (!exitPhone)
		{
			mobile.transform.position = Vector3.MoveTowards (mobile.transform.position, movePoints [1].position, 3.2f);
			yield return new WaitUntil (() => mobile.transform.position == movePoints [1].position);
			yield return new WaitForSeconds (1f);
			textScreen.SetActive (true);
			Debug.Log ("outside loop");

		} else if (exitPhone)
		{
			Debug.Log ("inside loop");
			textScreen.SetActive (false);
			yield return new WaitForSeconds (0.5f);
			mobile.transform.position = Vector3.MoveTowards (mobile.transform.position, movePoints [0].position, 3.2f);
			yield return new WaitUntil (() => mobile.transform.position == movePoints [0].position);
			active = false;
			exitPhone = false;
		}

	}

	public void CloseText ()
	{
		exitPhone = true;
	}
}
