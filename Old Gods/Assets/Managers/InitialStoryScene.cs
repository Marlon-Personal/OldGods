using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitialStoryScene : MonoBehaviour
{
    public Text dialogue;
    public Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrintDialog());
    }

    IEnumerator PrintDialog()
    {
        dialogue.text = "The story will go here";
        buttonText.text = "Skip Scene";
        yield return new WaitForSeconds(2f);
        dialogue.text = "The user will be able to skip the story by clicking next";
        yield return new WaitForSeconds(2f);
        dialogue.text = "End of story";
        buttonText.text = "Continue";
    }

    public void CreateGame()
    {
        SceneManager.LoadScene("2.PlayerCreationScene");
    }
}
