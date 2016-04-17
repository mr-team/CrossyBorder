using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour {
    public Sprite[] sprites;
    public float speed = 0.3f;

    private float timer = 0f;
    private int spriteIndex = 0;
    private SpriteRenderer renderer;

	void Start() {
        renderer = GetComponent<SpriteRenderer>();
        if(sprites.Length == 0)
            Debug.LogError("No animation sprites found");
	}
	
	void Update() {
        timer += Time.deltaTime;
        if(timer >= speed) {
            if(spriteIndex == sprites.Length - 1) {
                spriteIndex = 0;
                renderer.sprite = sprites[spriteIndex];
            } else {
                spriteIndex++;
                renderer.sprite = sprites[spriteIndex];
            }
            timer = 0f;
        }
	}
}
