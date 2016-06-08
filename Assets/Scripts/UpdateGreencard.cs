using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UpdateGreencard : MonoBehaviour {
    public int[] scores;
    public Sprite[] sprites;

    private SpriteRenderer sr;
    private GameMaster gm;
    private int lastScore = 0;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        if(scores.Length != sprites.Length)
            Debug.LogError("Score and sprite length is not the same.");
    }

    void Update() {
        if(lastScore != gm.score) {
            for(int i = 0; i < scores.Length; i++) {
                if(gm.score > scores[i])
                    sr.sprite = sprites[i];
            }
            lastScore = gm.score;
        }
    }
}
