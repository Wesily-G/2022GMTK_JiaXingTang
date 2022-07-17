using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    Button newGameBtn;
    Button continueBtn;
    Button quitBtn;

    private void Awake()
    {
        //newGameBtn = transform.GetChild(1).GetComponent<Button>();
        //continueBtn = transform.GetChild(2).GetComponent<Button>();
        //quitBtn = transform.GetChild(3).GetComponent<Button>();

        //newGameBtn.onClick.AddListener(NewGame);
        //continueBtn.onClick.AddListener(ContinueGame);
        //quitBtn.onClick.AddListener(QuitGame);
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneController.Ins.TransitionToFirstLevel();

    }
    void ContinueGame()
    {
        SceneController.Ins.TransitionToLoadLevel();
    }
    void QuitGame()
    {
        Application.Quit();
        Debug.Log("ÍË³öÓÎÏ·");
    }

}
