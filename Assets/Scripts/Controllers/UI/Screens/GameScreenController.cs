using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameScreenController : UIController
{
	[SerializeField]
	private Button nextTurnButton, toggleTimerButton, endGameButton, endGameConfirmButton, endGameCancelButton, backButton, backToSetupConfirmButton, backToSetupCancelButton;
	[SerializeField]
	private GameObject turnInformationInputScreenParent, endGameConfirmationScreen, backToSetupConfirmationScreen;
	[SerializeField]
	private TextMeshProUGUI timerDisplay, turnInfoDisplay;
	[SerializeField]
	private Sprite[] pauseToggleSprites;
	[SerializeField]
	private GameplayScoreboardController scoreboard;

	private void OnEnable()
	{
		TimeEvents.OnTimerChanged += OnTimerChanged;
		TimeEvents.OnTimerToggled += OnTimerToggled;
		GameEvents.OnTurnStarted += OnTurnStarted;
		GameEvents.OnGameStarted += OnGameStarted;
	}

	private void OnDisable()
	{
		GameEvents.OnTurnStarted -= OnTurnStarted;
		TimeEvents.OnTimerToggled -= OnTimerToggled;
		TimeEvents.OnTimerChanged -= OnTimerChanged;
		GameEvents.OnGameStarted -= OnGameStarted;
	}

	public override void Toggle(bool active)
	{
		base.Toggle(active);
		if (!active)
		{
			TurnScreensOff();
		}
	}

	private void TurnScreensOff()
	{
		turnInformationInputScreenParent.SetActive(false);
		endGameConfirmationScreen.SetActive(false);
		backToSetupConfirmationScreen.SetActive(false);
	}

	private void OnGameStarted(PlayerData[] players)
	{
		endGameConfirmationScreen.SetActive(false);
		scoreboard.Initialize(players);
	}

	private void OnTurnStarted(PlayerData playerInfo, int turn)
	{
		turnInfoDisplay.text = FormatTurnInfo(playerInfo, turn + 1);
	}

	private void OnTimerToggled(bool paused)
	{
		toggleTimerButton.image.sprite = paused ? pauseToggleSprites[1] : pauseToggleSprites[0];
	}

	private string FormatTurnInfo(PlayerData playerInfo, int turn)
	{
		string playerName = playerInfo.name;
		string plays = Constants.CurrentLanguage == Language.English ? "Playing: " : "Juega: ";
		string turnText = Constants.CurrentLanguage == Language.English ? "Turn: " : "Turno: ";
		return $"{plays} {playerName}, {turnText} {turn}";
	}

	protected override void SetupButtons()
	{
		nextTurnButton.onClick.AddListener(() =>
		{
			turnInformationInputScreenParent.SetActive(true);
			TimeEvents.OnForceTimerToggleRequested?.Invoke(true);
		});

		toggleTimerButton.onClick.AddListener(() =>
		{
			TimeEvents.OnTimerToggleRequested?.Invoke();
		});

		endGameButton.onClick.AddListener(() =>
		{
			endGameConfirmationScreen.SetActive(true);
			TimeEvents.OnForceTimerToggleRequested?.Invoke(true);
		});

		endGameCancelButton.onClick.AddListener(() =>
		{
			endGameConfirmationScreen.SetActive(false);
			TimeEvents.OnForceTimerToggleRequested?.Invoke(false);
		});

		endGameConfirmButton.onClick.AddListener(() =>
		{
			GameEvents.OnGameEndRequested?.Invoke();
			UIEvents.OnScreenChangeRequested?.Invoke(Screens.EndGame);
		});

		backButton.onClick.AddListener(() =>
		{
			backToSetupConfirmationScreen.SetActive(true);
		});
		backToSetupConfirmButton.onClick.AddListener(() =>
		{
			GameEvents.OnGameResetRequested?.Invoke(true);
		});
		backToSetupCancelButton.onClick.AddListener(() =>
		{
			backToSetupConfirmationScreen.SetActive(false);
		});
	}

	protected override void SetupInputs()
	{
	}

	private void OnTimerChanged(float time)
	{
		var timeLeft = TimeSpan.FromSeconds(time);
		timerDisplay.color = time > 120 ? Color.white : time > 60 ? Color.yellow : Color.red;
		timerDisplay.text = $"{timeLeft.Minutes}:{timeLeft.Seconds.ToString("00")}";
	}
}
