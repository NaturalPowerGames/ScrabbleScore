using System;

public static class TimeEvents
{
	public static Action OnTimerToggleRequested;
	/// <summary>
	/// Bool = paused
	/// </summary>
	public static Action<bool> OnForceTimerToggleRequested;
	/// <summary>
	/// Bool = paused
	/// </summary>
	public static Action<bool> OnTimerToggled;
	public static Action OnTimerStartRequested;
	public static Action OnTimerStarted;
	public static Action OnTimerEnded;
	public static Action OnTimerEndRequested;
	public static Action<float> OnTimerChanged;
	public static Action<float> OnTimeSpentInTurnChanged;
}
