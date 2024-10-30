using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnInfoInputScreenController : MonoBehaviour
{
    [SerializeField]
    private Button cancelButton, confirmButton;
    [SerializeField]
    private TMP_InputField wordInput, scoreInput;

	private void Awake()
	{
		SetupButtons();
		SetupInputs();
	}

	private void SetupButtons()
	{
		cancelButton.onClick.AddListener(() =>
		{
			gameObject.SetActive(false);
		});
		confirmButton.onClick.AddListener(() =>
		{
			gameObject.SetActive(false);

		});
	}

	private void SetupInputs()
	{
		wordInput.onEndEdit.AddListener((x) =>
		{
			//todo
		});

		scoreInput.onEndEdit.AddListener((x) =>
		{
			//todo
		});
	}
}
