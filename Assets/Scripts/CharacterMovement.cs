using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : Chicken
{
    private EventManager eventManager;

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
        eventManager = FindObjectOfType<EventManager>();
        characterController = GetComponent<CharacterController>();
        bodyParts.Add(gameObject);
        
    }

    private void Update()
    {
        TraceMaker();

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0f, 0f, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);
        transform.Rotate(0f, moveX * rotateSpeed , 0f);

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
        

    }

    

    private void GrowTail()
    {
        GameObject tail = Instantiate(bodyPrefab);
        bodyParts.Add(tail);
    }

    private OrbSpawn orbSpawn;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "orb")
        {
            print("chmove");
            Orb orb= hit.gameObject.GetComponent<Orb>();
            //orb.OrbCollected();
            orb.gameObject.SetActive(false);
            orbSpawn = FindObjectOfType<OrbSpawn>();
            orbSpawn.orbCount.Value -= 1;
            Destroy(orb);
            GrowTail();
        }
    }

    private void OnEnable()
    {
        // Subscribe to the event when the player becomes active/enabled
        FindObjectOfType<EventManager>().TailGrow.AddListener(GrowTail);
    }

    
}
