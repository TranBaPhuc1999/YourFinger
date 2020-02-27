using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActionMenuGame : MonoBehaviour
{

    public GameObject Menu;
    public GameObject yesNoBoxQuitGame;
    public GameObject buttonMusic;
    public GameObject buttonSound;
    public Sprite spriteMusicOff;
    public Sprite spriteMusicOn;
    public Sprite spriteSoundOff;
    public Sprite spriteSoundOn;
    

    private Data data = new Data();
    // Start is called before the first frame update
    void Start()
    {

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


}
