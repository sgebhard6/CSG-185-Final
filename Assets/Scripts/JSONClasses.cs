using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONClasses
{
}

[System.Serializable]
public class TopTenScore
{
	public string PlayerName;
	public int Score;
}

[System.Serializable]
public class Score
{
	[HideInInspector]
	public int ScoreID;
	public int PlayerID;
	public int GameTypeID;
	public int Score1;

	public Score (int _playerID, int _gameTypeID, int _score1)
	{
		PlayerID = _playerID;
		GameTypeID = _gameTypeID;
		Score1 = _score1;
	}
}

[System.Serializable]
public class Player
{
	[HideInInspector]
	public int PlayerID;
	public string PlayerName;

	public Player (string _playerName)
	{
		PlayerName = _playerName;
	}
}

[System.Serializable]
public class GameType
{
	[HideInInspector]
	public int GameTypeID;
	public string GameTypeName;

	public GameType (string _gameTypeName)
	{
		GameTypeName = _gameTypeName;
	}
}