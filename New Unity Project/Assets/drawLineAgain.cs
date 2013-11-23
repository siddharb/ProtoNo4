using UnityEngine;
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

	
	void Start () 
	{
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetPosition(0, destination.position);
		lineRenderer.SetWidth(0.45f, 0.45f);
		dist = Vector3.Distance(origin.position,destination.position);
		points = new Transform[20];
		points[0] = origin;
		points[1] = destination;
		//calculatePoints();
	}
	
	void Update () 
	{
		drawLaser(points[0].position,3);
	}

	Vector3 calculateReflection(Transform a, Transform b)
	{
		//float theta = transform.eulerAngles.y + 90+temp;
		//dir=new Vector3((-1)*reach*Mathf.Cos(theta),0,reach*Mathf.Sin(theta));
		Vector3 temp = Vector3.zero;
		temp.x = a.position.x + 2*(b.position.x - a.position.x);
		Debug.DrawLine(a.position,temp);
		return temp;
	}
	void drawLaser(Vector3 startPoint,int n)  
	{
    Vector3 rayDir = transform.TransformDirection (Vector3.forward);
 		RaycastHit hit;
    for(int i = 0; i < n; i++)
    {     
        if (Physics.Raycast (startPoint, rayDir, out hit, 1000f)) 
        {
           Debug.DrawLine (startPoint, hit.point);
           rayDir = Vector3.Reflect( (hit.point - startPoint).normalized, hit.normal  ) ;
               startPoint = hit.point;
        }
			else
			{
				Debug.DrawLine (startPoint, rayDir*1000f);
			}
    }
}
}
