using UnityEngine;
using System.Collections;

public class Data
{
	private const string id = "ID";
	public string ID
	{
		get { return PlayerPrefs.GetString(id, SystemInfo.deviceUniqueIdentifier); }
		set { PlayerPrefs.SetString(id, value); }
	}

	private const string namePlayer = "NamePlayer";
	public string NamePlayer
	{
		get { return PlayerPrefs.GetString(namePlayer, SystemInfo.deviceUniqueIdentifier); }
		set { PlayerPrefs.SetString(namePlayer, value); }
	}

	private const string posInLeaderboard = "PosInLeaderboard";
	public int PosInLeaderboard
	{
		get { return PlayerPrefs.GetInt(posInLeaderboard, -1); }
		set {PlayerPrefs.SetInt(posInLeaderboard, value);}
	}


	private const string score = "Score";
	public int Score
	{
		get { return PlayerPrefs.GetInt(score, 0); }
		set { PlayerPrefs.SetInt(score, value); }
	}

	private const string bestScore = "BestScore";
	public int BestScore
	{
		get { return PlayerPrefs.GetInt(bestScore, 0); }
		set { PlayerPrefs.SetInt(bestScore, value); }
	}

	private const string userScoreLB = "UserScoreLB";
	public int UserScoreLB
	{
		get { return PlayerPrefs.GetInt(userScoreLB, 0); }
		set { PlayerPrefs.SetInt(userScoreLB, value); }
	}

	private const string minScoreLB = "MinScoreLB";
	public int MinScoreLB
	{
		get { return PlayerPrefs.GetInt(minScoreLB, 0); }
		set { PlayerPrefs.SetInt(minScoreLB, value); }
	}

	private const string minIDLB = "MinIDLB";
	public string MinIDLB
	{
		get { return PlayerPrefs.GetString(minIDLB, ""); }
		set { PlayerPrefs.SetString(minIDLB, value); }
	}

	private const string sound = "Sound";
	public int Sound
	{
		get { return PlayerPrefs.GetInt(sound, 1); }
		set { PlayerPrefs.SetInt(sound, value); }
	}


	private const string music = "Music ";
	public int Music
	{
		get { return PlayerPrefs.GetInt(music, 1); }
		set { PlayerPrefs.SetInt(music, value); }
	}

	private const string firstPlay = "FirstPlay";
	public int FirstPlay
	{
		get { return PlayerPrefs.GetInt(firstPlay, 1); }
		set { PlayerPrefs.SetInt(firstPlay, value); }
	}

	private const string revivalTimes = "RevivalTimes";
	public int RevivalTimes
	{
		get { return PlayerPrefs.GetInt(revivalTimes, 3); }
		set { PlayerPrefs.SetInt(revivalTimes, value); }
	}
}
