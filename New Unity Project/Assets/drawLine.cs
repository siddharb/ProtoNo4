using UnityEngine;
using System.Collections;

public class drawLine : MonoBehaviour {

	public Transform origin;
	public Transform destination;
	private Transform[] points;
	private LineRenderer lineRenderer;
	private float dist;
	public float lineSpeed;
	private float counter;
	RaycastHit hit;
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
		lineRenderer.SetPosition(0, points[1].position);
		float slope = (points[0].position.z - points[1].position.z)/(points[0].position.x - points[1].position.x); // find slope
		
		theta3 = 90-(Mathf.Rad2Deg*Mathf.Atan(slope) - points[1].eulerAngles.y); //find angle between line and reflection
		//print ("theta3 "+theta3);
		
		print ("t3 "+theta3 + " y "+points[1].eulerAngles.y);

		theta3 = theta3 % 360; // so that angle always lies between 0-360

		if (theta3 < 0)
		{
	 	   theta3 += 360;
		}
		theta3 = theta3*3.14f/180f;  //converts angle to radians
		float roty = points[1].eulerAngles.y;
		roty = roty*3.14f/180f;      //roa
		slope = Mathf.Tan(theta3);
		print ("Slope "+slope);
		float theta2 = (90 - (points[1].eulerAngles.y + theta3));
		print ("t2 "+theta2);
		//float xe = points[0].position.x + 2*(points[1].position.x - points[0].position.x) ;
		if(xe == 300)
			xe = points[1].position.x;
		//print (slope);
		theta2 = -1*theta2*3.14f/180f;
		xe = 100;
		float z = -1*slope*xe + points[0].position.z;
		if(points[0].position.x>points[1].position.x || slope<0)
		{
			xe = -1*xe;
		}
		//float y = (Mathf.Tan(theta2))*xe+ (points[0].position.y - points[1].position.y);
		//print (Mathf.Tan(theta2));

		
		Vector3 reflection = new Vector3(xe,0,z);
				lineRenderer.SetPosition(1, (points[1].position - points[0].position)+reflection);
//		Vector3 reflectionx = new Vector3(xe,y,(points[0].position.y - points[1].position.y));
		Debug.DrawLine(origin.position,(calculateReflection(points[0],points[1])));
		Debug.DrawLine(destination.position,reflection,Color.red);
		Debug.DrawRay(origin.position,destination.position - origin.position,Color.white);
		if(counter < dist)
		{
			counter += 1/lineSpeed;
			float x = Mathf.Lerp(0,dist,counter);
			Vector3 pointAlongLine = origin.position + x*Vector3.Normalize(destination.position - origin.position);
			lineRenderer.SetPosition(1, pointAlongLine);
		}
	}
	void calculatePoints()
	{
		int i = 0;
		while(points[i+1].position!=Vector3.zero)
		{
			print (i);
			if (Physics.Raycast(points[i].position, points[i+1].position - origin.position, out hit, 1000))
			{
				if (hit.collider.gameObject.tag == "mirror")
				{
				print ("Inside me"+i);
					print((calculateReflection(points[i],points[i+1]).x)+(calculateReflection(points[i],points[i+1]).y));
					points[i+1] = points[i];
					i++;
				}
			}
		}
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
}
