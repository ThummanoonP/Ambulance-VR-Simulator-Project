using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ButtonScrip : MonoBehaviour // คลาส Menu ปุ่มกด
{
    public void StartProgram()
    {
        SceneManager.LoadScene("AmbulanceScene", LoadSceneMode.Single);
    }

    public void QuitProgram()
    {
        if (EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.ExitPlaymode();
        }
        else
        Application.Quit();

    }

    public void TitleMenu()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialScene", LoadSceneMode.Single);
    }

}

