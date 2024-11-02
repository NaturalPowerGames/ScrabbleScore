using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	private int currentPlayerIndex;
	private GameData currentGame;
	private SaverLoader saverLoader;

	private void Awake()
	{
		saverLoader = new SaverLoader();
	}

	private void Start()
	{
		BaseSetupGame();
	}

	private void OnEnable()
	{
		SetupEvents.OnPlayerAmountChangeRequested += OnPlayerAmountChangeRequested;
		SetupEvents.OnMinutesPerTurnChangeRequested += OnMinutesPerTurnChangeRequested;
		SetupEvents.OnPlayerNameChangeRequested += OnPlayerNameChangeRequested;
		SetupEvents.OnPlayerTablePositionChangeRequested += OnPlayerTablePositionChangeRequested;
		GameEvents.OnTurnStartRequested += OnTurnStartRequested;
		GameEvents.OnTurnEnded += OnTurnEnded;
		GameEvents.OnGameStartRequested += OnGameStartRequested;
		GameEvents.OnGameEndRequested += OnGameEndRequested;
		GameEvents.OnDifferentialScoresUpdateRequested += OnDifferentialScoresUpdateRequested;
		GameEvents.OnGameResetRequested += OnGameResetRequested;
		DataEvents.OnSaveGameRequested += OnSaveGameRequested;
		DataEvents.OnLoadAllGamesForUserRequested += OnLoadAllGamesForUserRequested;
	}

	private void OnDisable()
	{
		SetupEvents.OnPlayerAmountChangeRequested -= OnPlayerAmountChangeRequested;
		SetupEvents.OnMinutesPerTurnChangeRequested -= OnMinutesPerTurnChangeRequested;
		SetupEvents.OnPlayerNameChangeRequested -= OnPlayerNameChangeRequested;
		SetupEvents.OnPlayerTablePositionChangeRequested -= OnPlayerTablePositionChangeRequested;
		GameEvents.OnTurnStartRequested -= OnTurnStartRequested;
		GameEvents.OnTurnEnded -= OnTurnEnded;
		GameEvents.OnGameStartRequested -= OnGameStartRequested;
		GameEvents.OnGameEndRequested -= OnGameEndRequested;
		GameEvents.OnDifferentialScoresUpdateRequested -= OnDifferentialScoresUpdateRequested;
		GameEvents.OnGameResetRequested -= OnGameResetRequested;
		DataEvents.OnSaveGameRequested -= OnSaveGameRequested;
		DataEvents.OnLoadAllGamesForUserRequested -= OnLoadAllGamesForUserRequested;
	}

	private void OnLoadAllGamesForUserRequested(string user)
	{
		GameData[] games = saverLoader.LoadGamesFromLocalStorage(user);
		DataEvents.OnGamesLoadedForUser?.Invoke(games);
	}

	private void OnSaveGameRequested()
	{
		saverLoader.SaveGame(currentGame);
		DataEvents.OnGameSaved?.Invoke();
	}

	private void BaseSetupGame()
	{
		currentGame = new GameData();
		OnPlayerAmountChangeRequested(Constants.BasePlayerAmount);
		OnMinutesPerTurnChangeRequested(Constants.BaseTurnTime);
	}

	private void ResetGame()
	{
		currentGame = new GameData(currentGame);
		OnGameStartRequested();
	}

	private void OnGameResetRequested(bool setup)
	{
		if (setup)
		{
			BaseSetupGame();
			UIEvents.OnScreenChangeRequested?.Invoke(Screens.BasicSetup);
		}
		else
		{
			ResetGame();
			UIEvents.OnScreenChangeRequested?.Invoke(Screens.Game);
		}
	}

	private void OnDifferentialScoresUpdateRequested(int[] differentials)
	{
		for (int i = 0; i < currentGame.Players.Count; i++)
		{
			currentGame.Players[i].UpdateDifferential(differentials[i]);
		}
		GameEvents.OnGameEnded?.Invoke(currentGame.Players.ToArray(), currentGame.CurrentScoresOrderedByDescending());
	}

	private void OnPlayerAmountChangeRequested(int playerAmount)
	{
		currentGame.ChangePlayerAmount(playerAmount);
		Constants.PlayerAmount = currentGame.PlayerAmount;
	}

	private void OnMinutesPerTurnChangeRequested(int minutes)
	{
		currentGame.TimerMinutes = minutes;
	}

	private void OnPlayerNameChangeRequested(int index, string newName)
	{
		currentGame.UpdatePlayerInfo(index, newName);
		SetupEvents.OnPlayerInfoChanged?.Invoke(currentGame.Players[index]);
	}

	private void OnPlayerTablePositionChangeRequested(int index, TablePositions position)
	{
		currentGame.UpdatePlayerInfo(index, position);
		SetupEvents.OnPlayerInfoChanged?.Invoke(currentGame.Players[index]);
	}

	private void OnGameStartRequested()
	{
		GameEvents.OnGameStarted?.Invoke(currentGame.Players.ToArray());
	}

	private void OnTurnStartRequested(int playerIndex, int turn)
	{
		currentPlayerIndex = playerIndex;
		GameEvents.OnTurnStarted?.Invoke(currentGame.Players[currentPlayerIndex], turn);
	}

	private void OnTurnEnded(TurnData turnInfo)
	{
		currentGame.AddTurnToPlayer(currentPlayerIndex, turnInfo);
		GameEvents.OnScoresUpdated?.Invoke(CalculatePlayerScores());
	}

	private void OnGameEndRequested()
	{
		GameEvents.OnGameEnded?.Invoke(currentGame.Players.ToArray(),currentGame.CurrentScoresOrderedByDescending());
	}

	private ScoreData[] CalculatePlayerScores()
	{
		ScoreData[] scores = new ScoreData[currentGame.PlayerAmount];
		for (int i = 0; i < scores.Length; i++)
		{
			scores[i] = new ScoreData(currentGame.Players[i].name, currentGame.Players[i].Score);
		}
		return scores;
	}
}
