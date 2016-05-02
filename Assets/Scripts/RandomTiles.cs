using UnityEngine;
using System.Collections;

public class RandomTiles : MonoBehaviour {
    public Sprite[] tiles;

	void Start() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(sr.sprite == null)
            sr.sprite = tiles[Random.Range(0, tiles.Length)];
	}
}
