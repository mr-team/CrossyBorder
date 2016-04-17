using UnityEngine;
using System.Collections;

public class World
{
	Tile[,] tiles;

	int height;
	int width;
	float scale;
	float[,] noiseMap;

	public Tile[,] Tiles {
		get
		{
			return tiles;
		}
	}

	public World (int Width, int Height, float scale)
	{
		height = Height;
		width = Width;
		this.scale = scale;
		tiles = new Tile[width, height];
	}

	public void GenerateWorld (string seedText)
	{
		if (seedText == "")
			seedText = "Matias er " + Random.Range (int.MinValue, int.MaxValue) + " meter høy.";
		noiseMap = Noise.GenerateNoiseMap (width * 10, height * 10, scale, seedText);
		
		for (int i = 0; i < tiles.GetLength (0); i++)
		{
			for (int u = 0; u < tiles.GetLength (1); u++)
			{
				/*if (u == 0)
				{
					tiles [i, u] = new Tile (new Vector2 (i, u));
					tiles [i, u].Walkable = false;
					
				} else if (i == 3 && u == 4)
				{
					tiles [i, u] = new Tile (new Vector2 (i, u));
					tiles [i, u].Walkable = true;
					tiles [i, u].Deadly = true;

				} else if (i == 2 && u == 2)
				{
					tiles [i, u] = new Tile (new Vector2 (i, u));
					tiles [i, u].Walkable = false;
				} else
				{
					tiles [i, u] = new Tile (new Vector2 (i, u));
					tiles [i, u].Walkable = true;
				}*/
				if (u == 0)
				{
					tiles [i, u] = new Tile (new Vector2 (i, u));
					tiles [i, u].Walkable = false;
				} else if (u == 1)
				{
					tiles [i, u] = new Tile (new Vector2 (i, u));
					tiles [i, u].Walkable = true;
				} else if (u == 6)
				{
					tiles [i, u] = new Tile (new Vector2 (i, u));
					tiles [i, u].Walkable = true;
				} else if (u == 8)
				{
					tiles [i, u] = new Tile (new Vector2 (i, u));
					tiles [i, u].Walkable = true;
				} else if (u == 10)
				{
					tiles [i, u] = new Tile (new Vector2 (i, u));
					tiles [i, u].Walkable = true;
				} else if (u == 13)
				{
					tiles [i, u] = new Tile (new Vector2 (i, u));
					tiles [i, u].Walkable = true;
				} else if (u == 16)
				{
					tiles [i, u] = new Tile (new Vector2 (i, u));
					tiles [i, u].Walkable = true;
				} else if (u == 18)
				{
					tiles [i, u] = new Tile (new Vector2 (i, u));
					tiles [i, u].Walkable = true;
				} else
				{
					tiles [i, u] = new Tile (new Vector2 (i, u));
					tiles [i, u].Walkable = noiseMap [i, u] <= 0.65f;
				}
			}
		}
	}

	public Vector2 GetTilePos (int index1, int index2)
	{
		return tiles [index1, index2].TilePos;
	}
}
