using System;
using UnityEngine;

public class DebugInfoDisplayController : MonoBehaviour
{
	[SerializeField]
	private bool showLogs;

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

	private void OnTurnEnded(TurnData obj)
	{
		if(showLogs)
		Debug.Log(obj.ToString());
	}

	private void OnPlayerInfoChanged(PlayerData info)
	{
		if(showLogs)
		Debug.Log(info.ToString());
	}
}
