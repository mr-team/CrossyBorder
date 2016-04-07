using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour
{
	GameMaster GM;
	World world;
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
				if (world.Tiles [i, u].Deadly)
				{
					GameObject tileObj = Instantiate (tileGraphic [1], new Vector2 (world.GetTilePos (i, u).x, world.GetTilePos (i, u).y), Quaternion.identity) as GameObject;
					tileObj.name = ("Tile: " + i + " , " + u);
				} else if (!world.Tiles [i, u].Walkable)
				{
					GameObject tileObj = Instantiate (tileGraphic [1], new Vector2 (world.GetTilePos (i, u).x, world.GetTilePos (i, u).y), Quaternion.identity) as GameObject;
					tileObj.name = ("Tile: " + i + " , " + u);
				} else
				{
					GameObject tileObj = Instantiate (tileGraphic [0], new Vector2 (world.GetTilePos (i, u).x, world.GetTilePos (i, u).y), Quaternion.identity) as GameObject;
					tileObj.name = ("Tile: " + i + " , " + u);

				}
			}
		}
	}
}
