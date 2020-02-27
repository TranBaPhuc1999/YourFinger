using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using TMPro;
using UnityEngine;

public class ActionNameInNameBox : MonoBehaviour
{
    private Data data = new Data();
    public TMP_InputField InputName;
    public TextMeshProUGUI PreName;
    private List<String> listNameLeaderboards;
    public GameObject menuObject;

    private void Awake()
    {
        listNameLeaderboards = new List<string>();
        PreName.text = data.NamePlayer;
        addNameToList();
    }

    private void addNameToList()
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

        listNameLeaderboards.Clear();
        foreach (DataSnapshot dt in args.Snapshot.Children)
        {
            listNameLeaderboards.Add((string)dt.Child("name").Value);
        }
    }

    public void updateNamePlayer(GameObject gameObject)
    {

        if (!InputName.text.Equals(""))
        {
            if (checkTheSameName())
            {
                gameObject.SetActive(true);
            }
            else
            {
                data.NamePlayer = InputName.text;
                InputName.text = "";
                PreName.text = data.NamePlayer;

                if (data.PosInLeaderboard != 0)
                {
                    FirebaseDatabase.DefaultInstance
                    .GetReference("Leaders").Child(data.ID).Child("name").SetValueAsync(data.NamePlayer);
                }

                menuObject.SetActive(true);
            }


        }
        else
        {
            menuObject.SetActive(true);
        }
    }

    public void clickButtonCancel()
    {
        InputName.text = "";
    }

    public Boolean checkTheSameName()
    {
        String nameUser = InputName.text.ToLower();

        foreach(String name in listNameLeaderboards)
        {
            if (nameUser.Equals(name.ToLower())){
                return true;
            }
        }
        return false;
    }
}
