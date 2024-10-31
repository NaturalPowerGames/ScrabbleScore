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

	//protected virtual void OnEnable()
	//{
	//	ToggleEventSubscriptions(true);
	//}

	//protected virtual void OnDisable()
	//{
	//	ToggleEventSubscriptions(false); do I wanna change this?
	//}

	public virtual void Toggle(bool active)
	{
		background.SetActive(active);
	}
	//protected abstract void ToggleEventSubscriptions(bool active); Do I wanna add this? it's pretty I guess
	protected abstract void SetupButtons();
	protected abstract void SetupInputs();	
}
