using UnityEngine;
using System.Collections;

public class SmokePuff : MonoBehaviour
{

	void Start ()
	{
	
	}

	void Update ()
	{
		Destroy (this.gameObject, 1f);
	}
}
