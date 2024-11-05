using UnityEngine;
using TMPro;

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
		for (int i = 0; i < game.PlayerAmount; i++)
		{
			var record = Instantiate(recordPrefab, recordParent);
			record.Initialize(game.Players[i]);
		}
		turnCount.text = $"Turnos: {game.TurnCount}";
		date.text = $"Fecha: {game.MatchDate}";
		totalTime.text = $"Duracion: {game.TimeInGame:m\\:ss}";
	}
}
