using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TurnInfoInputScreenController : MonoBehaviour
{
	[SerializeField]
	private Button cancelButton, confirmButton;
	[SerializeField]
	private TMP_InputField wordInput, scoreInput;
	private int score
	{
		get
		{
			int score = 0;
			int.TryParse(scoreInput.text, out score);
			return score;
		}
	}

	private void Awake()
	{
		SetupButtons();
	}

	private void OnEnable()
	{
		ClearInputs();
	}

	private void SetupButtons()
	{
		cancelButton.onClick.AddListener(() =>
		{
			gameObject.SetActive(false);
			TimeEvents.OnForceTimerToggleRequested?.Invoke(false);
		});

		confirmButton.onClick.AddListener(() =>
		{
			gameObject.SetActive(false);
			GameEvents.OnTurnEndRequested?.Invoke(wordInput.text, score);
		});
	}
	
	private void ClearInputs()
	{
		wordInput.text = "";
		scoreInput.text = "";
	}
}
