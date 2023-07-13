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

    [SerializeField]
    public Animator animator;

    public float moveSpeed = 3f;
    public float jumpSpeed = 10f;
    public float gravity = 0.5f;
    public float rotateSpeed = 1.5f;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private bool isJumping;

    [SerializeField] private GameObject bodyPrefab;

    public List<GameObject> bodyParts = new List<GameObject>();


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        bodyParts.Add(gameObject);
    
    }

    private void FixedUpdate()
    {
        if (!LevelManager.Instance.isPaused) {

            float moveX = Input.GetAxis("Horizontal");
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

    



    private void GrowTail()
    {
        GameObject tail = Instantiate(bodyPrefab);
        bodyParts.Add(tail);

        chickCount++;
        UpdateScore(chickCount);
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
    }

    private void OnEnable()
    {
        // Subscribe to the event when the player becomes active/enabled
        FindObjectOfType<EventManager>().TailGrow.AddListener(GrowTail);
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

}
