using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSound : MonoBehaviour
{
    public AudioSource audioSource;
    private Vector3 startPos;
    public float desiredDuration = 5f;
    private float elapsedTime;

    public Transform posA;
    public Transform posB;
    public Transform pos—;

    public bool startMove = false;
    public bool startMove2 = false;

    public bool now = true;
    public bool now1 = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPos = transform.position;
    }



    void Update()
    {
        if (startMove)
        {
            if (now)
            {
                startPlay();
                now = false;
            }

            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;


            transform.position = Vector3.Lerp(posA.position, posB.position, percentageComplete);


            if (transform.position == posB.position)
            {
                if (now1)
                {
                    audioSource.loop = false;
                    now1 = false;
                }
                startMove = false;
            }
        }

        if (startMove2)
        {
            if (now)
            {
                startPlay();
                now = false;
            }

            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;


            transform.position = Vector3.Lerp(posA.position, pos—.position, percentageComplete);


            if (transform.position == pos—.position)
            {
                if (now1)
                {
                    audioSource.loop = false;
                    now1 = false;
                }
                startMove = false;
            }
        }
    }

    
    public void StartSound()
    {
        startMove = true;
    }
    
    public void StartSound2()
    {
        startMove2 = true;
    }

    public void startPlay()
    {
        audioSource.Play();
    }
}
