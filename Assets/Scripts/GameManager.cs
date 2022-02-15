using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int playerNumber;
	public int[] diceRolls;
	public bool isDiceLocked = true;
	public int noOfDiceLock;
}

public class GameManager : MonoBehaviour
{
	// Even numbers are player 1's turn.
	public int playerIndex;
	public int gameTurn = 1;
	public int totalTurns = 2;
	public int totalPlayers = 1;
	public int sidesOfDie = 1;
	public int totalDice = 1;
	public Player[] player;

	int Roll()
	{
		int roll = Random.Range(1, sidesOfDie + 1);

		//Reason this is a function is to add in extra effects like spawning/destroying cubes.

		return roll;
	}

	// Start is called before the first frame update
	void Start()
	{
		player = new Player[totalPlayers];
		for (int i = 0; i < totalPlayers; i++)
		{
			player[i].playerNumber = i+1;
			player[i].diceRolls = new int[totalDice];
		}

		Debug.Log("Player 1 goes first.\nPress 'Space' to roll.");
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (gameTurn <= totalTurns)
			{
				if (player[playerIndex].isDiceLocked)
				{
					if (player[playerIndex].noOfDiceLock > 0)
					{
						// rolls dice for that player
						for (int j = 0; j < player[playerIndex].noOfDiceLock; j++)
						{
							player[playerIndex].diceRolls[j] = Roll();
						}

						Debug.Log("Player " + player[playerIndex].playerNumber + " rolled a total of " +
						          player[playerIndex].diceRolls.Sum());
					}
					
					player[playerIndex].isDiceLocked = false;
					playerIndex++;
					if (playerIndex == totalPlayers)
					{
						playerIndex = 0;
						gameTurn++;
						if (gameTurn > totalTurns)
						{
							// Game Over
							player[0].isDiceLocked = true;
							
							//display score and offer to start again.
						}
					}
				}
				else
				{
					player[playerIndex].isDiceLocked = true;
				}
			}
			else
			{
				for (int i = 0; i < totalPlayers; i++)
				{
					player[i].diceRolls = new int[totalDice];
				}

				gameTurn = 1;
				player[0].isDiceLocked = false;
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (!player[playerIndex].isDiceLocked)
			{
				if (player[playerIndex].noOfDiceLock < totalDice)
				{
					player[playerIndex].noOfDiceLock++;
				}
			}
		}
		
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (!player[playerIndex].isDiceLocked)
			{
				if (player[playerIndex].noOfDiceLock < 0)
				{
					player[playerIndex].noOfDiceLock--;
				}
			}
		}
	}
}