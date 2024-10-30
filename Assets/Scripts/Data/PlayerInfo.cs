using System.Collections.Generic;

[System.Serializable]
public class PlayerInfo
{
    public string name;
    public TablePositions tableLocation;
    private List<TurnInfo> turns = new List<TurnInfo>();

    public PlayerInfo(int index)
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
			return score;
		}
	}

    public void AddTurn(string word, int score, float timeSpent)
	{
        turns.Add(new TurnInfo(word, score, timeSpent));
	}
}
