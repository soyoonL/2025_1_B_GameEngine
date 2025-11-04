using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseVoxelMap : MonoBehaviour
{
    public GameObject grassPrefab;
    public GameObject soilPrefab;
    public GameObject waterPrefab;

    public int width = 20;
    public int depth = 20;
    public int maxHeight = 16; 

    [SerializeField] float noiseScale = 20f;
    public int waterLevel = 4;

    void Start()
    {
        float offsetX = Random.Range(-9999f, 9999f);
        float offsetZ = Random.Range(-9999f, 9999f);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                float nx = (x + offsetX) / noiseScale;
                float nz = (z + offsetZ) / noiseScale;

                float noise = Mathf.PerlinNoise(nx, nz);

                int h = Mathf.FloorToInt(noise * maxHeight);

                for (int y=0; y <= h; y++)
                {
                    GameObject prefab = (y==h) ? grassPrefab : soilPrefab;
                    Place(prefab, x, y, z);
                }

                if(h < waterLevel)
                {
                    for(int y = h+1;y <= waterLevel; y++)
                    {
                        Place(waterPrefab,x,y,z);
                    }
                }
            }
        }
    }

    private void Place(GameObject prefab, int x, int y, int z)
    {
        if (prefab == null) return;

        var go = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"{prefab.name}_{x}_{y}_{z}";
    }

}
