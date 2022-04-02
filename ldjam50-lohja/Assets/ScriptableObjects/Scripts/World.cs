using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class World : ScriptableObject
{
    public int groundWidth;
    public int groundHeight;
    public Vector3Int groundOffSet;

    public Tilemap groundMap;
    public Tilemap terrainMap;
    public Tilemap objectMap;
    public Tilemap fireMap;

    public TileBase fireTileSmall;
    public TileBase fireTileMedium;
    public TileBase fireTileBig;

    public Fire[,] fires;

    public void InitMaps(Tilemap groundMap, Tilemap terrainMap, Tilemap objectMap, Tilemap fireMap)
    {
        this.groundMap = groundMap;
        this.terrainMap = terrainMap;
        this.objectMap = objectMap;
        this.fireMap = fireMap;

        groundWidth = groundMap.size.x;
        groundHeight = groundMap.size.y;
        groundOffSet = new Vector3Int((int)groundMap.localBounds.min.x, (int)groundMap.localBounds.min.y, 0);
    }

    public void InitFires()
    {
        fires = new Fire[groundHeight, groundWidth];
        Fire.SetFireTiles(fireTileSmall, fireTileMedium, fireTileBig);
        for (int y = 0; y < groundHeight; y++)
        {
            for (int x = 0; x < groundWidth; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                if (groundMap.HasTile(position + groundOffSet))
                {
                    fires[y, x] = new Fire(fires, position, groundOffSet, fireMap);
                }
            }
        }
        for (int y = 0; y < fires.GetLength(0); y++)
        {
            for (int x = 0; x < fires.GetLength(1); x++)
            {
                Fire fire = fires[y, x];
                if (fire != null)
                {
                    fires[y, x].FindNeighbours();
                }
            }
        }

        fires[20, 20].StartFire();
    }

    public void StartFire(Vector3Int position)
    {
        Fire fire = fires[position.y, position.x];
        fire.StartFire();
    }

    public void StopFire(Vector3Int position)
    {
        Fire fire = fires[position.y, position.x];
        fire.StopFire();
    }

    public void TickFire()
    {
        for (int y = 0; y < fires.GetLength(0); y++)
        {
            for (int x = 0; x < fires.GetLength(1); x++)
            {
                Fire fire = fires[y, x];
                if (fire != null)
                {
                    fires[y, x].Tick();
                }
            }
        }
    }

    public List<Fire> GetAllFiresWithinBounds(Bounds bounds)
    {
        List<Fire> firesCollided = new List<Fire>();
        for (int y = Mathf.RoundToInt(bounds.min.y); y < Mathf.RoundToInt(bounds.max.y); y++)
        {
            for (int x = Mathf.RoundToInt(bounds.min.x); x < Mathf.RoundToInt(bounds.max.x); x++)
            {
                Fire fire = fires[y, x];
                if (fire != null)
                {
                    firesCollided.Add(fire);
                }
            }
        }
    }
}
