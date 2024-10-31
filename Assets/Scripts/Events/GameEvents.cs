using System;

public static class GameEvents
{
	/// <summary>
	/// Player index, turn amount
	/// </summary>
	public static Action<int, int> OnTurnStartRequested;
	/// <summary>
	/// Player, turn
	/// </summary>
	public static Action<PlayerData, int> OnTurnStarted;
	/// <summary>
	/// Word, score
	/// </summary>
	public static Action<string, int> OnTurnEndRequested;
	public static Action<TurnData> OnTurnEnded;
	public static Action<ScoreData[]> OnScoresUpdated;

	public static Action OnGameStartRequested;
	/// <summary>
	/// Send Players with the started event
	/// </summary>
	public static Action<PlayerData[]> OnGameStarted;
	public static Action OnGameEndRequested;
	/// <summary>
	/// Player scores
	/// </summary>
	public static Action<PlayerData[], ScoreData[]> OnGameEnded;
	public static Action<int[]> OnDifferentialScoresUpdateRequested;
	/// <summary>
	/// Setup?
	/// </summary>
	public static Action<bool> OnGameResetRequested;
}
