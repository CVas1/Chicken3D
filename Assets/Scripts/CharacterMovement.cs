using Lean.Touch;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : Chicken
{
    public event Action<int> OnScoreUpdated;
    private int chickCount = 0;

    [SerializeField] public Animator animator;
    [SerializeField] private AudioSource orbSound;


    public float moveSpeed = 3f;
    public float jumpSpeed = 10f;
    public float gravity = 0.5f;
    public float rotateSpeed = 1.5f;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private bool isJumping;
    float moveX = 0f;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private GameObject bodyPrefabClown;
    [SerializeField] private GameObject bodyPrefabHat;


    public List<GameObject> bodyParts = new List<GameObject>();

    private GameMaster gameMaster;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        bodyParts.Add(gameObject);
        gameMaster = FindAnyObjectByType<GameMaster>();
        orbSound = GetComponent<AudioSource>();
        clown = LevelManager.Instance.clownChick;
        hat = LevelManager.Instance.hatChick;
    }

    private void FixedUpdate()
    {
        if (!gameMaster.isPaused) {

            
            float moveZ = Input.GetAxis("Vertical");

            moveDirection = new Vector3(0f, 0f, moveZ);
            moveDirection = transform.TransformDirection(moveDirection);
            transform.Rotate(0f, moveX * rotateSpeed, 0f);

            if (characterController.isGrounded)
            {
                isJumping = false;

                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                    isJumping = true;
                }
            }

            moveDirection.y -= gravity * Time.deltaTime;

            characterController.Move(moveDirection.normalized * Time.deltaTime * moveSpeed);

            if (!isJumping)
                moveDirection.y = 0f;

            TraceMaker();
        }
        
    }


    private int clown;
    private int hat;

    private void GrowSkinIfHave() 
    {

        if(clown > 0)
        {
            GameObject tail = Instantiate(bodyPrefabClown);
            bodyParts.Add(tail);
            clown -= 1;
        }
        else if(hat > 0)
        {
            GameObject tail = Instantiate(bodyPrefabHat);
            bodyParts.Add(tail);
            hat -= 1;
        }
        else
        {
            GameObject tail = Instantiate(bodyPrefab);
            bodyParts.Add(tail);
        }
    }

    private void GrowTail()
    {
        LevelManager.Instance.coin += 1;
        GrowSkinIfHave();

        chickCount++;
        UpdateScore(chickCount);

        orbSound.Play();
    }

    public void UpdateScore(int newScore)
    {
        OnScoreUpdated?.Invoke(newScore);
    }


    private OrbSpawn orbSpawn;

    private void OnTriggerEnter(Collider hit)
    {

        if (hit.gameObject.tag == "orb")
        {
            EatAnimation();
            Orb orb = hit.gameObject.GetComponent<Orb>();
            orb.OrbCollected();
            GrowTail();

        }
        else if(hit.gameObject.tag == "object")
        {
            gameMaster.GameOver();
        }
    }

    private void OnEnable()
    {
        // Subscribe to the event when the player becomes active/enabled
        FindObjectOfType<EventManager>().TailGrow.AddListener(GrowTail);
        LeanTouch.OnFingerUpdate += HandleFingerUpdate;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerUpdate -= HandleFingerUpdate;
    }

    private void RunAnimation()
    {
        animator.SetBool("Run", true);
    }
    private void RunAnimationFalse()
    {
        animator.SetBool("Run", false);
    }
    private void EatAnimation()
    {
        animator.SetTrigger("Eat");
    }

    //---------------------------------------LeanTouch--------------------------------------------------
    

    private void HandleFingerUpdate(LeanFinger finger)
    {
        if (finger.Up)
        {
            NoFinger();
        }
        else if (finger.ScreenPosition.x < Screen.width / 2)
        {
            LeftSideTouched();
        }
        else if (finger.ScreenPosition.x > Screen.width / 2)
        {
            RightSideTouched();
        }
        
    }

    private void LeftSideTouched()
    {
        moveX = Mathf.Lerp(moveX, -1f, 0.1f);
    }

    private void RightSideTouched()
    {
        moveX = Mathf.Lerp(moveX , 1f, 0.1f);
    }
    private void NoFinger()
    {
        moveX = 0f;
    }

}
