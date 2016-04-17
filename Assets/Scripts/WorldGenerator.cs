using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour
{
	GameMaster GM;
	public Transform tileParent;
	World world;
    public GameObject sandTileGraphic;
    public GameObject ladderPrefab;
	public GameObject[] tileGraphic = new GameObject[10];
    public System.Random rand;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		GM.onNextRound += ClearWorld;
		GM.onNextRound += DrawWorld;
		world = GM.World;
        rand = new System.Random(GM.seed.GetHashCode());
        DrawWorld ();
	}

	void DrawWorld ()
	{
		for (int i = 0; i < world.Tiles.GetLength (0); i++)
		{
			for (int u = 0; u < world.Tiles.GetLength (1); u++)
			{
				GameObject tileObj = Instantiate (sandTileGraphic, new Vector2 (world.GetTilePos (i, u).x, world.GetTilePos (i, u).y), Quaternion.identity) as GameObject;
				tileObj.name = ("Tile: " + i + " , " + u);
				tileObj.transform.parent = tileParent;
                float ladder = rand.Next() / 100000000f;

                if(!world.Tiles[i, u].Walkable && u == 0) {
                    GameObject rock = Instantiate(tileGraphic[0], new Vector3(world.GetTilePos(i, u).x, world.GetTilePos(i, u).y, -0.1f), Quaternion.identity) as GameObject;
                    rock.name = ("Rock");
                    rock.transform.parent = tileObj.transform;
                } else if(!world.Tiles[i, u].Walkable) {
                    GameObject rock = Instantiate(tileGraphic[Random.Range(0, tileGraphic.Length)], new Vector3(world.GetTilePos(i, u).x, world.GetTilePos(i, u).y, -0.1f), Quaternion.identity) as GameObject;
                    rock.name = ("Rock");
                    rock.transform.parent = tileObj.transform;
                } else if(ladder <= 1f) {
                    GameObject laddr = Instantiate(ladderPrefab, new Vector3(world.GetTilePos(i, u).x, world.GetTilePos(i, u).y, -0.1f), Quaternion.identity) as GameObject;
                    laddr.name = ("Ladder");
                    laddr.transform.parent = tileObj.transform;
                }
			}
		}
	}

	void ClearWorld ()
	{
		foreach (Transform child in tileParent)
		{
			Debug.Log ("destoyed a tile");
			GameObject.Destroy (child.gameObject);
		}
		world = GM.World;
	}
}
