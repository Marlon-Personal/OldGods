using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("3.PlayerHubScene");
    }

    public void CreateGame()
    {
        SceneManager.LoadScene("2.OpeningDialogueScene");
    }
}
