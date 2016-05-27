using UnityEngine;
using System.Collections;

public class MoveShovelPickup : MonoBehaviour {
    public string targetUiName = "Shovel";
    public float speed = 25f;

    private GameObject targetUI;
    private GameMaster GM;
    private PlayerController PC;

    void Start() {
        GM = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
        targetUI = GameObject.Find(targetUiName + "" + (PC.shovelCount + 1));
    }
	
	void Update() {
        Vector3 screen2World = Camera.main.ScreenToWorldPoint(targetUI.transform.position);

        if(Vector3.Distance(transform.position, screen2World) <= 0.2f) {
            if(PC.shovelCount < 3)
                PC.shovelCount++;
            Destroy(gameObject);
        }

        transform.localScale *= 1.01f;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, screen2World, step);

	}
}
