using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	private void Awake()
	{
		Constants.CurrentGame = new GameInfo();
		OnPlayerAmountChangeRequested(Constants.BasePlayerAmount);
		OnMinutesPerTurnChangeRequested(Constants.BaseTurnTime);
	}

	private void OnEnable()
	{
		SetupEvents.OnPlayerAmountChangeRequested += OnPlayerAmountChangeRequested;
		SetupEvents.OnMinutesPerTurnChangeRequested += OnMinutesPerTurnChangeRequested;
		SetupEvents.OnPlayerNameChangeRequested += OnPlayerNameChangeRequested;
		SetupEvents.OnPlayerTablePositionChangeRequested += OnPlayerTablePositionChangeRequested;
	}

	private void OnDisable()
	{
		SetupEvents.OnPlayerAmountChangeRequested -= OnPlayerAmountChangeRequested;
		SetupEvents.OnMinutesPerTurnChangeRequested -= OnMinutesPerTurnChangeRequested;
		SetupEvents.OnPlayerNameChangeRequested -= OnPlayerNameChangeRequested;
		SetupEvents.OnPlayerTablePositionChangeRequested -= OnPlayerTablePositionChangeRequested;
	}	

	private void OnPlayerAmountChangeRequested(int playerAmount)
	{
		Constants.CurrentGame.ChangePlayerAmount(playerAmount);
	}

	private void OnMinutesPerTurnChangeRequested(int obj)
	{
		Constants.CurrentGame.TimerMinutes = obj;
	}

	private void OnPlayerNameChangeRequested(int index, string newName)
	{
		Constants.CurrentGame.UpdatePlayerInfo(index, newName);
		SetupEvents.OnPlayerInfoChanged?.Invoke(Constants.CurrentGame.PlayerInfos[index]);
	}

	private void OnPlayerTablePositionChangeRequested(int index, TablePositions position)
	{
		Constants.CurrentGame.UpdatePlayerInfo(index, position);
		SetupEvents.OnPlayerInfoChanged?.Invoke(Constants.CurrentGame.PlayerInfos[index]);
	}
}
