using UnityEngine;

public class TerrainPloughing : MonoBehaviour
{
    public Terrain terrain;
    public float depth = 0.8f; // Profundidad de la tierra arada

    void Start()
    {
        if (terrain == null)
        {
            terrain = GetComponent<Terrain>();
        }

        if (terrain != null)
        {
            ArableLand();
        }
        else
        {
            Debug.LogError("Terrain component not found!");
        }
    }

    void ArableLand()
    {
        int xRes = terrain.terrainData.heightmapResolution;
        int zRes = terrain.terrainData.heightmapResolution;

        // Tamaño del área a modificar (200x200)
        int areaSize = Mathf.FloorToInt(200f / (terrain.terrainData.size.x / xRes));

        float[,] heights = terrain.terrainData.GetHeights(0, 0, xRes, zRes);

        for (int x = 0; x < areaSize; x++)
        {
            for (int z = 0; z < areaSize; z++)
            {
                heights[x, z] -= depth;
            }
        }

        terrain.terrainData.SetHeights(0, 0, heights);
    }
}
