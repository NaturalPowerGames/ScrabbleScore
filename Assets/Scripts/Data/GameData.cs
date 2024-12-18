using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameData
{
	[SerializeField]
	private Guid matchID;
	public Guid MatchID => matchID;
	[SerializeField]
	private string matchDate, endDate;
	public DateTime MatchDate=> DateTime.Parse(matchDate);
	public DateTime EndDate => DateTime.Parse(endDate);
	[SerializeField]
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

	public int PlayerAmount => players.Count;

	public float TotalTime 
	{
		get
		{
			float seconds = (float) (MatchDate - EndDate).TotalSeconds;
			return seconds;
		}
	}

	public TimeSpan TimeInGame
	{
		get
		{
			return MatchDate - EndDate;
		}
	}

	[SerializeField]
	private List<PlayerData> players = new List<PlayerData>();

	public List<PlayerData> Players => players;

	public int TurnCount 
	{
		get
		{
			int totalTurns = players.Sum(player => player.Turns.Count);
			return totalTurns;
		}
	}
	public GameData()
	{
		matchID = Guid.NewGuid();
		matchDate = DateTime.Now.ToString();
	}

	public GameData(GameData gameData)
	{
		matchID = Guid.NewGuid();
		matchDate = gameData.matchDate;
		TimerMinutes = gameData.TimerMinutes;
		players = new List<PlayerData>(gameData.Players);
		ResetPlayers();
	}

	private void ResetPlayers()
	{
		foreach (PlayerData player in players)
		{
			player.ClearTurns();
			player.UpdateDifferential(0);
		}
	}

	public void AddPlayerInfo(PlayerData playerData)
	{		
		players.Add(playerData);
	}

	public void UpdatePlayerInfo(int index, TablePositions position)
	{
		players[index].tableLocation = position;
	}

	public void UpdatePlayerInfo(int index, string name)
	{
		name = name.TrimEnd().ToLower();
		players[index].name = name;
	}

	public void ChangePlayerAmount(int playerAmount)
	{
		if (players.Count > playerAmount)
		{
			players.RemoveRange(playerAmount, players.Count - playerAmount);
		}
		else if (players.Count < playerAmount)
		{
			for (int i = players.Count; i < playerAmount; i++)
			{
				players.Add(new PlayerData(i)); 
			}
		}
		SetupEvents.OnPlayerAmountChanged?.Invoke(PlayerAmount);
	}

	public void AddTurnToPlayer(int playerIndex, TurnData turn)
	{
		players[playerIndex].AddTurn(turn);
	}

	public ScoreData[] CurrentScoresOrderedByDescending()
	{
		var scores = new ScoreData[PlayerAmount];
		for (int i = 0; i < scores.Length; i++)
		{
			scores[i] = new ScoreData(players[i].name, players[i].Score);
		}
		return scores.OrderByDescending(score => score.Score).ToArray();
	}

	public void OnGameEnded()
	{
		endDate = DateTime.Now.ToString();
	}
}
