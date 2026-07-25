using UnityEngine;

public class WorldBuilder
{
    private GameObject blockPrefab;
    private Transform blocksParent;

    public WorldBuilder(GameObject prefab, Transform parent)
    {
        blockPrefab = prefab;
        blocksParent = parent;
    }

    public void BuildWorld(MapData mapData)
    {
        for(int x = 0; x < mapData.Width; x++)
        {
            for (int z = 0; z < mapData.Depth; z++)
            {
                int currentHeight = mapData.HeightMap[x, z];

                for (int y = 0; y <= currentHeight; y++)
                {
                    bool isVisible = false;

                    if (y == currentHeight) isVisible = true;
                    else
                    {
                        if (x > 0 && mapData.HeightMap[x - 1, z] < y) isVisible = true;
                        if (x < mapData.Width - 1 && mapData.HeightMap[x + 1, z] < y) isVisible = true;
                        if (z > 0 && mapData.HeightMap[x, z - 1] < y) isVisible = true;
                        if (z < mapData.Depth - 1 && mapData.HeightMap[x, z + 1] < y) isVisible = true;
                    }

                    if (isVisible)
                    {
                        Vector3 blockPos = new Vector3(x * mapData.BlockSize, currentHeight * mapData.BlockSize, z * mapData.BlockSize);
                        GameObject.Instantiate(blockPrefab, blockPos, Quaternion.identity, blocksParent);
                    }
                }
            }
        }
    }
}
