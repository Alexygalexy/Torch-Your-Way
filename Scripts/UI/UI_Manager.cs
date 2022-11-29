using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameObject bg;
    public GameObject mainMenu;
    public GameObject options;
    public GameObject goal1;
    public GameObject goal2;
    public GameObject goal3;

    public MouseLook mouseSens;
    public Slider sens;

    private bool canContinue;



    void Start()
    {

        
    }

    void Update()
    {
        if (!canContinue && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            canContinue = true;
            Cursor.lockState = CursorLockMode.None;
            mouseSens.mouseSensitivity = 0f;
            bg.SetActive(true);
        }
        else if (canContinue && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            canContinue = false;
            Cursor.lockState = CursorLockMode.Locked;
            mouseSens.mouseSensitivity = sens.value;
            bg.SetActive(false);
        }
        
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void OptionsMenu()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void returnToMenu()
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void returnToMenuScene()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void Page1()
    {
        mainMenu.SetActive(false);
        goal1.SetActive(true);
    }
    public void Page2()
    {
        goal1.SetActive(false);
        goal2.SetActive(true);
    }
    public void Page3()
    {
        goal2.SetActive(false);
        goal3.SetActive(true);
    }
}
