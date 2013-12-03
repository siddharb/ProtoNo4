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
		lineRenderer.SetColors(Color.yellow, Color.yellow);
		lineRenderer2.SetWidth(0.1f, 0.1f);
		lineRenderer2.SetPosition(0, origin.position);
		lineRenderer2.SetVertexCount(vertextCount+1);
		lineRenderer2.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer2.SetColors(Color.white, Color.white);
	}
	
	void Update () 
	{
		drawLaser(origin.position,vertextCount);
	
	}

	void drawLaser(Vector3 startPoint,int n)  
	{
 	   Vector3 rayDir = transform.TransformDirection (Vector3.forward);
 		RaycastHit hit;
		int mirrorCount = 1;
		bool mirror1 = false;
    	for(int i = 0; i < n; i++)
    	{     
       	 if (Physics.Raycast (startPoint, rayDir, out hit, 1000f)) 
    	    {
				if (hit.collider.gameObject.tag == "mirror")
					{

						Debug.DrawLine (startPoint, hit.point);

           				rayDir = Vector3.Reflect( (hit.point - startPoint).normalized, hit.normal  ) ;
            	   		startPoint = hit.point;
						if(mirror1 == false)
						{
						lineRenderer2.SetVertexCount(0);
						lineRenderer.SetPosition(i+1, startPoint);
						}
						else
						{
						lineRenderer2.SetVertexCount(++mirrorCount);
						lineRenderer2.SetPosition(mirrorCount, startPoint);
						//mirrorCount++;
						}
					}
				if (hit.collider.gameObject.name == "Glass_red")
				{
					lineRenderer2.SetVertexCount(vertextCount+1);
					lineRenderer.SetVertexCount(3);
					Debug.DrawLine (startPoint, hit.point);
					rayDir = (hit.point - startPoint)*100f ;
					lineRenderer.SetPosition(i+1, hit.point);
					if(mirror1 == false)
					{
					lineRenderer2.SetPosition(0, hit.point);
					}
					lineRenderer2.SetPosition(mirrorCount, rayDir);
					mirrorCount++;
					lineRenderer2.SetVertexCount(mirrorCount);
					startPoint = hit.point;
					mirror1 = true;
				}
				if (hit.collider.gameObject.name == "Cube")
				{
					Debug.DrawLine (startPoint, hit.point);

					if(mirror1 == false)
					{
						lineRenderer2.SetVertexCount(0);
						lineRenderer.SetPosition(i+1, hit.point);
					}
					//else
					//{
					//	lineRenderer2.SetPosition(mirrorCount, startPoint);
				//		mirrorCount++;
				}
				if (hit.collider.gameObject.name == "Sphere")
				{
					if(mirror1 == false)
					{
						lineRenderer2.SetVertexCount(0);
						lineRenderer.SetPosition(i+1, hit.point);
					}
					Application.Quit();
					//else
					//{
					//	lineRenderer2.SetPosition(mirrorCount, startPoint);
					//		mirrorCount++;
				}
     	 	 }
				else
				{
				if(mirror1 == false)
				{
				Debug.DrawLine (startPoint, rayDir*1000f);
				lineRenderer.SetPosition(i+1, rayDir*1000f);
				}
				}
    	}
	}

}
