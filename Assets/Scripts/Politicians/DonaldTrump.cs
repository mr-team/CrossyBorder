using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DonaldTrump : politician
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
			GM.fbiTroops = true;
			Debug.Log ("card " + cardNum + " on " + gameObject.name + " was activated");
		}
		if (cardNum == 1)
		{
			GM.mines = true;
			Debug.Log ("card " + cardNum + " on " + gameObject.name + " was activated");
		}
	}

}
