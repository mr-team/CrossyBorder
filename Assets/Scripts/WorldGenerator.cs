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

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		GM.onNextRound += ClearWorld;
		GM.onNextRound += DrawWorld;
		rand = new System.Random (GM.seed.GetHashCode ());
		DrawWorld ();
	}

	void DrawWorld ()
	{
		Debug.Log ("drew a new world");
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
					GameObject laddr = Instantiate (ladderPrefab, new Vector3 (GM.World.GetTilePos (i, u).x, GM.World.GetTilePos (i, u).y, -0.1f), Quaternion.identity) as GameObject;
					laddr.name = ("Ladder");
					GM.maxLadder++;
					laddr.transform.parent = tileObj.transform;
				} else if (GM.mines && mineRan >= 2f && mineRan <= 3f)
				{
					
					GameObject mine = Instantiate (mineprefab, new Vector3 (GM.World.GetTilePos (i, u).x, GM.World.GetTilePos (i, u).y, -0.1f), Quaternion.identity) as GameObject;
					mine.name = ("mine");
					mine.transform.parent = tileObj.transform;
					
				} else if (GM.bearTraps && BTrand >= 2f && BTrand <= 3f)
				{
					
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
		
		foreach (Transform child in tileParent)
		{
			GameObject.Destroy (child.gameObject);
		}
	}
}
