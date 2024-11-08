using UnityEngine;
using TMPro;
using UnityEngine.Localization;

public class GameRecordDisplayController : MonoBehaviour
{
	[SerializeField]
	private PlayerRecordDisplayController recordPrefab;
	[SerializeField]
	private Transform recordParent;
	[SerializeField]
	private TextMeshProUGUI turnCount, date, totalTime;

	public void Initialize(GameData game)
	{
		DisplayRecords(game);
	}

	private void DisplayRecords(GameData game)
	{
		recordParent.RemoveAllChildren();
		for (int i = 0; i < game.PlayerAmount; i++)
		{
			var record = Instantiate(recordPrefab, recordParent);
			record.Initialize(game.Players[i]);
		}

		turnCount.text = $"{Constants.StringTable.GetLocalizedString("Turns")}: {game.TurnCount}";
		date.text = $"{Constants.StringTable.GetLocalizedString("Date")}: {game.MatchDate}";
		totalTime.text = $"{Constants.StringTable.GetLocalizedString("Duration")}: {game.TimeInGame:m\\:ss}";
	}
}
