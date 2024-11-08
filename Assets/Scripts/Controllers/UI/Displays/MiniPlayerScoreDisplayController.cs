using UnityEngine;
using TMPro;
using System;

public class MiniPlayerScoreDisplayController : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI playerName, playerScore;

	public void UpdateDisplay(string playerName, int playerScore)
	{
		this.playerName.text = playerName;
		this.playerScore.text = playerScore.ToString();
	}
	
	public void UpdateDisplay(ScoreData scoreInfo)
	{
		UpdateDisplay(scoreInfo.PlayerName, scoreInfo.Score);
	}
}
