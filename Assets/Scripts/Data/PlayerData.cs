using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public string name;
    public TablePositions tableLocation;
    private List<TurnData> turns = new List<TurnData>();
	private int differential;

    public PlayerData(int index)
	{
		name = $"Jugador {index+1}"; //localization?
	}

    public override string ToString()
	{
		string info = $"Player name: {name}, Sitting at {tableLocation}, Current score: {Score}";
		return info;
	}

    public int Score
	{
		get
		{
			int score = 0;
			for (int i = 0; i < turns.Count; i++)
			{
				score += turns[i].Score;
			}
			score += differential;
			return score;
		}
	}

    public void AddTurn(string word, int score, float timeSpent)
	{
        turns.Add(new TurnData(word, score, timeSpent));
	}

	public void ClearTurns()
	{
		turns.Clear();
	}

	public void AddTurn(TurnData turnInfo)
	{
		turns.Add(turnInfo);
	}

	public void UpdateDifferential(int differential)
	{
		this.differential = differential;
	}
}
