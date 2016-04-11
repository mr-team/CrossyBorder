using UnityEngine;
using System.Collections;

public static class Noise {

	public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, string seedText) {
        int seed = Mathf.RoundToInt( seedText.GetHashCode() / 100000f );
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if(scale <= 0)
            scale = 0.0001f;

        for(int y = 0; y < mapHeight; y++) {
            for(int x = 0; x < mapWidth; x++) {
                float sampleX = (x + seed) / scale;
                float sampleY = (y + seed) / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }
        return noiseMap;
    }
}
