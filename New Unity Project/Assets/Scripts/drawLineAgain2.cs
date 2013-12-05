using UnityEngine;
using System.Collections;

public class drawLineAgain2 : MonoBehaviour {

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
	Vector3 startPoint;
	int n = 3;
	int i = 0;

	
	void Start () 
	{
		lineSpeed = 100;
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetPosition(0, destination.position);
		lineRenderer.SetWidth(0.45f, 0.45f);
		dist = Vector3.Distance(origin.position,destination.position);
		points = new Transform[20];
		points[0] = origin;
		startPoint = origin.position;
		points[1] = destination;
	}
	
	void Update () 
	{
	//	drawLaser(points[0].position,3);
		Vector3 rayDir = transform.TransformDirection (Vector3.forward);
 		RaycastHit hit;
    	if(i<n)
    	{     
       	 if (Physics.Raycast (startPoint, rayDir, out hit, 1000f)) 
    	    {
				if (hit.collider.gameObject.tag == "mirror")
					{
						Debug.DrawLine (startPoint, hit.point);
           				rayDir = Vector3.Reflect( (hit.point - startPoint).normalized, hit.normal  ) ;
            	   		startPoint = hit.point;
					}
     	 	 }
				else
				{
					Debug.DrawLine (startPoint, rayDir*1000f);
				}
			i++;
    	}
		else
		{
			i = 0;
		}
	
	}
	void drawLine(Vector3 origin, Vector3 destination)
	{
		if(counter < dist)
		{
			counter += 1/lineSpeed;
			float x = Mathf.Lerp(0,dist,counter);
			Vector3 pointAlongLine = origin + x*Vector3.Normalize(destination - origin);
			lineRenderer.SetPosition(1, pointAlongLine);
		}
		else
		{
			drawingComplete = true;
		}
	}

}
