using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GameRecordsScreenController : UIController
{
	[SerializeField]
	private Button loadGameRecordsForUserButton, saveGameButton;
	[SerializeField]
	private TMP_InputField usernameInput;
	[SerializeField]
	private TextMeshProUGUI gameSavedText;
	[SerializeField]
	private TMP_Dropdown gameChoiceDropdown;
	[SerializeField]
	private Transform gameParent;

	private void OnEnable()
	{
		DataEvents.OnGamesLoadedForUser += OnGamesLoadedForUser;
		DataEvents.OnGameSaved += OnGameSaved;
	}

	private void OnDisable()
	{
		DataEvents.OnGamesLoadedForUser -= OnGamesLoadedForUser;
		DataEvents.OnGameSaved -= OnGameSaved;
	}

	private void OnGameSaved()
	{
		gameSavedText.color = Color.white;
		gameSavedText.DOColor(Constants.Transparent, 2f);
	}

	private void OnGamesLoadedForUser(GameData[] datas)
	{
		DisplayLoadedGames(datas);
	}

	protected override void SetupButtons()
	{
		loadGameRecordsForUserButton.onClick.AddListener(() =>
		{
			if(usernameInput.text != "")
			{
				DataEvents.OnLoadAllGamesForUserRequested?.Invoke(usernameInput.text);
			}
		});
		saveGameButton.onClick.AddListener(() =>
		{
			DataEvents.OnSaveGameRequested?.Invoke();
		});
	}

	protected override void SetupInputs()
	{
	}

	private void DisplayLoadedGames(GameData[] datas)
	{

	}
}
