[System.Serializable]
public class TurnInfo
{
	public string Word;
	public int Score;
	public float TimeSpent;

	public TurnInfo(string word, int score, float timeSpent)
	{
		this.Word = word;
		this.Score = score;
		this.TimeSpent = timeSpent;
	}

	public TurnInfo()
	{

	}

	public override string ToString()
	{
		string turnInfoFormatted = $"{Word} played, {Score} points, {TimeSpent} seconds";
		return turnInfoFormatted;
	}
}
