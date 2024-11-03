using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInputScreenController : UIController
{
	[SerializeField]
	private Button goToGameButton;
	[SerializeField]
	private Transform inputsParent;
	[SerializeField]
	private PlayerNameInputController inputPrefab;

	protected override void SetupButtons()
	{
		goToGameButton.onClick.AddListener(() =>
		{
			GameEvents.OnGameStartRequested?.Invoke();
			UIEvents.OnScreenChangeRequested?.Invoke(Screens.Game);
		});
	}

	protected override void SetupInputs()
	{
		//do we setup inputs here?
	}


	private void OnEnable()
	{
		SetupEvents.OnPlayerAmountChanged += OnPlayerAmountChanged;
	}

	private void OnDisable()
	{
		SetupEvents.OnPlayerAmountChanged -= OnPlayerAmountChanged;
	}

	private void OnPlayerAmountChanged(int amount)
	{
		DeleteCurrentChildren();
		GenerateInputs(amount);
	}

	private void GenerateInputs(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			int index = i;
			var playerInput = Instantiate(inputPrefab, inputsParent);
			playerInput.Initialize(index);
		}
	}

	private void DeleteCurrentChildren()
	{
		inputsParent.RemoveAllChildren();
	}
}
