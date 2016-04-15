using UnityEngine;
using System.Collections;

public class BernieSanders : politician
{
	protected override void Start ()
	{
		base.Start ();
	}

	protected override void Update ()
	{
		base.Update ();
	}

	protected override void OnMouseDown ()
	{
		base.OnMouseDown ();
		clickCB (this.gameObject);
	}

	protected override void ActivateAbility (int cardNum)
	{
		if (cardNum == 0)
		{
			GM.bearTraps = true;
			Debug.Log ("card " + cardNum + " on " + gameObject.name + " was activated");
		}
		if (cardNum == 1)
		{
			GM.mines = true;
			Debug.Log ("card " + cardNum + " on " + gameObject.name + " was activated");
		}
	}
}
