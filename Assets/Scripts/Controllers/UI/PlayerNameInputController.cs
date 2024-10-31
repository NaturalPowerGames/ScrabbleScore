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
		nameInput.text = $"Jugador {index + 1}";
		tableLocation.SetValueWithoutNotify(0);
		SetupInputs();
	}

	private void SetupInputs()
	{
		nameInput.onEndEdit.AddListener((x) =>
		{
			SetupEvents.OnPlayerNameChangeRequested?.Invoke(index, x);
		});

		tableLocation.onValueChanged.AddListener((x) =>
		{
			SetupEvents.OnPlayerTablePositionChangeRequested?.Invoke(index, (TablePositions)x);
		});
	}
}
