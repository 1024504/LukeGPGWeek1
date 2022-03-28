using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class ConeTile : MonoBehaviour
{
	public float tileWidth;
	public float amplitude;
	public float waveLength;
	public float terrainEdgeCoord;
	private ProBuilderMesh mesh;
	private Vector3 position;
	public Vertex[] allVertices;
	public List<Vector3> vertCoords = new(9);
	public List<Vector3> newRelativeVertCoords = new(9);
	private List<int> indices = new() {0, 1, 2, 3, 4, 5, 6, 7, 8};
	public List<int> indicesFromAll;
	
// Start is called before the first frame update
    void Start()
    {
	    mesh = GetComponent<ProBuilderMesh>();
	    position = transform.position;
	    allVertices = mesh.GetVertices();
	    var check = false;
	    vertCoords.Add(allVertices[0].position);
	    indicesFromAll.Add(0);
	    for (var i = 1; i < allVertices.Length; i++)
	    {
		    for (var j = i-1; j >= 0; j--)
		    {
			    if (allVertices[i].position==allVertices[j].position)
			    {
				    check = true;
			    }
		    }
		    if (!check)
		    {
			    vertCoords.Add(allVertices[i].position);
			    indicesFromAll.Add(i);
		    }

		    check = false;
	    }

	    for (var i = 0; i < vertCoords.Count; i++)
	    {
		    newRelativeVertCoords.Add(new Vector3(vertCoords[i].x,
			    amplitude * Mathf.PerlinNoise((position.x+vertCoords[i].x-terrainEdgeCoord) / waveLength, (position.z+vertCoords[i].z-terrainEdgeCoord) / waveLength) - position.y,
			    vertCoords[i].z));
		    mesh.SetSharedVertexPosition(i, newRelativeVertCoords[i]);
	    }
    }

    // Update is called once per frame
    void Update()
    {
	    
    }
}
