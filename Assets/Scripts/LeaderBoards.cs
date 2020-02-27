using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using TMPro;
using UnityEngine;

public class LeaderBoards : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    private float templateHeight = 75f;
    private List<Transform> listTransform;
    public TextMeshProUGUI textPosBagde;
    public TextMeshProUGUI textBestScore;
    private Data data = new Data();

    private void Awake()
    {
        listTransform = new List<Transform>();
        entryTemplate.gameObject.SetActive(false);

        getHighScores();

        setUpTextBagde();
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
                if (i == 3) break;
            }
        }
        else
        {
            foreach (DataSnapshot dt in args.Snapshot.Children)
            {
                updateHighscoreEntryTransform((string)dt.Child("name").Value, (-(long)dt.Child("score").Value) + "", i);
                i++;
                if (i == 3) break;
            }
        }

        //tìm vị trí của user
        int pos = 0;
        foreach (DataSnapshot dt in args.Snapshot.Children)
        {
            if (data.ID.Equals(dt.Key))
            {
                data.PosInLeaderboard = pos;
                if (-(long)dt.Child("score").Value>= data.BestScore){
                    data.BestScore = Convert.ToInt32(- (long)dt.Child("score").Value);
                    textBestScore.text = "Best: " + data.BestScore;
                }
                else
                {
                    FirebaseDatabase.DefaultInstance
                    .GetReference("Leaders").Child(data.ID).Child("score").SetValueAsync(-data.BestScore);
                }
                break;
            }
            pos++; 
        }

        if (pos == 100) data.PosInLeaderboard = -1;

        setUpTextBagde();
    }


    private void createHighscoreEntryTransform(string nameUser, string scoreUser, int i)
    {
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);

        entryTransform.gameObject.SetActive(true);

        TextMeshProUGUI textRank = entryTransform.Find("rankText").GetComponent<TextMeshProUGUI>();
        textRank.text = (i + 1) + "";
        TextMeshProUGUI textName = entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>();
        textName.text = nameUser;
        TextMeshProUGUI textScore = entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>();
        textScore.text = scoreUser;


        setUpText(textRank, i);
        setUpText(textName, i);
        setUpText(textScore, i);

        listTransform.Add(entryTransform);
    }


    private void updateHighscoreEntryTransform(string nameUser, string scoreUser, int i)
    {
        listTransform[i].Find("nameText").GetComponent<TextMeshProUGUI>().text = nameUser;
        listTransform[i].Find("scoreText").GetComponent<TextMeshProUGUI>().text = scoreUser;
    }

    void setUpText(TextMeshProUGUI text, int i)
    {
        string colorText = "";
        float textSize = 0;
        switch (i)
        {
            case 0: colorText = "#E26363"; textSize = 55; break;
            case 1: colorText = "#D1D43B"; textSize = 50; break;
            case 2: colorText = "#51D993"; textSize = 45; break;
            default: colorText = "#FFFFFF"; textSize = 40; break;
        }
        Color color;
        ColorUtility.TryParseHtmlString(colorText, out color);
        text.color = color;
        text.fontSize = textSize;
    }

    void setUpTextBagde()
    {
        if (data.PosInLeaderboard != -1)
        {
            this.transform.Find("bagde").gameObject.SetActive(true);
            switch (data.PosInLeaderboard)
            {
                case 0: textPosBagde.text = "1st"; break;
                case 1: textPosBagde.text = "2nd"; break;
                case 2: textPosBagde.text = "3rd"; break;
                default: textPosBagde.text = (data.PosInLeaderboard + 1) + "th"; break;
            }
        }
        else
        {
            this.transform.Find("bagde").gameObject.SetActive(false);
        }

        setUpText(textPosBagde, data.PosInLeaderboard);
    }
}
