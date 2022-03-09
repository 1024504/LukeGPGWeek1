using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ScoreView : MonoBehaviour
{
	public GameManagerWeek5 gameManager;
	public TextMeshProUGUI announcerTextMesh;
	public TextMeshProUGUI scoreTextMesh;
	private List<string> tempAnnouncement = new List<string> {"","","","",""};
	private List<string> tempScores;
	private int noOfDice;
	void DisplayMessage(string announcement)
	{
		tempAnnouncement[4] = tempAnnouncement[3];
		tempAnnouncement[3] = tempAnnouncement[2];
		tempAnnouncement[2] = tempAnnouncement[1];
		tempAnnouncement[1] = tempAnnouncement[0];
		tempAnnouncement[0] = announcement;
		announcerTextMesh.text = tempAnnouncement[4]+"\n"+tempAnnouncement[3]+"\n"+tempAnnouncement[2]+"\n"+tempAnnouncement[1]+"\n"+tempAnnouncement[0];
	}

	void UpdateScores(List<GameObject> players)
	{
		if (noOfDice == 0)
		{
			noOfDice = players[0].GetComponent<PlayerWeek5>().diceRolls.Length;
		}
		
		tempScores = new List<string>(noOfDice);
		
		scoreTextMesh.fontSize = Mathf.Min(96f/players.Count, 96f/(2+noOfDice));
		string line1 = "Player:";
		string lastLine = "Score :";
		for (var j = 0; j < noOfDice; j++)
		{
			if (j < 10)
			{
				tempScores.Add("\nDice "+(j+1)+":");
			}
			else
			{
				tempScores.Add("\nDice"+(j+1)+":");
			}
		}
		
		for (var i = 0; i < players.Count; i++)
		{
			if (i < 10)
			{
				line1 += "|  "+(i+1)+" |";
			}
			else
			{
				line1 += "| "+(i+1)+" |";
			}
			for (var j = 0; j < noOfDice; j++)
			{
				//add colour
				if (j<players[i].GetComponent<PlayerWeek5>().noOfDiceLock)
				{
					if (players[i].GetComponent<PlayerWeek5>().diceRolls[j] < 10)
					{
						tempScores[j] += "<color=red>|  "+players[i].GetComponent<PlayerWeek5>().diceRolls[j]+" |</color>";
					}
					else
					{
						tempScores[j] += "<color=red>| "+players[i].GetComponent<PlayerWeek5>().diceRolls[j]+" |</color>";
					}
				}
				else
				{
					if (players[i].GetComponent<PlayerWeek5>().diceRolls[j] < 10)
					{
						tempScores[j] += "|  "+players[i].GetComponent<PlayerWeek5>().diceRolls[j]+" |";
					}
					else
					{
						tempScores[j] += "| "+players[i].GetComponent<PlayerWeek5>().diceRolls[j]+" |";
					}
				}
			}
			if (players[i].GetComponent<PlayerWeek5>().diceRolls.Sum() < 10)
			{
				lastLine += "|  "+players[i].GetComponent<PlayerWeek5>().diceRolls.Sum()+" |";
			}
			else
			{
				lastLine += "| "+players[i].GetComponent<PlayerWeek5>().diceRolls.Sum()+" |";
			}
		}

		foreach (var t in tempScores)
		{
			line1 += t;
		}

		line1 += "\n" + lastLine;
		
		scoreTextMesh.text = line1;
	}
	
	void OnEnable()
	{
		gameManager.UIAnnouncementEvent += DisplayMessage;
		gameManager.UIScoreEvent += UpdateScores;
	}

	void OnDisable()
	{
		gameManager.UIAnnouncementEvent -= DisplayMessage;
		gameManager.UIScoreEvent -= UpdateScores;
	}
}
