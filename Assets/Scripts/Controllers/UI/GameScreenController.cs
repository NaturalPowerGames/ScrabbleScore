using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameScreenController : UIController
{
	[SerializeField]
	private Button nextTurnButton, toggleTimerButton;
	[SerializeField]
	private GameObject turnInformationInputScreenParent;
	[SerializeField]
	private TextMeshProUGUI timerDisplay, turnInfoDisplay;
	[SerializeField]
	private Sprite[] pauseToggleSprites;

	private void OnEnable()
	{
		TimeEvents.OnTimerChanged += OnTimerChanged;
		TimeEvents.OnTimerToggled += OnTimerToggled;
		GameEvents.OnTurnStarted += OnTurnStarted;
	}

	private void OnDisable()
	{
		GameEvents.OnTurnStarted -= OnTurnStarted;
		TimeEvents.OnTimerToggled -= OnTimerToggled;
		TimeEvents.OnTimerChanged -= OnTimerChanged;
	}

	private void OnTimerToggled(bool obj)
	{
		throw new NotImplementedException();
	}

	private void OnTurnStarted(int playerIndex, int turn)
	{
		turnInfoDisplay.text = FormatTurnInfo(playerIndex, turn +1);
	}

	private string FormatTurnInfo(int playerIndex, int turn)
	{
		string playerName = Constants.CurrentGame.PlayerInfos[playerIndex].name;
		return $"Juega: {playerName}, turno {turn}";
	}

	protected override void SetupButtons()
	{
		nextTurnButton.onClick.AddListener(() =>
		{
			turnInformationInputScreenParent.SetActive(true);
		});

		toggleTimerButton.onClick.AddListener(() =>
		{
			//todo
		});
	}

	protected override void SetupInputs()
	{
	}

	private void OnTimerChanged(float time)
	{
		var timeLeft = TimeSpan.FromSeconds(time);
		timerDisplay.text = $"{timeLeft.Minutes}:{timeLeft.Seconds}";
	}
}
