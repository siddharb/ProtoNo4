using UnityEngine;
using System.Collections;

public class drawLineAgain : MonoBehaviour {

	public Transform origin;
	private LineRenderer lineRenderer;
	private LineRenderer lineRendererG;
	public int vertextCount = 3;
	Color c1 = Color.red;
	GameObject G;
	LineRenderer lineRenderer2;

	
	void Start () 
	{
		G = new GameObject();
		lineRenderer = GetComponent<LineRenderer>();
		lineRendererG = GetComponent<LineRenderer>();
		lineRenderer2 = G.AddComponent<LineRenderer>();
		lineRenderer.SetWidth(0.1f, 0.1f);
		lineRenderer.SetPosition(0, origin.position);
		lineRenderer.SetVertexCount(vertextCount+1);
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer2.SetWidth(0.1f, 0.1f);
		lineRenderer2.SetPosition(0, origin.position);
		lineRenderer2.SetVertexCount(2);
		lineRenderer2.material = new Material(Shader.Find("Particles/Additive"));
	}
	
	void Update () 
	{
		drawLaser(origin.position,vertextCount);
	
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

           				rayDir = Vector3.Reflect( (hit.point - startPoint).normalized, hit.normal  ) ;
            	   		startPoint = hit.point;
						lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
						lineRenderer.SetColors(Color.yellow, Color.yellow);
						lineRenderer.SetPosition(i+1, startPoint);
					}
				if (hit.collider.gameObject.name == "Glass_red")
				{
					Debug.DrawLine (startPoint, hit.point);
					LineRenderer l2 = GetComponent<LineRenderer>();
					rayDir = hit.point*100f ;
					//lineRenderer.SetPosition(2, rayDir*1000f);
					lineRenderer.SetPosition(i+1, hit.point);
					lineRenderer2.SetColors(Color.white, Color.white);
					lineRenderer2.SetPosition(0, hit.point);
					lineRenderer2.SetPosition(1, rayDir);
					startPoint = hit.point;
				}
     	 	 }
				else
				{
				//lineRenderer2.SetPosition(0, -1*hit.point);
				//lineRenderer2.SetPosition(1, -1*rayDir);
					Debug.DrawLine (startPoint, rayDir*1000f);
				lineRenderer.SetPosition(i+1, rayDir*1000f);
					//lineRenderer.SetPosition(i, rayDir*1000f);
				}
    	}
	}

}
