using UnityEngine;
using System.Collections;

public class CloudFloat : MonoBehaviour {
    private float moveEachSec = 0.7f;
    private Vector3 lastMove = Vector3.zero;
    private float timer = 0f;

	void Update() {
        if(timer < moveEachSec) {
            timer += Time.deltaTime;
        } else {
            timer = 0f;
            if(lastMove == Vector3.zero) {
                int state = new System.Random().Next(1, 3);
                switch(state) {
                    case 1:
                    lastMove = new Vector3(Random.Range(-.200f, .200f), 0f, 0f);
                    break;
                    case 2:
                    lastMove = new Vector3(0f, Random.Range(-.200f, .200f), 0f);
                    break;
                    case 3:
                    lastMove = new Vector3(Random.Range(-.200f, .200f), Random.Range(-.200f, .200f), Random.Range(-.200f, .200f));
                    break;
                }
                transform.Translate(lastMove);
            } else {
                lastMove *= -1f;
                transform.Translate(lastMove);
                lastMove = Vector3.zero;
            }
            moveEachSec = Random.Range(0.3f, 1.5f);
        }
	}
}
