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
	public static Action<PlayerInfo, int> OnTurnStarted;
	/// <summary>
	/// Word, score
	/// </summary>
	public static Action<string, int> OnTurnEndRequested;
	public static Action<TurnInfo> OnTurnEnded;
	public static Action OnGameStartRequested;
	public static Action OnGameStarted;
	public static Action OnGameEndRequested;
	public static Action OnGameEnded;	
}
