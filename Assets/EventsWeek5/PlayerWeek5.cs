using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeek5 : MonoBehaviour
{
	public GameManagerWeek5 gameManager;
	public int[] diceRolls;
	public bool isDiceLocked = true;
	public int noOfDiceLock;

	public void ResetScore(int totalDice)
	{
		diceRolls = new int[totalDice];
	}

	void OnEnable()
	{
		gameManager = transform.parent.GetComponent<GameManagerWeek5>();
		gameManager.StartEvent += ResetScore;
	}

	void OnDisable()
	{
		gameManager.StartEvent -= ResetScore;
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
