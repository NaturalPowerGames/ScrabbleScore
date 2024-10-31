using UnityEngine;
using TMPro; 

public class DifferentialInputController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField differentialInput;
    [SerializeField]
    private TextMeshProUGUI playerNameDisplay;
	
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
    
}
