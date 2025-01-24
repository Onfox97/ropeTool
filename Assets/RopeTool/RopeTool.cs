using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (LineRenderer))]
[ExecuteInEditMode]
public class RopeTool : MonoBehaviour
{
    [SerializeField] bool bakeRope;
    LineRenderer lineRenderer;

    [SerializeField] Transform ropeStart;
    [SerializeField] Transform ropeEnd;

    [SerializeField] AnimationCurve ropeCurve;
    [SerializeField] int pointsPerUnit = 3;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if(bakeRope)
        {
            BakeRope();
            bakeRope = false;
        }
    }

    void BakeRope()
    {
        List<Vector3> bodyLana = new List<Vector3>();

        float délkaLana = Vector3.Distance(ropeStart.position,ropeEnd.position);

        int početBodů = Mathf.RoundToInt(délkaLana * pointsPerUnit);

        bodyLana.Add(ropeStart.position);
        bodyLana.Add(ropeEnd.position);

        lineRenderer.SetPositions(bodyLana.ToArray());
    }
}
