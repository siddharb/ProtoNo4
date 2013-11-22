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
	
	void Start () 
	{
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetPosition(0, origin.position);
		lineRenderer.SetWidth(0.45f, 0.45f);
		dist = Vector3.Distance(origin.position,destination.position);
		points = new Transform[20];
		points[0] = origin;
		points[1] = destination;
		//calculatePoints();
	}
	
	void Update () 
	{
		float theta3 = Mathf.Rad2Deg*Mathf.Atan((Mathf.Abs(points[0].position.x) - Mathf.Abs(points[1].position.x))/(Mathf.Abs(points[0].position.y) - Mathf.Abs(points[1].position.y)));
		//print("theta1 "+points[1].eulerAngles.y);
		float theta2 = (90 - (points[1].eulerAngles.y + theta3));
		//print ("theta3 "+theta3);
		//print ("theta2 "+theta2);
		float xe = points[0].position.x + 2*(points[1].position.x - points[0].position.x) ;

		//print (theta2);
		theta2 = -1*theta2*3.14f/180f;
		float y = (Mathf.Tan(theta2))*xe+ (points[0].position.y - points[1].position.y);
		print (Mathf.Tan(theta2));

		
		Vector3 reflection = new Vector3(xe,y,(points[0].position.y - points[1].position.y));
		Vector3 reflectionx = new Vector3(xe,y,(points[0].position.y - points[1].position.y));
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
