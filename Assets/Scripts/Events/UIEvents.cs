using System;
using UnityEngine;

public static class UIEvents
{
	public static Action<Screens> OnScreenChangeRequested;
	public static Action<Screens> OnScreenChanged;
	public static Action<RectTransform> OnInputFieldInteractionBegan;
	public static Action<RectTransform> OnInputFieldInteractionEnded;
}
