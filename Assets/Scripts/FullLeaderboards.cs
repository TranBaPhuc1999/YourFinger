using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using TMPro;
using UnityEngine;

public class FullLeaderboards : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    private float templateHeight = 80f;
    private List<Transform> listTransform;
    private List<string> listNameUserLeaderboards;

    private void Awake()
    {
        listTransform = new List<Transform>();
        entryTemplate.gameObject.SetActive(false);
        listNameUserLeaderboards = new List<string>();

        getHighScores();

    }

    private void getHighScores()
    {
        FirebaseDatabase.DefaultInstance
    .GetReference("Leaders").OrderByChild("score").LimitToFirst(100)
    .ValueChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        int i = 0;
        if (listTransform.Count == 0)
        {
            foreach (DataSnapshot dt in args.Snapshot.Children)
            {
                createHighscoreEntryTransform((string)dt.Child("name").Value, (-(long)dt.Child("score").Value) + "", i);
                i++;
            }
        }
        else
        {
            foreach (DataSnapshot dt in args.Snapshot.Children)
            {
                updateHighscoreEntryTransform((string)dt.Child("name").Value, (-(long)dt.Child("score").Value) + "", i);
                i++;
            }
        }

        listNameUserLeaderboards.Clear();

        foreach (DataSnapshot dt in args.Snapshot.Children)
        {
            listNameUserLeaderboards.Add((string)dt.Child("name").Value);
        }
    }


    private void createHighscoreEntryTransform(string nameUser, string scoreUser, int i)
    {
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, - templateHeight * i);

        entryTransform.gameObject.SetActive(true);
        entryTransform.Find("background").gameObject.SetActive(i % 2 == 0);

        TextMeshProUGUI textRank = entryTransform.Find("rankText").GetComponent<TextMeshProUGUI>();
        textRank.text = (i + 1) + "";
        TextMeshProUGUI textName = entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>();
        textName.text = nameUser;
        TextMeshProUGUI textScore = entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>();
        textScore.text = scoreUser;

        string colorText = "";
        float textSize = 0;

        switch (i)
        {
            case 0: colorText = "#E26363"; textSize = 55; break;
            case 1: colorText = "#D1D43B"; textSize = 50; break;
            case 2: colorText = "#51D993"; textSize = 45; break;
            default: colorText = "#FFFFFF"; textSize = 45; break;
        }
        Color color;
        ColorUtility.TryParseHtmlString(colorText, out color);
        textRank.color = color;
        textName.color = color;
        textScore.color = color;
        textRank.fontSize = textSize;
        textName.fontSize = textSize;
        textScore.fontSize = textSize;



        listTransform.Add(entryTransform);
    }


    private void updateHighscoreEntryTransform(string nameUser, string scoreUser, int i)
    {
        listTransform[i].Find("nameText").GetComponent<TextMeshProUGUI>().text = nameUser;
        listTransform[i].Find("scoreText").GetComponent<TextMeshProUGUI>().text = scoreUser;
    }

}
