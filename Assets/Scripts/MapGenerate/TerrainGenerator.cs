using UnityEngine;

public class TerrainGenerator
{
    public void GenerateHeightMap(MapData mapData, float noiseScale, int maxHeight, float seed)
    {
        for (int x = 0; x < mapData.Width; x++)
        {
            for (int z = 0; z < mapData.Depth; z++)
            {
                float xCoord = (x * noiseScale) + seed;
                float zCoord = (z * noiseScale) + seed;
                float noiseValue = Mathf.PerlinNoise(xCoord, zCoord);

                mapData.HeightMap[x, z] = Mathf.Max(1, Mathf.RoundToInt(noiseValue * maxHeight));
            }
        }
        for (int x = 0; x < mapData.Width; x++)
        {
            for (int z = 0; z < mapData.Depth; z++)
            {
                int currentBlock = mapData.HeightMap[x, z];
                bool isPit = true;

                if (x > 0 && mapData.HeightMap[x - 1, z] <= currentBlock) isPit = false;
                if (x < mapData.Width - 1 && mapData.HeightMap[x + 1, z] <= currentBlock) isPit = false;
                if (z > 0 && mapData.HeightMap[x, z - 1] <= currentBlock) isPit = false;
                if (z < mapData.Width - 1 && mapData.HeightMap[x, z + 1] <= currentBlock) isPit = false;

                if (isPit) mapData.HeightMap[x, z]++;
            }
        }
    }
}