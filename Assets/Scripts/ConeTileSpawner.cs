using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeTileSpawner : MonoBehaviour
{
	// Prefab for each square of the world. Height will be changed to fit a 2D Perlin Noise wave.
	public GameObject worldTile;
	public float tileWidth = 1f;
	public int initialSpawnPoint = 10;//this extends in both directions, so consider it double
	public float amplitude = 10f;
	public float waveLength = 10f;
	public float noiseTranslation = 6543132f;
	
    // Start is called before the first frame update

    private void SpawnMap()
    {
	    for (var i = 0; i*tileWidth <= 2*initialSpawnPoint; i++)
	    {
		    for (var j = 0; j*tileWidth <= 2*initialSpawnPoint; j++)
		    {
			    GameObject go = Instantiate(worldTile, gameObject.transform);
			    go.GetComponent<ConeTile>().amplitude = amplitude;
			    go.GetComponent<ConeTile>().waveLength = waveLength;
			    go.GetComponent<ConeTile>().terrainEdgeCoord = initialSpawnPoint;
			    go.GetComponent<ConeTile>().tileWidth = tileWidth;
			    // if you set the y position to 1/2 the scaled perlin noise, you get a flat underside.
			    go.transform.position = new Vector3(i-initialSpawnPoint, amplitude*Mathf.PerlinNoise(i*tileWidth+noiseTranslation/waveLength, j*tileWidth+noiseTranslation/waveLength), j-initialSpawnPoint);
		    }
	    }
    }
    
    void Start()
    {
	    SpawnMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
