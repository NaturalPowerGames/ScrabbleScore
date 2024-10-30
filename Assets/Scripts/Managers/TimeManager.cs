using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	private DateTime TimerStart;
	private bool countingDown, paused, timerStarted;
	private float secondsInPause;

	private void OnEnable()
	{
		TimeEvents.OnTimerStartRequested += OnTimerStartRequested;
		TimeEvents.OnTimerToggleRequested += OnTimerToggleRequested;
	}

	private void OnDisable()
	{
		TimeEvents.OnTimerStartRequested -= OnTimerStartRequested;
		TimeEvents.OnTimerToggleRequested -= OnTimerToggleRequested;
	}

	private void OnTimerStartRequested()
	{
		TimerStart = DateTime.Now;
		TimeEvents.OnTimerStarted?.Invoke();
		countingDown = true;
		timerStarted = true;
	}

	private void OnTimerToggleRequested(bool active)
	{
		countingDown = active;
		TimeEvents.OnTimerToggled?.Invoke(active);
	}

	private void Update()
	{
		if (!timerStarted) return;
		var differenceSeconds = (DateTime.Now - TimerStart).Seconds;
		TimerCountdown(differenceSeconds);
		CalculateSecondsInPause();
		TimeEvents.OnTimeSpentInTurnChanged?.Invoke(differenceSeconds - secondsInPause);
	}

	private void TimerCountdown(int differenceSeconds)
	{
		if (countingDown)
		{
			var secondsLeft = Constants.CurrentGame.TimerMinutes * 60 - differenceSeconds + secondsInPause;
			if (secondsLeft <= 0)
			{
				TimeEvents.OnTimerEnded?.Invoke();
				countingDown = false;
			}
			else
			{
				TimeEvents.OnTimerChanged?.Invoke(secondsLeft);
			}
		}
	}

	private void CalculateSecondsInPause()
	{
		if (paused)
		{
			secondsInPause += Time.deltaTime;
		}
	}
}
