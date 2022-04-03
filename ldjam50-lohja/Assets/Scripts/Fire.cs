using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fire
{
    public World world;
    public Fire[,] fires;
    private Tilemap fireMap;
    private Vector3Int position;
    private Vector3Int offset;

    public float intensity;
    public float buildupSpeed = 0.05f;
    public List<Fire> neighbours;

    private static List<TileBase> fireTileSmall;
    private static List<TileBase> fireTileMedium;
    private static List<TileBase> fireTileBig;

    private int tileIndex;

    public bool active;

    public Fire(World world, Vector3Int position, Vector3Int offset, Tilemap fireMap)
    {
        this.world = world;
        this.fires = world.fires;
        this.position = position;
        this.fireMap = fireMap;
        this.offset = offset;
        intensity = 0;
        active = false;
    }

    public static void SetFireTiles(List<TileBase> small, List<TileBase> medium, List<TileBase> big)
    {
        fireTileSmall = small;
        fireTileMedium = medium;
        fireTileBig = big;
    }

    public void Tick()
    {
        if (active)
        {
            intensity = Mathf.Min(intensity + buildupSpeed, 1);
            UpdateTile();
            UpdateNeighbours();
        }
    }

    private void UpdateTile()
    {
        TileBase newTile = fireTileSmall[tileIndex];

        if (intensity > 0.7f)
        {
            newTile = fireTileBig[tileIndex];
        }
        else if (intensity > 0.4f)
        {
            newTile = fireTileMedium[tileIndex];
        }

        fireMap.SetTile(position + offset, newTile);
    }

    private void UpdateNeighbours()
    {
        int neighboursOnFire = 0;
        foreach (Fire fire in neighbours)
        {
            if (fire.active)
            {
                neighboursOnFire++;
            }
        }

        bool allNeighboursOnFire = neighboursOnFire == neighbours.Count;

        if (!allNeighboursOnFire)
        {
            bool setOnFire = SetNeighbourOnFire();

            if (setOnFire)
            {
                int neighbourIndex = Random.Range(0, neighbours.Count);

                neighbours[neighbourIndex].StartFire();
            }
        }
    }

    private bool SetNeighbourOnFire()
    {
        float fire = Random.Range(intensity * 5, 10);

        return fire > 9.5f;
    }

    public void StartFire()
    {
        tileIndex = Random.Range(0, fireTileSmall.Count);
        fireMap.SetTile(position + offset, fireTileSmall[tileIndex]);
        active = true;
        if (!world.activeFires.Contains(this))
        {
            world.activeFires.Add(this);
        }
    }

    public void Extinguish(float force)
    {
        if (active)
        {
            intensity -= force;
            if (intensity <= 0)
            {
                StopFire();
            }
        }
    }

    public void StopFire()
    {
        fireMap.SetTile(position + offset, null);
        active = false;
        intensity = 0;

        world.activeFires.Remove(this);
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

    public Vector3Int GetTilemapPosition()
    {
        return position + offset;
    }

    public Vector3 GetWorldPosition()
    {
        return fireMap.CellToWorld(GetTilemapPosition());
    }
}
