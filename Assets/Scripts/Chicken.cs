using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public Vector3 lastTransform;
    public Vector3 lookTransform;
    protected List<Vector3> trace = new List<Vector3>();
    protected const int gap = 20;


    protected void TraceMaker()
    {
        trace.Add(transform.position);
        if (trace.Count > gap)
        {
            trace.RemoveAt(0);
        }
        lastTransform = trace[0];
    }
}
