using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

public class SaverLoader
{
	private string baseSaveLocation;

	public SaverLoader()
	{
		baseSaveLocation = Application.persistentDataPath + "/Saves";
		if (!Directory.Exists(baseSaveLocation))
		{
			Directory.CreateDirectory(baseSaveLocation);
		}
	}

	public void SaveGame(GameData gameData)
	{
		var json = JsonUtility.ToJson(gameData);
		File.WriteAllText(FinalSaveLocation(gameData.MatchID), json);
	}

	public GameData[] LoadGamesFromLocalStorage(string participant)
	{
		// Get all files in the baseSaveLocation directory with ".json" extension
		string[] saveFiles = Directory.GetFiles(baseSaveLocation, "*.json");

		List<GameData> loadedGames = new List<GameData>();

		foreach (var filePath in saveFiles)
		{
			// Read the file content as JSON
			string json = File.ReadAllText(filePath);

			// Deserialize JSON to GameData object
			GameData gameData = JsonUtility.FromJson<GameData>(json);

			// Check if the GameData participant matches the specified participant
			if (gameData.Players.Any(player => player.name == participant))
			{
				loadedGames.Add(gameData);
			}
		}
		return loadedGames.ToArray();
	}

	private string FinalSaveLocation(Guid guid)
	{
		return $"{baseSaveLocation}/{guid}.json";
	}
}
