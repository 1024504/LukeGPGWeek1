using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Perlin3D : MonoBehaviour
{
	public int size = 5;
	public GameObject voxel;
	public float threshold = 0.5f;
	public float amplitude = 10;
	public float waveLength;
	// Randomizers for generating a 3D Perlin noise space. 
	private float randX;
	private float randY;
	private float randZ;

	private void Awake()
	{
		randX = Random.Range(0,1000)/1000f; 
		randY = Random.Range(0,1000)/1000f;
		randZ = Random.Range(0,1000)/1000f;
	}

	// Start is called before the first frame update
    void Start()
    {
	    for (var i = 0; i < size; i++)
	    {
		    for (var j = 0; j < size; j++)
		    {
			    for (var k = 0; k < size; k++)
			    {
				    float perlin3DValue = Mathf.PerlinNoise(Mathf.PerlinNoise(i+randX, j+randY),k+randZ);
				    if (perlin3DValue>threshold)
				    {
					    GameObject go = Instantiate(voxel, gameObject.transform);
					    go.name = i.ToString() + "." + j.ToString() + "." + k.ToString();
					    go.transform.position = new Vector3(i, j, k);
				    }
			    }
		    }
	    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
