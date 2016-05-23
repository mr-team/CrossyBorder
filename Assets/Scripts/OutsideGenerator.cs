using UnityEngine;
using System.Collections;

public class OutsideGenerator : MonoBehaviour {
    public Sprite border;
    public Sprite[] sand;
    public GameObject[] decoration;
    public bool leftSide = true;
    public int width = 5;

    private float dist = 1f;
    private GameMaster GM;
    private int height = 100;
    private System.Random rand;

    void Start() {
        if(leftSide)
            dist = -1f;
        GM = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        height = GM.worldHeigth;
        rand = new System.Random(GM.seed.GetHashCode());
        GenerateOutside();
    }

    void Update() {
        if(GM.worldHeigth != height) {
            height = GM.worldHeigth;
            rand = new System.Random(GM.seed.GetHashCode());
            GenerateOutside();
        }
    }
	
	public void GenerateOutside() {
        DeleteOutside();

        GameObject sandParent = new GameObject("Sand Parent");
        sandParent.transform.parent = transform;
        sandParent.transform.localPosition = Vector3.zero;

        GameObject borderParent = new GameObject("Border Parent");
        borderParent.transform.parent = transform;
        borderParent.transform.localPosition = Vector3.zero;

        GameObject decorParent = new GameObject("Decoration Parent");
        decorParent.transform.parent = transform;
        decorParent.transform.localPosition = Vector3.zero;

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                GameObject snd = new GameObject("Sand " + x + ", " + y);
                snd.transform.parent = sandParent.transform;
                snd.transform.localPosition = new Vector3(x * dist, y, 1f);
                snd.AddComponent<SpriteRenderer>().sprite = sand[rand.Next(0, sand.Length)];

                if(x == 0) {
                    GameObject kant = new GameObject("Border " + x + ", " + y);
                    kant.transform.parent = borderParent.transform;
                    kant.transform.localPosition = new Vector3(x * dist, y);
                    kant.AddComponent<SpriteRenderer>().sprite = border;
                    if(leftSide)
                        kant.transform.Rotate(new Vector3(0f, 0f, 180f));
                } else {
                    int percentage = rand.Next(0, 100);
                    if(percentage <= 15) {
                        GameObject decor = Instantiate(decoration[rand.Next(0, decoration.Length)], Vector3.zero, Quaternion.identity) as GameObject;
                        decor.transform.parent = decorParent.transform;
                        decor.transform.localPosition = new Vector3(x * dist, y);
                    }
                }
            }
        }

    }

    public void DeleteOutside() {
        
    }
}
