using UnityEngine;
using System.Collections;

public class FeedBack_StopWatch : MonoBehaviour
{
	Animator stopAnim;
	public bool oneSec;
	public bool twoSec;

	void Start ()
	{
		stopAnim = GetComponent<Animator> ();
	}


	void Update ()
	{
		if (oneSec)
		{
			stopAnim.SetBool ("1Sec", true);
			stopAnim.SetBool ("2Sec", false);
			twoSec = false;

			Destroy (this.gameObject, 1f);
		}

		if (twoSec)
		{
			stopAnim.SetBool ("1Sec", false);
			stopAnim.SetBool ("2Sec", true);
			oneSec = false;

			Destroy (this.gameObject, 2f);
		}


	}
}
