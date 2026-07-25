using System.Collections.Generic;
using UnityEngine;

public class MapData
{
    public int Width { get; private set; }
    public int Depth { get; private set; }
    public float BlockSize { get; private set; }
    
    public int[,] HeightMap { get; set; }
    public int[,] RegionMap { get; set; }
    public bool[,] IsOccupied { get; set; }
    public Dictionary<int, List<Vector2Int>> RegionTiles { get; set; }
    public Dictionary<int, int> RegionSizes { get; set; }

    public MapData(int width, int depth, float blockSize)
    {
        Width = width;
        Depth = depth;
        BlockSize = blockSize;
        HeightMap = new int[width, depth];
        RegionMap = new int[width, depth];
        IsOccupied = new bool[width, depth];
        RegionTiles = new Dictionary<int, List<Vector2Int>>();
        RegionSizes = new Dictionary<int, int>();
    }
}
