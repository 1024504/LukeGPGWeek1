using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
	// Prefab for each square of the world. Height will be changed to fit a 2D Perlin Noise wave.
	public GameObject worldTile;
	public int terrainSpawnRange = 10;
	public float amplitude = 10;
	public float waveLength = 10;
	
    // Start is called before the first frame update
    void Start()
    {
	    for (var i = 0; i <= 2*terrainSpawnRange; i++)
	    {
		    for (var j = 0; j <= 2*terrainSpawnRange; j++)
		    {
			    string temp1;
			    string temp2;
			    GameObject go = Instantiate(worldTile, gameObject.transform);
			    if (i < 10)
			    {
				    temp1 = "0" + i.ToString();
			    }
			    else
			    {
				    temp1 = i.ToString();
			    }
			    if (j < 10)
			    {
				    temp2 = "0" + j.ToString();
			    }
			    else
			    {
				    temp2 = j.ToString();
			    }
			    go.name = temp1+temp2;
			    // if you set the y position to 1/2 the scaled perlin noise, you get a flat underside.
			    go.transform.position = new Vector3(i-terrainSpawnRange, 1+amplitude*Mathf.PerlinNoise(i/waveLength, j/waveLength), j-terrainSpawnRange);
			    go.transform.localScale = new Vector3(1, 1+amplitude*Mathf.PerlinNoise(i/waveLength, j/waveLength), 1);
		    }
	    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
