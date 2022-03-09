using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManagerWeek5 : MonoBehaviour
{
	// Even numbers are player 1's turn.
	public int playersIndex;
	public int currentRound = 1;
	public int totalRounds = 2;
	public int totalPlayers = 1;
	public int sidesOfDie = 6;
	public int totalDice = 2;
	public GameObject playerPrefab;
	public List<GameObject> players;
	public int[] scores;

	public delegate void StartGameAction(int totalDice);

	public event StartGameAction StartEvent;

	public void StartGame()
	{
		StartEvent?.Invoke(totalDice);
		UIScoreEvent?.Invoke(players);
	}

	public delegate void UIAnnouncementAction(string announcement);

	public event UIAnnouncementAction UIAnnouncementEvent;

	public delegate void UIScoreAction(List<GameObject> players);
	
	public event UIScoreAction UIScoreEvent;


	int Roll()
	{
		int roll = Random.Range(1, sidesOfDie + 1);

		//Reason this is a function is to add in extra effects like spawning/destroying cubes.

		return roll;
	}

	void Awake()
	{
		scores = new int[totalPlayers];
		for (var i=0; i<totalPlayers; i++)
		{
			players.Add(Instantiate(playerPrefab, transform));
		}
	}
	
	void Start()
	{
		StartGame();
		UIAnnouncementEvent?.Invoke("Player 1 goes first.");
		UIAnnouncementEvent?.Invoke("Press 'Space' to roll.");
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (currentRound <= totalRounds)
			{
				if (players[playersIndex].GetComponent<PlayerWeek5>().isDiceLocked)
				{
					if (players[playersIndex].GetComponent<PlayerWeek5>().noOfDiceLock < totalDice)
					{
						for (int j = totalDice-1; j >= players[playersIndex].GetComponent<PlayerWeek5>().noOfDiceLock; j--)
						{
							players[playersIndex].GetComponent<PlayerWeek5>().diceRolls[j] = Roll();
							
						}
						scores[playersIndex] = players[playersIndex].GetComponent<PlayerWeek5>().diceRolls.Sum();
						UIScoreEvent?.Invoke(players);
					}
					
					//message to say current score
					UIAnnouncementEvent?.Invoke("Player " + (playersIndex+1) + " has a score of " +
					                            scores[playersIndex]+".");
					
					players[playersIndex].GetComponent<PlayerWeek5>().isDiceLocked = false;
					playersIndex++;
					if (playersIndex == totalPlayers)
					{
						playersIndex = 0;
						currentRound++;
						//display current turn
						UIAnnouncementEvent?.Invoke("Round "+currentRound+".");
						if (currentRound > totalRounds)
						{
							// Game Over
							UIAnnouncementEvent?.Invoke("Game Over");
							var winnerIndex = 0;
							var winnerScore = 0;
							for (var i=0; i<totalPlayers; i++)
							{
								UIAnnouncementEvent?.Invoke("Player "+(i+1)+"'s score is: "+scores[i]+".");
								//Just learned continue
								if (scores[i] <= winnerScore) continue;
								winnerScore = scores[i];
								winnerIndex = i;
							}
							//display score and offer to start again.
							UIAnnouncementEvent?.Invoke("Player "+(winnerIndex+1)+" is the winner with a score of "+winnerScore);
						}
					}

					if (currentRound <= totalRounds)
					{
						//display whose turn it is
						UIAnnouncementEvent?.Invoke("Player "+(playersIndex+1)+"'s turn.");
						if (players[playersIndex].GetComponent<PlayerWeek5>().isDiceLocked)
						{
							UIAnnouncementEvent?.Invoke("Press 'Space' to roll.");
						}
						else
						{
							UIAnnouncementEvent?.Invoke("'Arrows' to lock dice. 'Space' to confirm.");
						}
					}
				}
				else
				{
					players[playersIndex].GetComponent<PlayerWeek5>().isDiceLocked = true;
					//state player's dice are locked and how many
					UIAnnouncementEvent?.Invoke("Player "+(playersIndex+1)+" has locked "+players[playersIndex].GetComponent<PlayerWeek5>().noOfDiceLock+" dice. 'Space' to roll.");
				}
			}
			else
			{
				//resetting game
				for (int i = 0; i < totalPlayers; i++)
				{
					players[i].GetComponent<PlayerWeek5>().diceRolls = new int[totalDice];
				}

				currentRound = 1;
				foreach (var t in players)
				{
					t.GetComponent<PlayerWeek5>().noOfDiceLock =0;
					t.GetComponent<PlayerWeek5>().isDiceLocked = true;
				}
				scores = new int[totalPlayers];
				UIAnnouncementEvent?.Invoke("Game Reset.");
				UIScoreEvent?.Invoke(players);
				//update scoreboard***
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (!players[playersIndex].GetComponent<PlayerWeek5>().isDiceLocked)
			{
				if (players[playersIndex].GetComponent<PlayerWeek5>().noOfDiceLock < totalDice)
				{
					players[playersIndex].GetComponent<PlayerWeek5>().noOfDiceLock++;
					//display new number of locked dice
					UIScoreEvent?.Invoke(players);
				}
			}
		}
		
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (!players[playersIndex].GetComponent<PlayerWeek5>().isDiceLocked)
			{
				if (players[playersIndex].GetComponent<PlayerWeek5>().noOfDiceLock > 0)
				{
					players[playersIndex].GetComponent<PlayerWeek5>().noOfDiceLock--;
					//display new number of locked dice
					UIScoreEvent?.Invoke(players);
				}
			}
		}
	}
}