using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{

    public Slider staminaBar;

    private float maxStamina = 100f;
    private float currentStamina;

    public float staminaDeplete;


    public float waitMaxTime = 2f;
    public float currentTime = 0f;

    public static Stamina instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentStamina = maxStamina;
    }

    void Update()
    {
        staminaBar.value = currentStamina;

        if (currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
        }

        if (currentStamina <= 0f)
        {


            currentTime = waitMaxTime;

            if (currentTime >= waitMaxTime)
            {
                currentTime -= Time.deltaTime;
            }
            else if (currentTime <= 0f)
            {
                IncreaseStamina();
            }

            
        }
        
    }

    public void ReduceStamina()
    {
        currentStamina -= staminaDeplete;
    }

    public void IncreaseStamina()
    {
        currentStamina += staminaDeplete + 5f;
    }
}
