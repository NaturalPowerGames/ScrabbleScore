using System;
using UnityEngine;

public class GameplayScoreboardController : MonoBehaviour
{
	[SerializeField]
	private MiniPlayerScoreDisplayController displayPrefab;
	private MiniPlayerScoreDisplayController[] scoreDisplays;

	private void OnEnable()
	{
		GameEvents.OnScoresUpdated += OnScoresUpdated;
	}

	private void OnDisable()
	{
		GameEvents.OnScoresUpdated -= OnScoresUpdated;
	}

	private void OnScoresUpdated(ScoreData[] scores)
	{
		for (int i = 0; i < scores.Length; i++)
		{
			int index = i;
			scoreDisplays[index].UpdateDisplay(scores[index].PlayerName, scores[index].Score);
		}
	}

	public void Initialize(PlayerData[] players)
	{
		transform.RemoveAllChildren();
		scoreDisplays = new MiniPlayerScoreDisplayController[players.Length];
		for (int i = 0; i < players.Length; i++)
		{
			int index = i;
			scoreDisplays[index] = Instantiate(displayPrefab, transform);
			scoreDisplays[index].UpdateDisplay(players[index].name, players[index].Score);
		}
	}
}
