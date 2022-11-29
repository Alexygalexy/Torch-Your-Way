using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    private float x;
    private float z;
    public Vector3 move;

    public float speed = 2f;
    public float speedSlow = 0.5f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    Vector3 velocity;
    bool isGrounded;

    [Header("Sprint")]
    public bool isSprinting;
    private bool isWebbed = false;

    [Header("HeadBob")]
    public Transform torchParent;

    private Vector3 torchParentOrigin;
    private Vector3 targetTorchBobPosition;

    private float movementCounter;
    private float idleCounter;

    [Header("Footstep Parameters")]
    public bool useFootsteps = true;
    public float baseStepSpeed = 0.5f;
    public float sprintStepMultiplier = 0.6f;
    public AudioSource footstepAudioSource = default;
    public AudioClip[] caveClips = default;
    private float footstepTimer = 0;
    private float GetCurrentOffset => isSprinting ? baseStepSpeed * sprintStepMultiplier : baseStepSpeed;


    void Start()
    {
        //HeadBob
        torchParentOrigin = torchParent.localPosition;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //HeadBob
        if (x >= -0.2 && x <= 0.2 && z >= -0.2 && z <= 0.2)
        {
            HeadBob(idleCounter, 0.025f, 0.025f);
            idleCounter += Time.deltaTime;
            torchParent.localPosition = Vector3.Lerp(torchParent.localPosition, targetTorchBobPosition, Time.deltaTime * 2f);
        } else
        {
            HeadBob(movementCounter, 0.031f, 0.031f);
            movementCounter += Time.deltaTime * 3f;
            torchParent.localPosition = Vector3.Lerp(torchParent.localPosition, targetTorchBobPosition, Time.deltaTime * 7f);
        }

        if (useFootsteps)
        {
            Handle_Footsteps();
        }

        if (!isWebbed)
        {
            HandleSprinting();

            if (isSprinting)
            {
                speed = 3.5f;
            }
            else
            {
                speed = 2f;
            }
        }
        
    }

    void HeadBob(float p_z, float p_x_intensity, float p_y_intensity)
    {
        targetTorchBobPosition = torchParentOrigin + new Vector3(Mathf.Cos(p_z) * p_x_intensity, Mathf.Sin(p_z * 2) * p_y_intensity, 0);
    }

    private void Handle_Footsteps()
    {
        if (move == Vector3.zero)
        {
            return;
        }

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0)
        {
            if (Physics.Raycast(groundCheck.transform.position, Vector3.down, out RaycastHit hit, 1))
            {
                switch (hit.collider.tag)
                {
                    case "Footstep":
                        footstepAudioSource.PlayOneShot(caveClips[Random.Range(0, caveClips.Length - 1)]);
                        break;
                }
            }
            
            footstepTimer = GetCurrentOffset;
        }
    }

    private void HandleSprinting()
    {
        
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpiderWeb")
        {
            isWebbed = true;
            Debug.Log("Adasdaasdsadasd");
            StartCoroutine(SpiderWebSlow());
        }
    }

    IEnumerator SpiderWebSlow()
    {
        speed = speedSlow;


        yield return new WaitForSeconds(4f);

        speed = 2f;
        isWebbed = false;
    }


    
}
