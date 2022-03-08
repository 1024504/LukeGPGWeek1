using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerWeek5 : MonoBehaviour
{
	public delegate void StartGameAction(int totalDice);

	public static event StartGameAction StartEvent;

	public void StartGame(int totalDice)
	{
		StartEvent?.Invoke(totalDice);
	}
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
