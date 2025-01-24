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
    [SerializeField] float curveWeight = 2;
    [SerializeField] int pointsPerUnit = 3;

    [SerializeField] bool fixEndPoint;
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
        int početBodů = Mathf.RoundToInt(délkaLana * pointsPerUnit) +1;
        float roztečBodů = délkaLana/(početBodů-1);
        //zpočítání vzdálenosti mezi jednotlivými body
        Vector3 směrLana = ropeEnd.position - ropeStart.position;
        směrLana.Normalize();


        Debug.Log("délka lana :" + délkaLana);
        Debug.Log("počet bodů : " + početBodů);
        Debug.Log("rozteč bodů : " + roztečBodů);
        Debug.Log("směr lana : " + směrLana);

        for(int i  = 0; i < početBodů;i++)
        {
            Vector3 novýBod = ropeStart.position + ((směrLana*roztečBodů) * i );

            novýBod += new Vector3(0,GetSvis(i,početBodů),0);

            bodyLana.Add(novýBod);
        }

        lineRenderer.positionCount = početBodů;
        lineRenderer.SetPositions(bodyLana.ToArray());

        //oprava poslední pozice
        if(fixEndPoint)lineRenderer.SetPosition(početBodů-1,ropeEnd.position);
    }

    float GetSvis(int i, int početBodů)
    {
        float bodSvisu = (float)i/(float)početBodů;
        return ropeCurve.Evaluate(bodSvisu) * curveWeight;
    }
}
