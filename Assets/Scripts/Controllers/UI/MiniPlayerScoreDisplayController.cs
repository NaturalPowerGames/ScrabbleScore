using UnityEngine;
using TMPro;

public class MiniPlayerScoreDisplayController : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI playerName, playerScore;

	public void UpdateDisplay(string playerName, int playerScore)
	{
		this.playerName.text = playerName;
		this.playerScore.text = playerScore.ToString();
	}
}
