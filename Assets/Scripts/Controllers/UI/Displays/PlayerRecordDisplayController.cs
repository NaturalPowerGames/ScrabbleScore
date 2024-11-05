using UnityEngine;
using TMPro;
using System;

public class PlayerRecordDisplayController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameDisplay, scoreDisplay, averageTimeDisplay, scrabblesDisplay, turnsDisplay;

    public void Initialize(string name, int score, float averageTimeInSeconds, int scrabbles, int turns)
	{
        nameDisplay.text = name;
        scoreDisplay.text = score.ToString();
        var timeSpan = TimeSpan.FromSeconds(averageTimeInSeconds);
        averageTimeDisplay.text = $"{timeSpan.Minutes}:{timeSpan.Seconds:00}";
        scrabblesDisplay.text = $"{scrabbles}";
        turnsDisplay.text = turns.ToString();
    }

    public void Initialize(PlayerData playerData)
    {
        Initialize(playerData.name, playerData.Score, playerData.AverageTurnTime, playerData.Scrabbles, playerData.Turns.Count);
    }
}
