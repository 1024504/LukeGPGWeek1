using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManagerWeek5 : MonoBehaviour
{
	// Even numbers are player 1's turn.
	public EventManagerWeek5 EM;
	public int currentPlayer;
	public int currentTurn = 1;
	public int totalTurns = 2;
	public int totalPlayers = 1;
	public int sidesOfDie = 6;
	public int totalDice = 2;
	public List<Player> players;

	int Roll()
	{
		int roll = Random.Range(1, sidesOfDie + 1);

		//Reason this is a function is to add in extra effects like spawning/destroying cubes.

		return roll;
	}

	// Start is called before the first frame update
	void Start()
	{
		players = new List<Player>(totalPlayers);

		Debug.Log("Player 1 goes first.\nPress 'Space' to roll.");
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (currentTurn <= totalTurns)
			{
				if (players[currentPlayer].isDiceLocked)
				{
					if (players[currentPlayer].noOfDiceLock > 0)
					{
						// rolls dice for that player
						for (int j = 0; j < players[currentPlayer].noOfDiceLock; j++)
						{
							players[currentPlayer].diceRolls[j] = Roll();
						}

						Debug.Log("Player " + currentPlayer+1 + " rolled a total of " +
						          players[currentPlayer].diceRolls.Sum());
					}
					
					players[currentPlayer].isDiceLocked = false;
					currentPlayer++;
					if (currentPlayer == totalPlayers)
					{
						currentPlayer = 0;
						currentTurn++;
						if (currentTurn > totalTurns)
						{
							// Game Over
							players[0].isDiceLocked = true;
							
							//display score and offer to start again.
						}
					}
				}
				else
				{
					players[currentPlayer].isDiceLocked = true;
				}
			}
			else
			{
				for (int i = 0; i < totalPlayers; i++)
				{
					players[i].diceRolls = new int[totalDice];
				}

				currentTurn = 1;
				players[0].isDiceLocked = false;
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (!players[currentPlayer].isDiceLocked)
			{
				if (players[currentPlayer].noOfDiceLock < totalDice)
				{
					players[currentPlayer].noOfDiceLock++;
				}
			}
		}
		
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (!players[currentPlayer].isDiceLocked)
			{
				if (players[currentPlayer].noOfDiceLock < 0)
				{
					players[currentPlayer].noOfDiceLock--;
				}
			}
		}
	}
}