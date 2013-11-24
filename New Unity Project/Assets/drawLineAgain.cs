﻿using UnityEngine;
using System.Collections;

public class drawLineAgain : MonoBehaviour {

	public Transform origin;
	public Transform destination;
	private Transform[] points;
	private LineRenderer lineRenderer;
	private float dist;
	public float lineSpeed;
	private float counter;
	float reach = 100;
	float xe = 1;
	float theta3;
	bool drawingComplete;
	int vertextCount = 3;

	
	void Start () 
	{
		lineSpeed = 100;
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetWidth(0.1f, 0.1f);
		lineRenderer.SetPosition(0, origin.position);
		lineRenderer.SetVertexCount(vertextCount+1);
		dist = Vector3.Distance(origin.position,destination.position);
		points = new Transform[20];
		points[0] = origin;
		points[1] = destination;
	}
	
	void Update () 
	{
		drawLaser(points[0].position,3);
	
	}

	void drawLaser(Vector3 startPoint,int n)  
	{
 	   Vector3 rayDir = transform.TransformDirection (Vector3.forward);
 		RaycastHit hit;
    	for(int i = 0; i < n; i++)
    	{     
       	 if (Physics.Raycast (startPoint, rayDir, out hit, 1000f)) 
    	    {
				if (hit.collider.gameObject.tag == "mirror")
					{
						Debug.DrawLine (startPoint, hit.point);
						LineRenderer l2 = GetComponent<LineRenderer>();
						//l2.SetPosition(i,startPoint);
           				rayDir = Vector3.Reflect( (hit.point - startPoint).normalized, hit.normal  ) ;
						//lineRenderer.SetPosition(2, rayDir*1000f);
            	   		startPoint = hit.point;
					lineRenderer.SetPosition(i+1, startPoint);
					}
     	 	 }
				else
				{
					Debug.DrawLine (startPoint, rayDir*1000f);
				lineRenderer.SetPosition(i+1, rayDir*1000f);
					//lineRenderer.SetPosition(i, rayDir*1000f);
				}
    	}
	}

}
