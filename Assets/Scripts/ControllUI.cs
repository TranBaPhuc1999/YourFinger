using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

public class ControllUI : MonoBehaviour
{
    private GameObject gameObjectBackFromStore;

    public void openUI(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void closeUI(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void closeGame()
    {
        Application.Quit();
    }

    public void setGameObjectBackFromStore(GameObject gameObject)
    {
        gameObjectBackFromStore = gameObject;
    }

    public void goToStore()
    {
        Application.OpenURL("market://details?id=com.zipenter.yourfinger");
    }

    public void openGameObjectBackFromStore()
    {
        openUI(gameObjectBackFromStore);
    }
}
