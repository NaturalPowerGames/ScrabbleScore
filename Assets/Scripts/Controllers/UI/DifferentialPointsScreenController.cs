using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DifferentialPointsScreenController : MonoBehaviour
{
	[SerializeField]
	private DifferentialInputController differentialInputPrefab;
	[SerializeField]
	private Transform differentialInputsParent;
	private DifferentialInputController[] differentialInputs;
	[SerializeField]
	private TextMeshProUGUI[] playerNames;
	[SerializeField]
	private Button submitButton, cancelButton;

	private void Awake()
	{
		SetupButtons();
	}

	private void SetupButtons()
	{
		submitButton.onClick.AddListener(() =>
		{
			gameObject.SetActive(false);
			GameEvents.OnDifferentialScoresUpdateRequested?.Invoke(GetDifferentials());
		});
		cancelButton.onClick.AddListener(() =>
		{
			gameObject.SetActive(false);
		});
	}

	private int[] GetDifferentials()
	{
		int[] differentials = new int[differentialInputs.Length];
		for (int i = 0; i < differentialInputs.Length; i++)
		{
			differentials[i] = differentialInputs[i].GetDifferential;
		}
		return differentials;
	}

	public void Initialize(PlayerData[] playerInfos)
	{
		differentialInputsParent.DeleteAllChildren();
		differentialInputs = new DifferentialInputController[playerInfos.Length];
		for (int i = 0; i < differentialInputs.Length; i++)
		{
			differentialInputs[i] = Instantiate(differentialInputPrefab, differentialInputsParent);
			differentialInputs[i].Initialize(playerInfos[i].name);
		}
	}
}
