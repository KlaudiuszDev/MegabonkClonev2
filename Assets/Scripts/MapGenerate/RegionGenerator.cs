using System.Collections.Generic;
using UnityEngine;

public class RegionGenerator 
{
    private static readonly int[] dx = { 1, -1, 0, 0 };
    private static readonly int[] dz = { 0, 0, 1, -1 };
    private int currentRegionID;
    public void GenerateRegions(MapData mapData)
    {
        currentRegionID = 0;
        mapData.RegionSizes.Clear();
        mapData.RegionTiles.Clear();

        for (int x = 0; x < mapData.Width; x++)
            for (int z = 0; z < mapData.Depth; z++)
                mapData.RegionMap[x, z] = -1;

        for (int x = 0; x < mapData.Width; x++)
        {
            for (int z = 0; z < mapData.Depth; z++)
            {
                if (mapData.RegionMap[x, z] == -1)
                {
                    FloodFillRegion(mapData, x, z, currentRegionID, mapData.HeightMap[x, z]);
                    currentRegionID++;
                }
            }
        }
    }

    private void FloodFillRegion(MapData mapData, int startX, int startZ, int regionID, int targetHeight)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(new Vector2Int(startX, startZ));
        mapData.RegionMap[startX, startZ] = regionID;

        mapData.RegionTiles[regionID] = new List<Vector2Int>();
        mapData.RegionTiles[regionID].Add(new Vector2Int(startX, startZ));

        int regionSize = 1;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                int nx = current.x + dx[i];
                int nz = current.y + dz[i];

                if (nx >= 0 && nx < mapData.Width && nz >= 0 && nz < mapData.Depth)
                {
                    if (mapData.RegionMap[nx, nz] == -1 && mapData.HeightMap[nx, nz] == targetHeight)
                    {
                        mapData.RegionMap[nx, nz] = regionID;
                        queue.Enqueue(new Vector2Int(nx, nz));
                        regionSize++;
                        mapData.RegionTiles[regionID].Add(new Vector2Int(nx, nz));
                    }
                }
            }
        }

        mapData.RegionSizes[regionID] = regionSize;
    }
}
