using UnityEngine;

public abstract class UIController : MonoBehaviour
{
	[SerializeField]
	protected GameObject background;

    protected virtual void Awake()
	{
		SetupButtons();
		SetupInputs();
	}

	public virtual void Toggle(bool active)
	{
		background.SetActive(active);
	}
	protected abstract void SetupButtons();
	protected abstract void SetupInputs();	
}
