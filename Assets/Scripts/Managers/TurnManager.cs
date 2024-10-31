using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
	private int currentPlayerIndex, currentTurn;
	private TurnData currentTurnInfo;

	private void OnEnable()
	{
		GameEvents.OnGameStarted += OnGameStarted;
		GameEvents.OnTurnEndRequested += OnTurnEndRequested;
		TimeEvents.OnTimeSpentInTurnChanged += OnTimeSpentInTurnChanged;
	}

	private void OnDisable()
	{
		GameEvents.OnGameStarted -= OnGameStarted;
		GameEvents.OnTurnEndRequested -= OnTurnEndRequested;
		TimeEvents.OnTimeSpentInTurnChanged -= OnTimeSpentInTurnChanged;
	}

	private void OnGameStarted(PlayerData[] players)
	{
		StartNewTurn(0, 0);
	}

	private void StartNewTurn(int playerIndex, int turn)
	{
		currentPlayerIndex = playerIndex;
		currentTurn = turn;
		currentTurnInfo = new TurnData();
		GameEvents.OnTurnStartRequested?.Invoke(currentPlayerIndex, currentTurn);
		TimeEvents.OnTimerStartRequested?.Invoke();
	}

	private void OnTurnEndRequested(string word, int score)
	{
		currentTurnInfo.Word = word;
		currentTurnInfo.Score = score;
		currentTurn++;
		currentPlayerIndex++;
		if (currentPlayerIndex >= Constants.PlayerAmount)
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
