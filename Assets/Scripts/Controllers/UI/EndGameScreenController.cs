using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameScreenController : UIController
{
	[SerializeField]
	private Button playAgainButton, setupAgainButton;

	protected override void SetupButtons()
	{
		playAgainButton.onClick.AddListener(() =>
		{
			UIEvents.OnScreenChangeRequested?.Invoke(Screens.Game);
		});
		setupAgainButton.onClick.AddListener(() =>
		{
			UIEvents.OnScreenChangeRequested?.Invoke(Screens.BasicSetup);
		});
	}

	protected override void SetupInputs()
	{
	}
}
