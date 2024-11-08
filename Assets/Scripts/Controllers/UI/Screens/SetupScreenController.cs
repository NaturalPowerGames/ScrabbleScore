using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class SetupScreenController : UIController
{
    [SerializeField]
    private TMP_Dropdown timeInput, playersInput;
	[SerializeField]
	private Button goToGameButton, inputPlayerNamesButton, goToDataScreenButton, onlineServicesLoginButton;
	[SerializeField]
	private TMP_InputField idInput;
	[SerializeField]
	private TextMeshProUGUI connectedText;

	public override void Toggle(bool active)
	{
		base.Toggle(active);
		if (active)
		{
			timeInput.SetValueWithoutNotify(0);
			playersInput.SetValueWithoutNotify(0);
		}
	}

	private void OnEnable()
	{
		SetupEvents.OnConnectedToOnlineServices += OnConnectedToOnlineServices;
	}

	private void OnDisable()
	{
		SetupEvents.OnConnectedToOnlineServices -= OnConnectedToOnlineServices;
	}

	private void OnConnectedToOnlineServices()
	{
		connectedText.DOColor(Color.white, 3).OnComplete(() => connectedText.DOColor(Constants.Transparent,1));
	}

	protected override void SetupButtons()
	{
		goToGameButton.onClick.AddListener(() =>
		{
			//GameEvents.OnGameStartRequested?.Invoke();
		//	UIEvents.OnScreenChangeRequested?.Invoke(Screens.Game); from now on, players MUST go to the input screen
			UIEvents.OnScreenChangeRequested?.Invoke(Screens.PlayerNameInput);

		});
		inputPlayerNamesButton.onClick.AddListener(() =>
		{
			UIEvents.OnScreenChangeRequested?.Invoke(Screens.PlayerNameInput);
		});
		goToDataScreenButton.onClick.AddListener(() =>
		{
			UIEvents.OnScreenChangeRequested?.Invoke(Screens.Data);
		});
		onlineServicesLoginButton.onClick.AddListener(() =>
		{
			if (idInput.text == "") return;
			SetupEvents.OnUseOnlineServicesChangeRequested?.Invoke(true, idInput.text);
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

		idInput.onSelect.AddListener((x) =>
		{
			UIEvents.OnInputFieldInteractionBegan?.Invoke(idInput.GetComponent<RectTransform>());
		});
		idInput.onDeselect.AddListener((x) =>
		{
			UIEvents.OnInputFieldInteractionEnded?.Invoke(idInput.GetComponent<RectTransform>());
		});
	}
}
