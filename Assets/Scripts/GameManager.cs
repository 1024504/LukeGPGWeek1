using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int indexNumber;
	public int[] diceRolls;
	private int dieLocked;
	//public Vector3 firstDieLocation;
}

public class GameManager : MonoBehaviour
{
	// Even numbers are player 1's turn.
	public int playerIndex;
	public int totalPlayers = 1;
	public int sidesOfDie = 1;
	public int totalDice = 1;
	public Player[] player;
	
	int Roll()
	{
		int roll = Random.Range(1,sidesOfDie+1);
		
		//Reason this is a function is to add in extra effects like spawning/destroying cubes.
		
		return roll;
	}

	// Start is called before the first frame update
    void Start()
    {
	    player = new Player[totalPlayers];
	    for (int i=0; i<totalPlayers;i++)
	    {
		    player[i].indexNumber = i;
		    player[i].diceRolls = new int[totalDice];
	    }
	    
	    Debug.Log("Player 1 goes first.\nPress 'Space' to roll.");
    }

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
		    for (int i = 0; i < totalDice; i++)
		    {
			    player[playerIndex].diceRolls[i] = Roll();
			    
		    }

		    if (playerIndex == 0)
		    {
			    p1Score[0] = Roll(p1A1);
			    p1Score[1] = Roll(p1A2);
			    Debug.Log("Player 1 rolled "+p1Score[0]+" and "+p1Score[1]+".\nPlayer 1's score is " + (p1Score[0] + p1Score[1]));
			    playerIndex += 1;
			    Debug.Log("Player 2's turn.\nPress 'Enter' to roll.");
		    }
		    else if (playerIndex == 2)
		    {
			    if (p1Lock == 2)
			    {
				    Debug.Log("Player 1's score is " + (p1Score[0] + p1Score[1]));
				    playerIndex += 1;
				    Debug.Log("Player 2's turn.\nChoose to lock rolls: neither, first, or both.");
				    Debug.Log("Press 0, 1, or 2 to lock rolls.\nCurrently "+p1Lock+" rolls are locked.");
			    }
			    else if (p1Lock == 1)
			    {
				    p1Score[1] = Roll(p1A2);
				    Debug.Log("Player 1 rolled "+p1Score[1]+".\nPlayer 1's score is " + (p1Score[0] + p1Score[1]));
				    playerIndex += 1;
				    Debug.Log("Player 2's turn.\nChoose to lock rolls: neither, first, or both.");
				    Debug.Log("Press 0, 1, or 2 to lock rolls.\nCurrently "+p1Lock+" rolls are locked.");

			    }
			    else
			    {
				    p1Score[0] = Roll(p1A1);
				    p1Score[1] = Roll(p1A2);
				    Debug.Log("Player 1 rolled "+p1Score[0]+" and "+p1Score[1]+".\nPlayer 1's score is " + (p1Score[0] + p1Score[1]));
				    playerIndex += 1;
				    Debug.Log("Player 2's turn.\nChoose to lock rolls: neither, first, or both.");
				    Debug.Log("Press 0, 1, or 2 to lock rolls.\nCurrently "+p1Lock+" rolls are locked.");
			    }
		    }
		    else if (playerIndex == 5)
		    {
			    p1Lock = 0;
			    p2Lock = 0;
			    playerIndex = 0;
			    Debug.Log("Player 1 goes first.\nPress 'Space' to roll.");
		    }
	    }

	    if (Input.GetKeyDown(KeyCode.KeypadEnter)||Input.GetKeyDown(KeyCode.Return))
	    {
		    if (playerIndex == 1)
		    {
			    p2Score[0] = Roll(p1A1);
			    p2Score[1] = Roll(p1A2);
			    Debug.Log("Player 2 rolled "+p2Score[0]+" and "+p2Score[1]+".\nPlayer 2's score is " + (p2Score[0] + p2Score[1]));
			    playerIndex += 1;
			    Debug.Log("Player 1's turn.\nChoose to lock rolls: neither, first, or both.");
			    Debug.Log("Press 0, 1, or 2 to lock rolls.\nCurrently "+p1Lock+" rolls are locked.");
		    }
		    else if (playerIndex == 3)
		    {
			    if (p2Lock == 2)
			    {
				    Debug.Log("Player 2's score is " + (p2Score[0] + p2Score[1]));
				    playerIndex += 1;
			    }
			    else if (p2Lock == 1)
			    {
				    p2Score[1] = Roll(p1A2);
				    Debug.Log("Player 2 rolled "+p2Score[1]+".\nPlayer 2's score is " + (p2Score[0] + p2Score[1]));
				    playerIndex += 1;
			    }
			    else
			    {
				    p2Score[0] = Roll(p1A1);
				    p2Score[1] = Roll(p1A2);
				    Debug.Log("Player 2 rolled "+p2Score[0]+" and "+p2Score[1]+".\nPlayer 2's score is " + (p2Score[0] + p2Score[1]));
				    playerIndex += 1;
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
		    if (playerIndex == 2)
		    {
			    p1Lock = 0;
			    Debug.Log(p1Lock+" rolls are locked.\nPress 'Space' to roll, or choose again.");
		    }
		    else if (playerIndex == 3)
		    {
			    p2Lock = 0;
			    Debug.Log(p2Lock+" rolls are locked.\nPress 'Space' to roll, or choose again.");
		    }
	    }
	    
	    if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
	    {
		    if (playerIndex == 2)
		    {
			    p1Lock = 1;
			    Debug.Log(p1Lock+" rolls are locked.\nPress 'Space' to roll, or choose again.");
		    }
		    else if (playerIndex == 3)
		    {
			    p2Lock = 1;
			    Debug.Log(p2Lock+" rolls are locked.\nPress 'Space' to roll, or choose again.");
		    }
	    }
	    
	    if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
	    {
		    if (playerIndex == 2)
		    {
			    p1Lock = 2;
			    Debug.Log(p1Lock+" rolls are locked.\nPress 'Space' to roll, or choose again.");
		    }
		    else if (playerIndex == 3)
		    {
			    p2Lock = 2;
			    Debug.Log(p2Lock+" rolls are locked.\nPress 'Space' to roll, or choose again.");
		    }
	    }
    }
}
