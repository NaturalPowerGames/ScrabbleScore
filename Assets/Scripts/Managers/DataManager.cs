using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	private int currentPlayerIndex;
	private GameInfo currentGame;

	private void Awake()
	{
		currentGame = new GameInfo();
		OnPlayerAmountChangeRequested(Constants.BasePlayerAmount);
		OnMinutesPerTurnChangeRequested(Constants.BaseTurnTime);
	}

	private void OnEnable()
	{
		SetupEvents.OnPlayerAmountChangeRequested += OnPlayerAmountChangeRequested;
		SetupEvents.OnMinutesPerTurnChangeRequested += OnMinutesPerTurnChangeRequested;
		SetupEvents.OnPlayerNameChangeRequested += OnPlayerNameChangeRequested;
		SetupEvents.OnPlayerTablePositionChangeRequested += OnPlayerTablePositionChangeRequested;
		GameEvents.OnTurnStartRequested += OnTurnStartRequested;
		GameEvents.OnTurnEnded += OnTurnEnded;
	}

	private void OnDisable()
	{
		SetupEvents.OnPlayerAmountChangeRequested -= OnPlayerAmountChangeRequested;
		SetupEvents.OnMinutesPerTurnChangeRequested -= OnMinutesPerTurnChangeRequested;
		SetupEvents.OnPlayerNameChangeRequested -= OnPlayerNameChangeRequested;
		SetupEvents.OnPlayerTablePositionChangeRequested -= OnPlayerTablePositionChangeRequested;
		GameEvents.OnTurnStartRequested -= OnTurnStartRequested;
		GameEvents.OnTurnEnded -= OnTurnEnded;
	}

	private void OnTurnStartRequested(int playerIndex, int turn)
	{
		currentPlayerIndex = playerIndex;
		GameEvents.OnTurnStarted?.Invoke(currentGame.PlayerInfos[currentPlayerIndex], turn);
	}

	private void OnTurnEnded(TurnInfo turnInfo)
	{
		currentGame.AddTurnToPlayer(currentPlayerIndex, turnInfo);
	}

	private void OnPlayerAmountChangeRequested(int playerAmount)
	{
		currentGame.ChangePlayerAmount(playerAmount);
		Constants.PlayerAmount = currentGame.PlayerAmount;
	}

	private void OnMinutesPerTurnChangeRequested(int minutes)
	{
		currentGame.TimerMinutes = minutes;
	}

	private void OnPlayerNameChangeRequested(int index, string newName)
	{
		currentGame.UpdatePlayerInfo(index, newName);
		SetupEvents.OnPlayerInfoChanged?.Invoke(currentGame.PlayerInfos[index]);
	}

	private void OnPlayerTablePositionChangeRequested(int index, TablePositions position)
	{
		currentGame.UpdatePlayerInfo(index, position);
		SetupEvents.OnPlayerInfoChanged?.Invoke(currentGame.PlayerInfos[index]);
	}
}
