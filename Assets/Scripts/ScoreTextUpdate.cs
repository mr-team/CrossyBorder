using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreTextUpdate : MonoBehaviour {
    
    private TextMesh tm;
    private GameMaster gm;

	void Start() {
        tm = GetComponent<TextMesh>();
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
	}
	
	void Update() {
        tm.text = Mathf.Clamp(gm.score, 0, 999999999).ToString();
	}
}
