using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailParts : MonoBehaviour
{
    
    private int myOrder;
    private Transform head;
    public Vector3 lastTransform;
    public Vector3 lookTransform;

    public const int gap = 20;
    private List<Vector3> trace = new List<Vector3>();

    void Start()
    {
        head = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        for (int i = 0; i < head.GetComponent<CharacterMovement>().bodyParts.Count; i++)
        {
            if (gameObject == head.GetComponent<CharacterMovement>().bodyParts[i].gameObject)
            {
                myOrder = i;
            }
        }
        trace.Add(transform.position);
    }


    private void Update()
    {
        trace.Add(transform.position);
        if(trace.Count > gap)
        {
            trace.RemoveAt(0);
        }
        lookTransform = trace[0];
        lastTransform = trace[1];
    }

    private Vector3 movementVelocity;
    [Range(0.0f, 1.0f)]
    public float overTime = 0.2f;
    TailParts tailpartsSC;

    void FixedUpdate()
    {
        if (myOrder == 0)
        {
            transform.position = head.position;
                //Vector3.SmoothDamp(transform.position, head.position, ref movementVelocity, overTime);
            transform.LookAt(head.transform.position);
        }
        else
        {
            GameObject go = head.GetComponent<CharacterMovement>().bodyParts[myOrder - 1];
            tailpartsSC = go.GetComponent<TailParts>();
            transform.position = tailpartsSC.lastTransform;
            //Vector3.SmoothDamp(transform.position, head.GetComponent<CharacterMovement>().bodyParts[myOrder - 1].transform.position, ref movementVelocity, overTime);
            transform.LookAt(tailpartsSC.lookTransform);
        }
    }
}
