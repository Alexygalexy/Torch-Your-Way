using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrystalValueStore : MonoBehaviour
{
    private bool endGame = false;

    [Header("PickAxes")]
    public GameObject[] pickaxes;

    [Header("Collectable Crystals")]
    public GameObject[] collectCrystals;

    public bool[] crystalInHand;


    [Header("Crystals")]
    public GameObject[] crystals;
    private int chosenCrystals = 0;
    public Image[] crystalUI;

    [Header("TextBox")]
    public Text notificationTextBox;
    private float timeTextToAppear = 2f;
    private float timeWhenDisappear;


    void Start()
    {
        chosenCrystals = Random.Range(0, 3);
        crystals[chosenCrystals].SetActive(true);
        pickaxes[chosenCrystals].SetActive(false);
        collectCrystals[chosenCrystals].SetActive(false);
        crystalInHand[chosenCrystals] = true;
        crystalUI[chosenCrystals].enabled = true;



    }

    void Update()
    {

        if (notificationTextBox.enabled && (Time.time >= timeWhenDisappear))
        {
            notificationTextBox.enabled = false;
        }

    }

    public void CrystalAppear_Teal()
    {
        crystalInHand[0] = true;
        crystalUI[0].enabled = true;
    }
    public void CrystalAppear_Blue()
    {
        crystalInHand[1] = true;
        crystalUI[1].enabled = true;
    }
    public void CrystalAppear_Red()
    {
        crystalInHand[2] = true;
        crystalUI[2].enabled = true;
    }
    public void CrystalAppear_Green()
    {
        crystalInHand[3] = true;
        crystalUI[3].enabled = true;
    }

    public void PlaceCrystals()
    {
        if (!endGame)
        {
            if (crystalInHand[0] && crystalInHand[1] && crystalInHand[2] && crystalInHand[3])
            {
                crystals[0].SetActive(true);
                crystals[1].SetActive(true);
                crystals[2].SetActive(true);
                crystals[3].SetActive(true);
                endGame = true;
                notificationTextBox.text = "The door is unlocked. OPEN IT!";
                notificationTextBox.enabled = true;
                timeWhenDisappear = Time.time + timeTextToAppear;
            }
            else
            {
                notificationTextBox.text = "Find every crystal!";
                notificationTextBox.enabled = true;
                timeWhenDisappear = Time.time + timeTextToAppear;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Congrats");
        }
    }
}
