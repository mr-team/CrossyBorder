using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour
{
	public enum directions
	{
		left,
		right
	}

	public GameObject car;

	public Sprite[] sprites = new Sprite[2];
    public Sprite[] roadSprites;
    [Range(0, 100)]
    public int roadLength = 30;
    GameMaster GM;

	public Transform carParent;

	float spawnTimer;
	bool spawned;
    public directions SpawnDir;

    void Awake() {
        GM = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        GM.carLanes.Add(Mathf.RoundToInt(transform.position.y));
    }

	void Start ()
	{	

        if(carParent == null)
            carParent = transform;

        float add = 1f;
        if(SpawnDir != directions.left)
            add = -1f;
        for(int i = 0; i < roadLength; i++) {
            GameObject road = new GameObject("Road " + i);
            road.transform.parent = this.transform;
            road.AddComponent<SpriteRenderer>().sprite = roadSprites[Random.Range(0, roadSprites.Length)];
            road.transform.position = new Vector3(transform.position.x + (add * i), transform.position.y - 0.4f, transform.position.z);
        }
	}

	void Update ()
	{
		
		spawnTimer += Time.deltaTime;
		if (spawnTimer > 2)
		{
			SpawnCar ();
			spawnTimer = 0;

		}

	}

	public void SpawnCar ()
	{
		GameObject spawnedCar = Instantiate (car, transform.position, Quaternion.identity) as GameObject;
		CarController carControl = spawnedCar.GetComponent<CarController> ();
		spawnedCar.transform.parent = carParent;
		if (SpawnDir == directions.left)
		{
			if (GM.fbiTroops)
			{
				spawnedCar.transform.position = new Vector2 (transform.position.x - Random.Range (0, 6), transform.position.y);
				carControl.MoveLeft ();
				spawnedCar.GetComponent<SpriteRenderer> ().sprite = sprites [2];
			} else
			{
				spawnedCar.transform.position = new Vector2 (transform.position.x - Random.Range (0, 6), transform.position.y);
				carControl.MoveLeft ();
				spawnedCar.GetComponent<SpriteRenderer> ().sprite = sprites [0];
			}

		} else
		{
			if (GM.fbiTroops)
			{
				carControl.MoveRight ();
				spawnedCar.transform.position = new Vector2 (transform.position.x + Random.Range (0, 10), transform.position.y);
				spawnedCar.GetComponent<SpriteRenderer> ().sprite = sprites [3];


			} else
			{
				carControl.MoveRight ();
				spawnedCar.transform.position = new Vector2 (transform.position.x + Random.Range (0, 10), transform.position.y);
				spawnedCar.GetComponent<SpriteRenderer> ().sprite = sprites [1];

			}
		}
	}
}
