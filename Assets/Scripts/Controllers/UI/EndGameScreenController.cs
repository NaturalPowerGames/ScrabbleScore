using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EndGameScreenController : UIController
{
	[SerializeField]
	private Button playAgainButton, setupAgainButton, addDifferentialPointsButton, goToDataScreenButton;
	[SerializeField]
	private TextMeshProUGUI resultDisplay;
	[SerializeField]
	private MiniPlayerScoreDisplayController scoreDisplayPrefab;
	[SerializeField]
	private Transform scoreboardParent;
	[SerializeField]
	private DifferentialPointsScreenController differentialPointsScreen;

	protected override void SetupButtons()
	{
		playAgainButton.onClick.AddListener(() =>
		{
			GameEvents.OnGameResetRequested?.Invoke(false);
		});
		setupAgainButton.onClick.AddListener(() =>
		{
			GameEvents.OnGameResetRequested?.Invoke(true);
		});
		addDifferentialPointsButton.onClick.AddListener(() =>
		{
			differentialPointsScreen.gameObject.SetActive(true);
		});
		goToDataScreenButton.onClick.AddListener(() =>
		{
			UIEvents.OnScreenChangeRequested?.Invoke(Screens.Data);
		});
	}

	protected override void SetupInputs()
	{
	}

	private void OnEnable()
	{
		differentialPointsScreen.gameObject.SetActive(false);
		GameEvents.OnGameEnded += OnGameEnded;
	}

	private void OnDisable()
	{
		GameEvents.OnGameEnded -= OnGameEnded;
	}

	private void OnGameEnded(PlayerData[] players, ScoreData[] scores)
	{
		differentialPointsScreen.Initialize(players);
		DisplayResultText(scores);
		GenerateScoreboard(scores);
	}

	private void DisplayResultText(ScoreData[] scores)
	{
		if (HasTie(scores))
		{
			resultDisplay.text = FormatTieText(scores);
		}
		else
		{
			resultDisplay.text = FormatWinnerText(scores[0]);			
		}
	}

	private void GenerateScoreboard(ScoreData[] scores)
	{
		scoreboardParent.DeleteAllChildren();
		for (int i = 0; i < scores.Length; i++)
		{
			int index = i;
			var scoreDisplay = Instantiate(scoreDisplayPrefab, scoreboardParent);
			scoreDisplay.UpdateDisplay(scores[index]);
		}
	}

	private string FormatWinnerText(ScoreData winner)
	{
		string formattedWinnerText = $"Gano {winner.PlayerName} con {winner.Score} puntos";
		return formattedWinnerText;
	}

	private string FormatTieText(ScoreData[] members)
	{
		foreach (var member1 in members)
		{
			foreach (var member2 in members)
			{
				if (member1 != member2 && member1.Score == member2.Score)
				{
					return $"Empate entre {member1.PlayerName} y {member2.PlayerName} con {member1.Score} puntos";
				}
			}
		}
		return "No hubo empate.";
	}

	private bool HasTie(ScoreData[] scores)
	{
		for (int i = 0; i < scores.Length; i++)
		{
			for (int j = i + 1; j < scores.Length; j++)
			{
				if (scores[i].Score == scores[j].Score)
				{
					return true; // Tie found
				}
			}
		}
		return false; // No tie found
	}
}
