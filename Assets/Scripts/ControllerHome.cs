using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

public class ControllerHome : MonoBehaviour
{
    public GameObject yesNoBoxQuitGame;
	public TextMeshProUGUI textBestScore;
	public GameObject buttonMusic;
	public GameObject buttonSound;
	public Sprite spriteMusicOff;
	public Sprite spriteMusicOn;
	public Sprite spriteSoundOff;
	public Sprite spriteSoundOn;

    private AudioSource audioSource;
	private Data data = new Data();

    private void Awake()
	{
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://your-finger-unity.firebaseio.com/");

        getMinScoreLB();
        getUserBestScoreFromLB();

        audioSource = GetComponent<AudioSource>();

        if (data.Music != 0)
        {
            if (audioSource.isPlaying) return;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }


        textBestScore.text = "Best: " + data.BestScore;

        if (data.Music == 1)
        {
            buttonMusic.GetComponent<Image>().sprite = spriteMusicOn;
        }
        else
        {
            buttonMusic.GetComponent<Image>().sprite = spriteMusicOff;
        }

        if (data.Sound == 1)
        {
            buttonSound.GetComponent<Image>().sprite = spriteSoundOn;
        }
        else
        {
            buttonSound.GetComponent<Image>().sprite = spriteSoundOff;
        }
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			yesNoBoxQuitGame.SetActive(true);
	}

	public void onChangeMusic()
	{
		if (data.Music == 1)
		{
			data.Music = 0;
		}
		else
		{
			data.Music = 1;
		}

		if (data.Music == 1)
		{
			buttonMusic.GetComponent<Image>().sprite = spriteMusicOn;
		}
		else
		{
			buttonMusic.GetComponent<Image>().sprite = spriteMusicOff;
		}

		if (data.Music != 0)
		{
			if (audioSource.isPlaying) return;
			audioSource.Play();
		}
		else
		{
			audioSource.Stop();
		}
	}

	public void onChangeSound()
	{
		if (data.Sound == 1)
		{
			data.Sound = 0;
		}
		else
		{
			data.Sound = 1;
		}

		if (data.Sound == 1)
		{
			buttonSound.GetComponent<Image>().sprite = spriteSoundOn;
		}
		else
		{
			buttonSound.GetComponent<Image>().sprite = spriteSoundOff;
		}
	}

	private void getMinScoreLB()
	{
		FirebaseDatabase.DefaultInstance
	.GetReference("Leaders").OrderByChild("score").LimitToLast(1)
	.ValueChanged += HandleValueChangedMinScore;
	}

	void HandleValueChangedMinScore(object sender, ValueChangedEventArgs args)
	{
		if (args.DatabaseError != null)
		{
			Debug.LogError(args.DatabaseError.Message);
			return;
		}
			foreach (DataSnapshot dt in args.Snapshot.Children)
			{
			    data.MinScoreLB = Convert.ToInt32(- (long)dt.Child("score").Value);
			    data.MinIDLB = dt.Key;
			}

        if (data.BestScore > data.MinScoreLB)
        {
			FirebaseDatabase.DefaultInstance
            .GetReference("Leaders").Child(data.MinIDLB).RemoveValueAsync();

			FirebaseDatabase.DefaultInstance
			.GetReference("Leaders").Child(data.ID).Child("name").SetValueAsync(data.NamePlayer);

			FirebaseDatabase.DefaultInstance
			.GetReference("Leaders").Child(data.ID).Child("score").SetValueAsync(-data.BestScore);
		}
	}

	private void getUserBestScoreFromLB()
	{
		FirebaseDatabase.DefaultInstance
	.GetReference("Leaders").OrderByKey().EqualTo(data.ID)
	.ValueChanged += HandleValueChangedUserBestScore;
	}

	void HandleValueChangedUserBestScore(object sender, ValueChangedEventArgs args)
	{
		if (args.DatabaseError != null)
		{
			Debug.LogError(args.DatabaseError.Message);
			return;
		}

        if (args.Snapshot.ChildrenCount == 0)
        {
			data.UserScoreLB = 0;
        }
        else
        {
		    foreach (DataSnapshot dt in args.Snapshot.Children)
		    {
			    data.UserScoreLB = Convert.ToInt32(-(long)dt.Child("score").Value);
		    }
        }
	}
}
