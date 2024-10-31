using UnityEngine;
using System.Collections.Generic;
using System;

public class GameInfo
{
	private int timerMinutes, totalTime;
	public int TimerMinutes 
	{
		get => timerMinutes;
		set
		{
			timerMinutes = value;
			SetupEvents.OnMinutesPerTurnChanged?.Invoke(timerMinutes);
		}
	}

	public int PlayerAmount => playerInfos.Count;

	public float TotalTime => totalTime;

	private List<PlayerInfo> playerInfos = new List<PlayerInfo>();

	public List<PlayerInfo> PlayerInfos => playerInfos;

	public void AddPlayerInfo(PlayerInfo playerInfo)
	{
		playerInfos.Add(playerInfo);
	}

	public void UpdatePlayerInfo(int index, TablePositions position)
	{
		playerInfos[index].tableLocation = position;
	}

	public void UpdatePlayerInfo(int index, string name)
	{
		playerInfos[index].name = name;
	}

	public void ChangePlayerAmount(int playerAmount)
	{
		if (playerInfos.Count > playerAmount)
		{
			playerInfos.RemoveRange(playerAmount, playerInfos.Count - playerAmount);
		}
		else if (playerInfos.Count < playerAmount)
		{
			for (int i = playerInfos.Count; i < playerAmount; i++)
			{
				playerInfos.Add(new PlayerInfo(i)); 
			}
		}
		SetupEvents.OnPlayerAmountChanged?.Invoke(PlayerAmount);
	}

	internal void AddTurnToPlayer(int playerIndex, TurnInfo turnInfo)
	{
		playerInfos[playerIndex].AddTurn(turnInfo);
	}
}
