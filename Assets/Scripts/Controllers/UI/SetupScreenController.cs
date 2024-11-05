using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetupScreenController : UIController
{
    [SerializeField]
    private TMP_Dropdown timeInput, playersInput;
    [SerializeField]
    private Button goToGameButton, inputPlayerNamesButton, goToDataScreenButton;
	[SerializeField]
	private Toggle useOnlineServicesToggle;
	[SerializeField]
	private TMP_InputField idInput;

	public override void Toggle(bool active)
	{
		base.Toggle(active);
		if (active)
		{
			timeInput.SetValueWithoutNotify(0);
			playersInput.SetValueWithoutNotify(0);
		}
	}
	protected override void SetupButtons()
	{
		goToGameButton.onClick.AddListener(() =>
		{
			GameEvents.OnGameStartRequested?.Invoke();
			UIEvents.OnScreenChangeRequested?.Invoke(Screens.Game);
		});
		inputPlayerNamesButton.onClick.AddListener(() =>
		{
			UIEvents.OnScreenChangeRequested?.Invoke(Screens.PlayerNameInput);
		});
		goToDataScreenButton.onClick.AddListener(() =>
		{
			UIEvents.OnScreenChangeRequested?.Invoke(Screens.Data);
		});
	}

	protected override void SetupInputs()
	{
		playersInput.onValueChanged.AddListener((x) =>
		{
			SetupEvents.OnPlayerAmountChangeRequested?.Invoke(int.Parse(playersInput.options[x].text));
		});
		timeInput.onValueChanged.AddListener((x) =>
		{
			SetupEvents.OnMinutesPerTurnChangeRequested?.Invoke(int.Parse(timeInput.options[x].text));
		});

		useOnlineServicesToggle.onValueChanged.AddListener((x) =>
		{
			if (idInput.text == "") return;
			SetupEvents.OnUseOnlineServicesChangeRequested?.Invoke(x, idInput.text);
		});
	}
}
