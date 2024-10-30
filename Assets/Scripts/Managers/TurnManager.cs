using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
	private int currentPlayerIndex, currentTurn;
	private TurnInfo currentTurnInfo;

	private void OnEnable()
	{
		GameEvents.OnGameStartRequested += OnGameStartRequested;
		GameEvents.OnTurnEndRequested += OnTurnEndRequested;
		TimeEvents.OnTimeSpentInTurnChanged += OnTimeSpentInTurnChanged;
	}

	private void OnDisable()
	{
		GameEvents.OnGameStartRequested -= OnGameStartRequested;
		GameEvents.OnTurnEndRequested -= OnTurnEndRequested;
		TimeEvents.OnTimeSpentInTurnChanged -= OnTimeSpentInTurnChanged;
	}

	private void OnGameStartRequested()
	{
		StartNewTurn(0, 0);
	}

	private void StartNewTurn(int playerIndex, int turn)
	{
		currentPlayerIndex = 0;
		currentTurn = 0;
		currentTurnInfo = new TurnInfo();
		GameEvents.OnTurnStarted?.Invoke(currentPlayerIndex, currentTurn);
		TimeEvents.OnTimerStartRequested?.Invoke();
	}

	private void OnTurnEndRequested(string word, int score)
	{
		currentTurnInfo.Word = word;
		currentTurnInfo.Score = score;
		currentTurn++;
		currentPlayerIndex++;
		if (currentPlayerIndex >= Constants.CurrentGame.PlayerAmount)
		{
			currentPlayerIndex = 0;
		}
		GameEvents.OnTurnEnded?.Invoke(currentTurnInfo);
		StartNewTurn(currentPlayerIndex, currentTurn);
	}

	private void OnTimeSpentInTurnChanged(float seconds)
	{
		currentTurnInfo.TimeSpent = seconds;
	}
}
