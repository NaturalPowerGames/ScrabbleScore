[System.Serializable]
public class ScoreData
{
	private readonly string playerName;
	public string PlayerName => playerName;
	private readonly int score;
	public int Score => score;
	public ScoreData(string playerName, int score)
	{
		this.playerName = playerName;
		this.score = score;
	}
}
