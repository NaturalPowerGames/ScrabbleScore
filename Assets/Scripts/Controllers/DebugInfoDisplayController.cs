using System;
using UnityEngine;

public class DebugInfoDisplayController : MonoBehaviour
{
	private void OnEnable()
	{
		SetupEvents.OnPlayerInfoChanged += OnPlayerInfoChanged;
		GameEvents.OnTurnEnded += OnTurnEnded;
	}

	private void OnDisable()
	{
		SetupEvents.OnPlayerInfoChanged -= OnPlayerInfoChanged;
		GameEvents.OnTurnEnded -= OnTurnEnded;
	}

	private void OnTurnEnded(TurnInfo obj)
	{
		Debug.Log(obj.ToString());
	}

	private void OnPlayerInfoChanged(PlayerInfo info)
	{
		Debug.Log(info.ToString());
	}
}
