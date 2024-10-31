using System;

public static class SetupEvents
{
	public static Action<int> OnPlayerAmountChangeRequested;
	public static Action<int> OnPlayerAmountChanged;
	public static Action<int> OnMinutesPerTurnChangeRequested;
	public static Action<int> OnMinutesPerTurnChanged;
	public static Action<int, string> OnPlayerNameChangeRequested;
	public static Action<int, TablePositions> OnPlayerTablePositionChangeRequested;
	public static Action<PlayerData> OnPlayerInfoChangeRequested;
	public static Action<PlayerData> OnPlayerInfoChanged;
}
