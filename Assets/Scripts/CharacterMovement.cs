using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpSpeed = 5f;
    public float gravity = 0.5f;
    public int gap = 20;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private bool isJumping;

    [SerializeField] private GameObject bodyPrefab;

    public List<GameObject> bodyParts = new List<GameObject>();
    private List<Vector3> PositionHistory = new List<Vector3>();
    


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        for (int i = 0; i < 15; i++)
        {
            GrowTail();
            
        }
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveX, 0f, moveZ);
        moveDirection = transform.TransformDirection(moveDirection) * moveSpeed;

        if (characterController.isGrounded)
        {
            isJumping = false;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
                isJumping = true;
            }
        }

        moveDirection.y -= gravity;

        characterController.Move(moveDirection * Time.deltaTime);

        if (!isJumping)
            moveDirection.y = 0f;
        PositionHistory.Insert(0, transform.position);

        //TailMovement();

        //if (PositionHistory.Count > bodyParts.Count * gap)
        //{
        //    PositionHistory.RemoveAt(PositionHistory.Count -1);
        //}

    }

    private void TailMovement()
    {
        
        int index = 0;
        foreach(var part in bodyParts)
        {
            Vector3 point = PositionHistory[Mathf.Min(index * gap , PositionHistory.Count - 1)];
            Vector3 moveDirection = point - part.transform.position;
            part.transform.position += moveDirection * moveSpeed * Time.deltaTime;
            part.transform.LookAt(point);
            index++;
        }
        print(PositionHistory.Count);

    }

    private void GrowTail()
    {
        GameObject tail = Instantiate(bodyPrefab);
        bodyParts.Add(tail);
    }
}
