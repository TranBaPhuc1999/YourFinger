using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HammerGame : MonoBehaviour
{


    public GameObject destination_Red;
    public GameObject destination_Green;
    public GameObject menuGameOver;
    public GameObject howToPlay;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textBestScore;
    public TextMeshProUGUI textScoreGameOver;
    public TextMeshProUGUI textRevivalTimesGameOver;
    public Button buttonWatchVideo;

    private EffectsGame effectsGame;
    private AdmobManager admobManager;
    private Data data = new Data();
    private bool hand = false;
    private int score;
    private IEnumerator hitFunction;
    public Animator ani;
    private float timeShowAdmob = 600f;
    private float oldTime = 0;

    private void Awake()
    {
        textBestScore.text = "Best: " + data.BestScore;

        effectsGame = FindObjectOfType<EffectsGame>();
        admobManager = FindObjectOfType<AdmobManager>();

        destination_Red.SetActive(false);

        score = data.Score;
        changeValueScore(0);

        if (data.Score == 0)
        {
            data.RevivalTimes = 3;
        }

        if (data.FirstPlay == 1)
        {
            howToPlay.SetActive(true);
            data.FirstPlay = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            onBackHome();

        //Goi ad moi 10 phut
        if (Time.time > oldTime + timeShowAdmob)
        {
            oldTime = Time.time;
            admobManager.ShowInterstitial();
        }
    }

    //Kiểm tra tay và chạy nhạc theo frame của animation
    public void checkHand()
    {
        if (hand)
        {
            if (data.RevivalTimes > 0)
            {
                buttonWatchVideo.interactable = true;
            }
            else
            {
                buttonWatchVideo.interactable = false;
            }

            menuGameOver.SetActive(true);
            textScoreGameOver.text = "Score: " + score;
            textRevivalTimesGameOver.text = data.RevivalTimes + " times left";
            data.Score = 0;
        }
        else
        {
            ani.SetTrigger("Up");
            changeValueScore(1);
            

            if (data.BestScore < data.Score)
            {
                data.BestScore = data.Score;
                textBestScore.text = "Best: " + data.BestScore;
            }
        }
        
    }

    public void checkHandOnTroll()
    {
        if (hand)
        {
            changeValueScore(1);
            

            if (data.BestScore < data.Score)
            {
                data.BestScore = data.Score;
                textBestScore.text = "Best: " + data.BestScore;
            }

            onDown();
        }
        else
        {

            if(score > 0)
            {
                changeValueScore(-1);
            }

            effectsGame.playSound(3, true, false, 1f);
        }

    }


    public void playSoundHammerFromFrame()
    {
        effectsGame.playSound(2, false, false, 1f);
    }


    //Hai hàm này được gọi khi ấn và thả nút ra
    public void onDown()
    {

        if (Random.Range(0, 11) <= 7)
        {
            hitFunction = hit(Random.Range(0.5f, 5f), "Down");
        }
        else
        {
        
            hitFunction = hit(Random.Range(0.5f, 5f), "Troll");
        }

        StartCoroutine(hitFunction);

        hand = true;

        destination_Red.SetActive(true);
        destination_Green.SetActive(false);

        //âm thanh hồi hộp
        effectsGame.playSound(1, false, true, 0.6f);
    }


    public void onUp()
    {
        hand = false;
        
        StopCoroutine(hitFunction);

        destination_Red.SetActive(false);
        destination_Green.SetActive(true);

        effectsGame.stopSound("suspense");
    }

    //Đưa về màn hình chơi và set lại điểm, về màn hình chính
    public void replay()
    {
        menuGameOver.SetActive(false);
        ani.SetTrigger("Up");

        updateScoreToLeaderBoards();

        changeValueScore(-score);

        data.RevivalTimes =3;
    }


    void changeValueScore(int number)
    {
        score += number;
        data.Score = score;
        textScore.text = "Score: " + data.Score;
    }

    public void onBackHome()
    {
        updateScoreToLeaderBoards();

        SceneManager.LoadScene("MainMenu");
    }

    //Hàm chạy ani búa đập, dọa theo thời gian truyền vào
    IEnumerator hit(float timeDelay, string nameTrigger)
    {
            yield return new WaitForSeconds(timeDelay);

            ani.SetTrigger(nameTrigger);
    }


    private void updateScoreToLeaderBoards()
    {
        if (data.BestScore >= data.MinScoreLB)
        {
            if (data.UserScoreLB>0)
            {
                if (data.BestScore > data.UserScoreLB)
                {
                FirebaseDatabase.DefaultInstance
                .GetReference("Leaders").Child(data.ID).Child("score").SetValueAsync(-data.BestScore);
                }
            }
            else
            {
                FirebaseDatabase.DefaultInstance
                .GetReference("Leaders").Child(data.MinIDLB).RemoveValueAsync();

                FirebaseDatabase.DefaultInstance
                .GetReference("Leaders").Child(data.ID).Child("name").SetValueAsync(data.NamePlayer);

                FirebaseDatabase.DefaultInstance
                .GetReference("Leaders").Child(data.ID).Child("score").SetValueAsync(-data.BestScore);
            }
        }
    }

    public void RewardUser()
    {
        menuGameOver.SetActive(false);
        ani.SetTrigger("Up");
        data.Score = score;

        data.RevivalTimes -= 1;
    }
}
