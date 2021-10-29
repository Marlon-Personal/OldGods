using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitialScene : MonoBehaviour
{
     public Text dialogue;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrintDialog());
    }

    IEnumerator PrintDialog()
    {
        dialogue.text = "";
        yield return new WaitForSeconds(0.2f);
        dialogue.text = "O";
        yield return new WaitForSeconds(0.2f);
        dialogue.text = "OL";
        yield return new WaitForSeconds(0.2f);
        dialogue.text = "OLD";
        yield return new WaitForSeconds(0.4f);
        dialogue.text = "OLD G";
        yield return new WaitForSeconds(0.2f);
        dialogue.text = "OLD GO";
        yield return new WaitForSeconds(0.2f);
        dialogue.text = "OLD GOD";
        yield return new WaitForSeconds(0.2f);
        dialogue.text = "OLD GODS";

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("1.MainMenuScene");
    }

}
