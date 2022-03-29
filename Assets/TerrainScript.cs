using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
	public int depth = 20;
	public int length = 256;
	public int width = 256;
	public float frequency = 20f;
	public float amplitude = 1f;

	TerrainData GenerateTerrain(TerrainData terrainData)
	{
		terrainData.heightmapResolution = width + 1;
		
		terrainData.size = new Vector3(width, depth, length);
		
		terrainData.SetHeights(0,0,GenerateHeights());

		return terrainData;
	}

	float[,] GenerateHeights()
	{
		float[,] heights = new float[width, length];
		for (int i=0; i<width; i++)
		{
			for (int j=0; j < length; j++)
			{
				heights[i, j] = CalculateHeight(i, j);	
			}
		}

		return heights;
	}

	float CalculateHeight(int x, int y)
	{
		float xCoord = (float) x / width*frequency;
		float ycoord = (float) y / length*frequency;

		return amplitude * Mathf.PerlinNoise( xCoord,  ycoord);
	}


	// Start is called before the first frame update
    void Update()
    {
	    Terrain terrain = GetComponent<Terrain>();
	    terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    // Update is called once per frame
    
}
