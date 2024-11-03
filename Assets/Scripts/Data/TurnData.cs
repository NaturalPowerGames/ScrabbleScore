using System;

[System.Serializable]
public class TurnData
{
	public string Word;
	public int Score;
	public float TimeSpent;

	public TurnData(string word, int score, float timeSpent)
	{
		this.Word = word;
		this.Score = score;
		if (timeSpent < 1) timeSpent = 1;
		float truncatedTime = (float)Math.Truncate(timeSpent * 100) / 100;
		TimeSpent = truncatedTime;
	}

	public TurnData()
	{

	}

	public override string ToString()
	{
		string turnInfoFormatted = $"{Word} played, {Score} points, {TimeSpent} seconds";
		return turnInfoFormatted;
	}
}
