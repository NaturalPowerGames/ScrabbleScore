using UnityEngine;
using TMPro; 

public class DifferentialInputController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField differentialInput;
    [SerializeField]
    private TextMeshProUGUI playerNameDisplay;

	private void Awake()
	{
		SetupInputs();
	}

	public void Initialize(string playerName)
	{
        playerNameDisplay.text = playerName;
	}

    public int GetDifferential
	{
		get
		{
			int.TryParse(differentialInput.text, out int score);
			return score;
		}
	}

	private void SetupInputs()
	{
		differentialInput.onSelect.AddListener((x) =>
		{
			UIEvents.OnInputFieldInteractionBegan?.Invoke(differentialInput.GetComponent<RectTransform>());
		});
		differentialInput.onDeselect.AddListener((x) =>
		{
			UIEvents.OnInputFieldInteractionEnded?.Invoke(differentialInput.GetComponent<RectTransform>());
		});
	}
    
}
