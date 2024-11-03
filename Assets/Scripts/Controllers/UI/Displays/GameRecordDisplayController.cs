using UnityEngine;

public class GameRecordDisplayController : MonoBehaviour
{
	[SerializeField]
	private PlayerRecordDisplayController recordPrefab;
	[SerializeField]
	private Transform recordParent;

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
	}
}
