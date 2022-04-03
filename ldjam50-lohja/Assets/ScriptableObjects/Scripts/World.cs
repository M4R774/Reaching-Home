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

    public GameObject fireTask;
    public GameObject player;

    public Tilemap groundMap;
    public Tilemap terrainMap;
    public Tilemap objectMap;
    public Tilemap fireMap;

    public List<TileBase> fireTileSmall;
    public List<TileBase> fireTileMedium;
    public List<TileBase> fireTileBig;

    public Fire[,] fires;
    public List<Fire> allFires;
    public List<Fire> activeFires;

    public List<EngineTask> engines = new List<EngineTask>();

    public List<ComputerTask> computers;

    public float computerDamagePerTick = 0.5f;
    public float engineDamagePerTick = 1.0f;
    public float fireDamagePerTick = 0.01f;

    public float maxDamage = 150.0f;

    public float damage = 0;

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
        activeFires = new List<Fire>();
        allFires = new List<Fire>();
        Fire.SetFireTiles(fireTileSmall, fireTileMedium, fireTileBig);
        for (int y = 0; y < groundHeight; y++)
        {
            for (int x = 0; x < groundWidth; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                if (groundMap.HasTile(position + groundOffSet))
                {
                    fires[y, x] = new Fire(this, position, groundOffSet, fireMap);
                    allFires.Add(fires[y, x]);
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
    }

    public void TickDamage()
    {
        float damageAdd = 0;

        foreach(ComputerTask computer in computers)
        {
            if (!computer.healthy)
            {
                damageAdd += computerDamagePerTick;
            }
        }
        foreach (EngineTask engine in engines)
        {
            if (!engine.healthy)
            {
                damageAdd += engineDamagePerTick;
            }
        }
        foreach (Fire fire in activeFires)
        {
            if (fire.active)
            {
                damageAdd += fireDamagePerTick;
            }
        }

        damage += damageAdd;
    }

    public void InitEngines() {
        engines = new List<EngineTask>();
    }

    public void InitComputers() {
        computers = new List<ComputerTask>();
    }

    public void StartFireAtRandomLocation()
    {
        Fire fire = allFires[Random.Range(0, allFires.Count)];
        GameObject newFireTask = Instantiate(fireTask);
        newFireTask.transform.position = fire.GetWorldPosition();
        newFireTask.GetComponent<FireTask>().player = player;
        newFireTask.GetComponent<FireTask>().healthy = false;
        fire.StartFire();
    }

    public void StartFire(Vector3Int position)
    {
        Fire fire = fires[position.y, position.x];
        GameObject newFireTask = Instantiate(fireTask);
        newFireTask.transform.position = fire.GetWorldPosition();
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
        for (int y = Mathf.RoundToInt(bounds.min.y) - groundOffSet.y; y < Mathf.RoundToInt(bounds.max.y) - groundOffSet.y; y++)
        {
            for (int x = Mathf.RoundToInt(bounds.min.x) - groundOffSet.x; x < Mathf.RoundToInt(bounds.max.x) - groundOffSet.x; x++)
            {
                if (y >= 0 && y < fires.GetLength(0) && x >= 0 && x < fires.GetLength(1))
                {
                    Fire fire = fires[y, x];
                    if (fire != null && fire.active)
                    {
                        firesCollided.Add(fire);
                    }
                }
            }
        }

        return firesCollided;
    }
}
