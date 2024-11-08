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
			int score;
			int.TryParse(scoreInput.text, out score);
			return score;
		}
	}

	private void Awake()
	{
		SetupButtons();
		SetupInputs();
	}

	private void SetupInputs()
	{
		wordInput.onSelect.AddListener((x) =>
		{
			UIEvents.OnInputFieldInteractionBegan?.Invoke(wordInput.GetComponent<RectTransform>());
		});
		wordInput.onDeselect.AddListener((x) =>
		{
			UIEvents.OnInputFieldInteractionEnded?.Invoke(wordInput.GetComponent<RectTransform>());
		});
		scoreInput.onSelect.AddListener((x) =>
		{
			UIEvents.OnInputFieldInteractionBegan?.Invoke(scoreInput.GetComponent<RectTransform>());
		});
		scoreInput.onDeselect.AddListener((x) =>
		{
			UIEvents.OnInputFieldInteractionEnded?.Invoke(scoreInput.GetComponent<RectTransform>());
		});
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
