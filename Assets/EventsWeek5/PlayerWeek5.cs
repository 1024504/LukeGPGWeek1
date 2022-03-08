using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeek5 : MonoBehaviour
{
	public EventManagerWeek5 EM;
	public int[] diceRolls;
	public bool isDiceLocked = true;
	public int noOfDiceLock;
	public int[] score;

	public void ResetScore(int totalDice)
	{
		score = new int[totalDice];
	}

	void OnEnable()
	{
		EventManagerWeek5.StartEvent += ResetScore;
	}

	void OnDisable()
	{
		EventManagerWeek5.StartEvent -= ResetScore;
	}
	
    // Start is called before the first frame update void Start()
    void Start()
    {
	    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
