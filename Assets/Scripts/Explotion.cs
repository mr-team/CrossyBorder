using UnityEngine;
using System.Collections;

public class Explotion : MonoBehaviour
{
	void Update ()
	{
		Destroy (this.gameObject, 1);
	}
}
