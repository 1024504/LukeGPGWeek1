using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierTest : MonoBehaviour
{
	public GameObject prefab;
	public List<GameObject> controlPoints;
	public List<Vector3> cPPositions;
	public int numberOfCPs = 10;
	public float spawnRange = 100;
	public float duration = 1;
	public float smoothness = 60;

	private IEnumerator RunBezier()
	{
		gameObject.transform.position = Bezier.BezierFunction(cPPositions, 0);
		for (float t = 1; t < duration*smoothness; t++)
		{
			Vector3 temp = gameObject.transform.position;
			for (var i = 0; i < controlPoints.Count; i++)
			{
				cPPositions[i] = controlPoints[i].transform.position;
			}
			gameObject.transform.position = Bezier.BezierFunction(cPPositions, t/(duration*smoothness));
			Debug.DrawLine(temp, gameObject.transform.position, Color.green, duration*2);
			
			yield return new WaitForSeconds(1/smoothness);
		}
		gameObject.transform.position = Bezier.BezierFunction(cPPositions, 1);
	}
	
	private IEnumerator RunBezierReverse()
	{
		gameObject.transform.position = Bezier.BezierFunction(cPPositions, 1);
		for (float t = duration*smoothness-1; t > 0; t--)
		{
			Vector3 temp = gameObject.transform.position;
			for (var i = 0; i < controlPoints.Count; i++)
			{
				cPPositions[i] = controlPoints[i].transform.position;
			}
			gameObject.transform.position = Bezier.BezierFunction(cPPositions, t/(duration*smoothness));
			Debug.DrawLine(temp, gameObject.transform.position, Color.green, duration*2);
			
			yield return new WaitForSeconds(1/smoothness);
		}
		gameObject.transform.position = Bezier.BezierFunction(cPPositions, 0);
	}

	void Awake()
	{
		controlPoints = new (numberOfCPs);
	}
	
	
	// Start is called before the first frame update
    void Start()
    {
	    cPPositions.Capacity = (controlPoints.Capacity);
	    for (var i = 0; i < controlPoints.Capacity; i++)
	    {
		    controlPoints.Add(Instantiate(prefab));
		    controlPoints[i].transform.position = new Vector3(Random.Range(-spawnRange,spawnRange), Random.Range(-spawnRange,spawnRange), Random.Range(-spawnRange,spawnRange));
		    cPPositions.Add(controlPoints[i].transform.position);
	    }
    }

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
		    StartCoroutine(RunBezier());
	    }
	    
	    if (Input.GetKeyDown(KeyCode.KeypadEnter))
	    {
		    StartCoroutine(RunBezierReverse());
	    }

	    if (Input.GetKeyDown(KeyCode.R))
	    {
		    for (var i = 0; i < controlPoints.Capacity; i++)
		    {
			    controlPoints[i].transform.position = new Vector3(Random.Range(-spawnRange,spawnRange), Random.Range(-spawnRange,spawnRange), Random.Range(-spawnRange,spawnRange));
			    cPPositions.Add(controlPoints[i].transform.position);
		    }
	    }
    }
}