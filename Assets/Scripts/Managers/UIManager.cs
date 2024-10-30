using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField, EnumNamedArray(typeof(Screens))]
	private UIController[] screens;

	private void Awake()
	{
		OnScreenChangeRequested(Screens.BasicSetup);
	}

	private void OnEnable()
	{
		UIEvents.OnScreenChangeRequested += OnScreenChangeRequested;
	}

	private void OnDisable()
	{
		UIEvents.OnScreenChangeRequested -= OnScreenChangeRequested;
	}

	private void OnScreenChangeRequested(Screens screen)
	{
		DisableAllScreens();
		screens[(int)screen].Toggle(true);
	}

	private void DisableAllScreens()
	{
		for (int i = 0; i < screens.Length; i++)
		{
			screens[i].Toggle(false);
		}
	}
}
