using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// Even numbers are player 1's turn.
	public int state = 0;
	public int[] p1Score;
	public int[] p2Score;
	public int p1Lock = 0;
	public int p2Lock = 0;
	public Vector3 p1A1;
	public Vector3 p1A2;
	public Vector3 p2A1;
	public Vector3 p2A2;
	public GameObject cubePrefab;
	public GameObject[] p1A1Cubes;
	public GameObject[] p1A2Cubes;
	public GameObject[] p2A1Cubes;
	public GameObject[] p2A2Cubes;

	int Roll(Vector3 spawnLoc)
	{
		int roll = Random.Range(1,5);
		if (roll > 3)
		{
			//p1A1Cubes = new GameObject[4];
			//p1A1Cubes[3] = Instantiate(cubePrefab, spawnLoc, Quaternion.identity);
		}
		else if (roll > 2)
		{
			
		}
		else if (roll > 1)
		{
			
		}
		else
		{
			
		}
		return roll;
	}

	//int Turn(int player, int locked)
	
	
	
    // Start is called before the first frame update
    void Start()
    {
	    p1Score = new int[2];
	    p2Score = new int[2];
	    p1A1 = new Vector3(-8.13000011f,-3.75999999f,-13.2200003f);
	    p1A2 = new Vector3(-22.6900005f,-3.75999999f,-13.2200003f);
	    p2A1 = new Vector3(-8.13000011f,-3.75999999f,-13.2200003f);
	    p2A2 = new Vector3(-22.6900005f,-3.75999999f,4.44000006f);
	    Debug.Log("Player 1 goes first.\nPress 'Space' to roll.");
    }

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
		    if (state == 0)
		    {
			    p1Score[0] = Roll(p1A1);
			    p1Score[1] = Roll(p1A2);
			    Debug.Log("Player 1 rolled "+p1Score[0]+" and "+p1Score[1]+".\nPlayer 1's score is " + (p1Score[0] + p1Score[1]));
			    state += 1;
			    Debug.Log("Player 2's turn.\nPress 'Enter' to roll.");
		    }
		    else if (state == 2)
		    {
			    if (p1Lock == 2)
			    {
				    Debug.Log("Player 1's score is " + (p1Score[0] + p1Score[1]));
				    state += 1;
				    Debug.Log("Player 2's turn.\nChoose to lock rolls: neither, first, or both.");
				    Debug.Log("Press 0, 1, or 2 to lock rolls.\nCurrently "+p1Lock+" rolls are locked.");
			    }
			    else if (p1Lock == 1)
			    {
				    p1Score[1] = Roll(p1A2);
				    Debug.Log("Player 1 rolled "+p1Score[1]+".\nPlayer 1's score is " + (p1Score[0] + p1Score[1]));
				    state += 1;
				    Debug.Log("Player 2's turn.\nChoose to lock rolls: neither, first, or both.");
				    Debug.Log("Press 0, 1, or 2 to lock rolls.\nCurrently "+p1Lock+" rolls are locked.");

			    }
			    else
			    {
				    p1Score[0] = Roll(p1A1);
				    p1Score[1] = Roll(p1A2);
				    Debug.Log("Player 1 rolled "+p1Score[0]+" and "+p1Score[1]+".\nPlayer 1's score is " + (p1Score[0] + p1Score[1]));
				    state += 1;
				    Debug.Log("Player 2's turn.\nChoose to lock rolls: neither, first, or both.");
				    Debug.Log("Press 0, 1, or 2 to lock rolls.\nCurrently "+p1Lock+" rolls are locked.");
			    }
		    }
		    else if (state == 5)
		    {
			    p1Lock = 0;
			    p2Lock = 0;
			    state = 0;
			    Debug.Log("Player 1 goes first.\nPress 'Space' to roll.");
		    }
	    }

	    if (Input.GetKeyDown(KeyCode.KeypadEnter)||Input.GetKeyDown(KeyCode.Return))
	    {
		    if (state == 1)
		    {
			    p2Score[0] = Roll(p1A1);
			    p2Score[1] = Roll(p1A2);
			    Debug.Log("Player 2 rolled "+p2Score[0]+" and "+p2Score[1]+".\nPlayer 2's score is " + (p2Score[0] + p2Score[1]));
			    state += 1;
			    Debug.Log("Player 1's turn.\nChoose to lock rolls: neither, first, or both.");
			    Debug.Log("Press 0, 1, or 2 to lock rolls.\nCurrently "+p1Lock+" rolls are locked.");
		    }
		    else if (state == 3)
		    {
			    if (p2Lock == 2)
			    {
				    Debug.Log("Player 2's score is " + (p2Score[0] + p2Score[1]));
				    state += 1;
			    }
			    else if (p2Lock == 1)
			    {
				    p2Score[1] = Roll(p1A2);
				    Debug.Log("Player 2 rolled "+p2Score[1]+".\nPlayer 2's score is " + (p2Score[0] + p2Score[1]));
				    state += 1;
			    }
			    else
			    {
				    p2Score[0] = Roll(p1A1);
				    p2Score[1] = Roll(p1A2);
				    Debug.Log("Player 2 rolled "+p2Score[0]+" and "+p2Score[1]+".\nPlayer 2's score is " + (p2Score[0] + p2Score[1]));
				    state += 1;
			    }
			    if ((p2Score[0] + p2Score[1]) > (p1Score[0] + p1Score[1]))
			    {
				    Debug.Log("Game Over. Player 2 wins!\nPress 'Space' to reset game.");

			    }
			    else if ((p2Score[0] + p2Score[1]) < (p1Score[0] + p1Score[1]))
			    {
				    Debug.Log("Game Over. Player 1 wins!\nPress 'Space' to reset game.");

			    }
			    else
			    {
				    Debug.Log("Game Over. The score was a tie!\nPress 'Space' to reset game.");
			    }
		    }
		    
	    }

	    if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
	    {
		    if (state == 2)
		    {
			    p1Lock = 0;
			    Debug.Log(p1Lock+" rolls are locked.\nPress 'Space' to roll, or choose again.");
		    }
		    else if (state == 3)
		    {
			    p2Lock = 0;
			    Debug.Log(p2Lock+" rolls are locked.\nPress 'Space' to roll, or choose again.");
		    }
	    }
	    
	    if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
	    {
		    if (state == 2)
		    {
			    p1Lock = 1;
			    Debug.Log(p1Lock+" rolls are locked.\nPress 'Space' to roll, or choose again.");
		    }
		    else if (state == 3)
		    {
			    p2Lock = 1;
			    Debug.Log(p2Lock+" rolls are locked.\nPress 'Space' to roll, or choose again.");
		    }
	    }
	    
	    if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
	    {
		    if (state == 2)
		    {
			    p1Lock = 2;
			    Debug.Log(p1Lock+" rolls are locked.\nPress 'Space' to roll, or choose again.");
		    }
		    else if (state == 3)
		    {
			    p2Lock = 2;
			    Debug.Log(p2Lock+" rolls are locked.\nPress 'Space' to roll, or choose again.");
		    }
	    }
    }
}
