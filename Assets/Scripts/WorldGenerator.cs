using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour
{
	GameMaster GM;
	World world;
    public GameObject sandTileGraphic;
	public GameObject[] tileGraphic = new GameObject[10];

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		world = GM.World;
		DrawWorld ();
	}

	void DrawWorld ()
	{
		for (int i = 0; i < world.Tiles.GetLength (0); i++)
		{
			for (int u = 0; u < world.Tiles.GetLength (1); u++)
			{
                GameObject tileObj = Instantiate(sandTileGraphic, new Vector2(world.GetTilePos(i, u).x, world.GetTilePos(i, u).y), Quaternion.identity) as GameObject;
                tileObj.name = ("Tile: " + i + " , " + u);

                if(!world.Tiles[i, u].Walkable && u == 0) {
                    GameObject rock = Instantiate(tileGraphic[0], new Vector3(world.GetTilePos(i, u).x, world.GetTilePos(i, u).y, -0.1f), Quaternion.identity) as GameObject;
                    rock.name = ("Rock");
                    rock.transform.parent = tileObj.transform;
                }
                else if (!world.Tiles [i, u].Walkable)
				{
					GameObject rock = Instantiate (tileGraphic [Random.Range(0, tileGraphic.Length)], new Vector3(world.GetTilePos (i, u).x, world.GetTilePos (i, u).y, -0.1f), Quaternion.identity) as GameObject;
                    rock.name = ("Rock");
                    rock.transform.parent = tileObj.transform;
				}
			}
		}
	}
}
