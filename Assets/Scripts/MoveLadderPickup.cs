using UnityEngine;
using System.Collections;

public class MoveLadderPickup : MonoBehaviour {
    public string targetUiName = "";
    public float speed = 25f;

    private GameObject targetUI;
    private GameMaster GM;

    void Start() {
        GM = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        targetUI = GameObject.Find(targetUiName);
    }

    void Update() {
        Vector3 screen2World = Camera.main.ScreenToWorldPoint(targetUI.transform.position);

        if(Vector3.Distance(transform.position, screen2World) <= 0.2f) {
            if(GM.ladderCount < GM.maxLadder)
                GM.ladderCount++;
            Destroy(gameObject);
        }

        transform.localScale *= 1.01f;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, screen2World, step);

	}
}
