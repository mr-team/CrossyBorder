using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour
{
	GameMaster GM;
	public Transform tileParent;
	public GameObject sandTileGraphic;
	public GameObject ladderPrefab;
	public GameObject mineprefab;
	public GameObject bearTrapPrefab;
	public GameObject[] tileGraphic = new GameObject[10];
	public System.Random rand;

    void Awake() {
        GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
        if(GM.seed == "")
            GM.seed = "Matias er " + Random.Range(int.MinValue, int.MaxValue) + " meter høy.";
    }

	void Start()
	{
		GM.onNextRound += ClearWorld;
        GM.onNextRound += GM.ReAwake;
		GM.onNextRound += DrawWorld;
        rand = new System.Random(GM.seed.GetHashCode());
        DrawWorld ();
	}

	void DrawWorld ()
	{
        bool spawnedCatapult = false;
		for (int i = 0; i < GM.World.Tiles.GetLength (0); i++)
		{
			for (int u = 0; u < GM.World.Tiles.GetLength (1); u++)
			{
				GameObject tileObj = Instantiate (sandTileGraphic, new Vector2 (GM.World.GetTilePos (i, u).x, GM.World.GetTilePos (i, u).y), Quaternion.identity) as GameObject;
				tileObj.name = ("Tile: " + i + " , " + u);
				tileObj.transform.parent = tileParent;
				float ladder = rand.Next () / 100000000f;
				float mineRan = rand.Next () / 100000000f;
				float BTrand = rand.Next () / 100000000f;
                float catapultRand = rand.Next() / 100000000f;

                if (!GM.World.Tiles [i, u].Walkable && u == 0)
				{
					GameObject rock = Instantiate (tileGraphic [0], new Vector3 (GM.World.GetTilePos (i, u).x, GM.World.GetTilePos (i, u).y, -0.1f), Quaternion.identity) as GameObject;
					rock.name = ("Rock");
					rock.transform.parent = tileObj.transform;
				} else if (!GM.World.Tiles [i, u].Walkable)
				{
					GameObject rock = Instantiate (tileGraphic [Random.Range (0, tileGraphic.Length)], new Vector3 (GM.World.GetTilePos (i, u).x, GM.World.GetTilePos (i, u).y, -0.1f), Quaternion.identity) as GameObject;
					rock.name = ("Rock");
					rock.transform.parent = tileObj.transform;
				} else if (ladder <= 1f)
				{
                    if(u == 1 && i == 5)
                        continue;
                    GameObject laddr = Instantiate (ladderPrefab, new Vector3 (GM.World.GetTilePos (i, u).x, GM.World.GetTilePos (i, u).y, -0.1f), Quaternion.identity) as GameObject;
					laddr.name = ("Ladder");
                    if(rand.Next(0, 100) <= 50 /* % */) //Percentage needed to complete the level
					    GM.maxLadder++;
					laddr.transform.parent = tileObj.transform;
				} else if(GM.catapult && catapultRand >= 2f && catapultRand <= 3f && !spawnedCatapult) {
                    if(u <= 4)
                        continue;
                    GameObject catapult = Instantiate(GM.catapultPrefab, new Vector3(GM.World.GetTilePos(i, u).x, GM.World.GetTilePos(i, u).y, -0.1f), Quaternion.identity) as GameObject;
                    catapult.name = ("Catapult");
                    catapult.transform.parent = tileObj.transform;
                    spawnedCatapult = true;
                    GM.World.Tiles[i, u].Walkable = false;

                } else if(GM.mines && mineRan >= 2f && mineRan <= 3f)
				{
                    if(u <= 2)
                        continue;
                    GameObject mine = Instantiate (mineprefab, new Vector3 (GM.World.GetTilePos (i, u).x, GM.World.GetTilePos (i, u).y, -0.1f), Quaternion.identity) as GameObject;
					mine.name = ("mine");
					mine.transform.parent = tileObj.transform;
					
				} else if (GM.bearTraps && BTrand >= 2f && BTrand <= 3f)
				{
                    if(u == 1 && i == 5)
                        continue;
                    GameObject BearTrap = Instantiate (bearTrapPrefab, new Vector3 (GM.World.GetTilePos (i, u).x, GM.World.GetTilePos (i, u).y, -0.1f), Quaternion.identity) as GameObject;
					BearTrap.name = ("BearTrap");
					BearTrap.transform.parent = tileObj.transform;
				}

			}
		}
	}

	void ClearWorld ()
	{
		Debug.Log ("cleared the world");
        GM.carLanes.Clear();
        if(GM.seed.Length >= 10)
            GM.seed = GM.seed.Remove(10, GM.seed.Length - 10);
        GM.seed = GM.seed + rand.Next();
        GM.maxLadder = 0;
        rand = new System.Random(GM.seed.GetHashCode());

        GM.worldHeigth += rand.Next(5, 15);

        foreach (Transform child in tileParent)
		{
			GameObject.Destroy (child.gameObject);
		}

        foreach(GameObject obj in GM.carLaneObjects) {
            Destroy(obj);
        }
        GM.carLaneObjects.Clear();
	}
}
