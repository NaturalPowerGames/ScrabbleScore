using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	private DateTime timerStart;
	private bool countingDown, paused, timerStarted, hasLostFocus;
	private float secondsInPause;
	private int minutesPerTurn;

	private void OnEnable()
	{
		TimeEvents.OnTimerStartRequested += OnTimerStartRequested;
		TimeEvents.OnTimerToggleRequested += OnTimerToggleRequested;
		TimeEvents.OnTimerEndRequested += OnTimerEndRequested;
		TimeEvents.OnForceTimerToggleRequested += OnForceTimerToggleRequested;
		SetupEvents.OnMinutesPerTurnChanged += OnMinutesPerTurnChanged;
	}

	private void OnDisable()
	{
		TimeEvents.OnTimerStartRequested -= OnTimerStartRequested;
		TimeEvents.OnTimerToggleRequested -= OnTimerToggleRequested;
		TimeEvents.OnTimerEndRequested -= OnTimerEndRequested;
		TimeEvents.OnForceTimerToggleRequested -= OnForceTimerToggleRequested;
		SetupEvents.OnMinutesPerTurnChanged -= OnMinutesPerTurnChanged;
	}

	private void OnMinutesPerTurnChanged(int minutesPerTurn)
	{
		this.minutesPerTurn = minutesPerTurn;
	}

	private void OnForceTimerToggleRequested(bool paused)
	{
		this.paused = paused;
		TimeEvents.OnTimerToggled?.Invoke(paused);
	}

	private void OnTimerStartRequested()
	{
		ResetTimer();
		TimeEvents.OnTimerToggled?.Invoke(paused);
		TimeEvents.OnTimerStarted?.Invoke();
	}

	private void OnTimerToggleRequested()
	{
		countingDown = !countingDown;
		paused = !countingDown;		
		TimeEvents.OnTimerToggled?.Invoke(paused);
	}

	private void OnTimerEndRequested()
	{
		EndTimer();
	}

	private void Update()
	{
		if (!timerStarted) return;
		var differenceSeconds = (DateTime.Now - timerStart).Seconds;
		TimerCountdown(differenceSeconds);
		CalculateSecondsInPause();
		TimeEvents.OnTimeSpentInTurnChanged?.Invoke(differenceSeconds - secondsInPause);
	}

	private void TimerCountdown(int differenceSeconds)
	{
		if (countingDown)
		{
			var secondsLeft = minutesPerTurn * 60 - differenceSeconds + secondsInPause;
			if (secondsLeft <= 0)
			{
				EndTimer();
			}
			else
			{
				TimeEvents.OnTimerChanged?.Invoke(secondsLeft);
			}
		}
	}

	private void ResetTimer()
	{
		timerStart = DateTime.Now;
		countingDown = true;
		timerStarted = true;
		paused = false;
		secondsInPause = 0;
	}

	private void EndTimer()
	{
		countingDown = false;
		paused = false;
		TimeEvents.OnTimerEnded?.Invoke();
	}

	private void CalculateSecondsInPause()
	{
		if (paused)
		{
			secondsInPause += Time.deltaTime;
		}
	}

	private void OnApplicationFocus(bool focus)
	{
		if (focus && hasLostFocus)
		{
			hasLostFocus = false;
			if (timerStarted)
			{
				timerStart = DateTime.Parse(PlayerPrefs.GetString("TimerStartTime"));
			}
		}
		else
		{
			hasLostFocus = true;
			if (timerStarted)
			{
				PlayerPrefs.SetString("TimerStartTime", timerStart.ToString());
			}
		}
	}
}
