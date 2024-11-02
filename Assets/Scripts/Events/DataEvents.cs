using System;

public static class DataEvents
{
	public static Action OnSaveGameRequested;
	public static Action OnGameSaved;
	/// <summary>
	/// Username
	/// </summary>
	public static Action<string> OnLoadAllGamesForUserRequested;
	public static Action<GameData[]> OnGamesLoadedForUser;
}
