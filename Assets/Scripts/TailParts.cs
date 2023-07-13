using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailParts : Chicken
{
    private int myOrder;
    private Transform head;
    TailParts tailpartsSC;

    private GameMaster gameMaster;


    void Start()
    {
        head = GameObject.FindGameObjectWithTag("Player").gameObject.transform;

        gameMaster = FindAnyObjectByType<GameMaster>();


        for (int i = 0; i < head.GetComponent<CharacterMovement>().bodyParts.Count; i++)
        {
            if (gameObject == head.GetComponent<CharacterMovement>().bodyParts[i].gameObject)
            {
                myOrder = i;
            }
        }
        if (myOrder != 0)
        {
            GameObject go = head.GetComponent<CharacterMovement>().bodyParts[myOrder - 1];
            tailpartsSC = go.GetComponent<TailParts>();
        }
        trace.Add(transform.position);
        
    }


    private void FixedUpdate()
    {

        if (!gameMaster.isPaused)
        {
            TraceMaker();
            moveTail();
        }
        
        
    }

    //private Vector3 movementVelocity;
    //[Range(0.0f, 1.0f)]
    //public float overTime = 0.2f;

   
    private void moveTail()
    {
        if (myOrder == 1)
        {
            transform.LookAt(head.transform.position);
            transform.position = head.GetComponent<CharacterMovement>().lastTransform;
                //Vector3.SmoothDamp(transform.position, head.position, ref movementVelocity, overTime);
        }
        else
        {

            transform.LookAt(tailpartsSC.lastTransform);
            transform.position = tailpartsSC.lastTransform;
            //Vector3.SmoothDamp(transform.position, head.GetComponent<CharacterMovement>().bodyParts[myOrder - 1].transform.position, ref movementVelocity, overTime);
        }
    }
}
