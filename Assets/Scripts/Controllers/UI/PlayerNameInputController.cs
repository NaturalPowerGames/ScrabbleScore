using UnityEngine;
using TMPro;

public class PlayerNameInputController : MonoBehaviour
{
	private int index;
	[SerializeField]
	private TMP_InputField nameInput;
	[SerializeField]
	private TMP_Dropdown tableLocation;

	public void Initialize(int index)
	{
		this.index = index;
		var info = Constants.CurrentGame.PlayerInfos[index];
		nameInput.text = info.name;
		tableLocation.SetValueWithoutNotify((int)info.tableLocation);
		SetupInputs();
	}

	private void SetupInputs()
	{
		nameInput.onValueChanged.AddListener((x) =>
		{
			SetupEvents.OnPlayerNameChangeRequested?.Invoke(index, x);
		});

		tableLocation.onValueChanged.AddListener((x) =>
		{
			SetupEvents.OnPlayerTablePositionChangeRequested?.Invoke(index, (TablePositions)x);
		});
	}
}
