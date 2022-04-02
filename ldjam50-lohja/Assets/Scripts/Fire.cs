using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fire
{
    public Fire[,] fires;
    private Tilemap fireMap;
    private Vector3Int position;

    public int intensity;
    public List<Fire> neighbours;

    private static TileBase fireTileSmall;
    private static TileBase fireTileMedium;
    private static TileBase fireTileBig;

    public Fire(Fire[,] fires, Vector3Int position, Tilemap fireMap)
    {
        this.fires = fires;
        this.position = position;
        this.fireMap = fireMap;
    }

    public static void SetFireTiles(TileBase small, TileBase medium, TileBase big)
    {
        fireTileSmall = small;
        fireTileMedium = medium;
        fireTileBig = big;
    }

    public void StartFire()
    {
        fireMap.SetTile(position, fireTileSmall);
    }

    public void StopFire()
    {
        fireMap.SetTile(position, null);
    }

    public void FindNeighbours()
    {
        neighbours = new List<Fire>();
        for (int y = position.y - 1; y <= position.y + 1; y++)
        {
            for (int x = position.x - 1; x <= position.x + 1; x++)
            {
                if (IsInBounds(x, y))
                {
                    Fire fire = fires[y, x];

                    if (fire != null && (y != position.y || x != position.x))
                    {
                        neighbours.Add(fire);
                    }
                }
            }
        }
    }

    public void PrintNeighbours()
    {
        Debug.Log(neighbours.Count);

        if (neighbours.Count == 0)
        {
            Debug.Log(position);
        }
    }

    private bool IsInBounds(int x, int y)
    {
        return x < fires.GetLength(1) && x >= 0 && y < fires.GetLength(0) && y >= 0;
    }
}
