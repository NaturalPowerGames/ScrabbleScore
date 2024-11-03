using UnityEngine;
using TMPro;
using System;

public class PlayerRecordDisplayController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameDisplay, scoreDisplay, averageTimeDisplay;

    public void Initialize(string name, int score, float averageTimeInSeconds)
	{
        nameDisplay.text = name;
        scoreDisplay.text = score.ToString();
        var timeSpan = TimeSpan.FromSeconds(averageTimeInSeconds);
        averageTimeDisplay.text = $"{timeSpan.Minutes}:{timeSpan.Seconds:00}";
    }

    public void Initialize(PlayerData playerData)
    {
        Initialize(playerData.name, playerData.Score, playerData.AverageTurnTime);
    }
}
