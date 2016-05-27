using UnityEngine;
using System.Collections;

public class Explotion : MonoBehaviour
{
	bool giveScore = false;

	public bool GiveScore {
		get
		{
			return giveScore;
		}
		set
		{
			giveScore = value;
		}
	}

	void Update ()
	{
		
		Destroy (this.gameObject, 0.9f);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.transform.tag == "Car")
		{
			if (giveScore)
				other.gameObject.GetComponent<CarController> ().DestroyCarScore ();

			other.gameObject.GetComponent<CarController> ().DestroyCar ();
		}
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.transform.tag == "Car")
		{
			if (giveScore)
				other.gameObject.GetComponent<CarController> ().DestroyCarScore ();

			other.gameObject.GetComponent<CarController> ().DestroyCar ();
		}
	}
}
