using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMenuHome : MonoBehaviour
{
    public TextMeshProUGUI textBestScore;

    private Data data = new Data();
    // Start is called before the first frame update
    void Start()
    {
        textBestScore.text = "Best: "+data.BestScore;
    }

    public void onClickPlay()
    {
        SceneManager.LoadScene("Game");
    }
}   
