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
		lineRenderer.SetColors(Color.white, Color.white);
		lineRenderer2.SetWidth(0.1f, 0.1f);
		lineRenderer2.SetPosition(0, origin.position);
		lineRenderer2.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer2.SetColors(Color.red, Color.red);
	}
	
	void Update () 
	{
		drawLaser(origin.position,vertextCount);
	
	}

	void drawLaser(Vector3 startPoint,int n)  
	{
 	   Vector3 rayDir = transform.TransformDirection (Vector3.left);
 		RaycastHit hit;
		int mirrorCount = 0;
		int glassCount = 0;
		int run = 0;
		int run2 = 0;
		bool mirror1 = false;
		bool mirror2 = false;
		lineRenderer2.SetVertexCount(0);
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
							print ("mirror1_1");
						run++;
						lineRenderer.SetVertexCount(glassCount+2);
						lineRenderer.SetPosition(glassCount+1, hit.point);
						glassCount++;
						}
						else
						{
						print ("mirror1_2");
						run2++;
						lineRenderer2.SetVertexCount(mirrorCount+1);
						lineRenderer2.SetPosition(mirrorCount, hit.point);
						mirrorCount++;
						//mirrorCount++;
						}
					}
				if (hit.collider.gameObject.tag == "Glass_red")
				{
					if(mirror1 == false)
					{
						print ("GlassFirst");
					lineRenderer2.SetVertexCount(vertextCount+1);
					lineRenderer.SetVertexCount(run+1);
					Debug.DrawLine (startPoint, hit.point);
					rayDir = Vector3.Normalize((hit.point - startPoint)*1000f );
					lineRenderer.SetVertexCount(glassCount+2);
					lineRenderer.SetPosition(glassCount+1, hit.point);
					glassCount++;
					lineRenderer2.SetVertexCount(mirrorCount+1);
					lineRenderer2.SetPosition(mirrorCount, hit.point);
					mirrorCount++;
					startPoint = hit.point;
					mirror1 = true;
					}
					else
					{
						print ("Glass");
						lineRenderer2.SetVertexCount(vertextCount+1);
						Debug.DrawLine (startPoint, hit.point);
						rayDir = Vector3.Normalize((hit.point - startPoint)*1000f );
						glassCount++;
						lineRenderer2.SetVertexCount(mirrorCount+1);
						lineRenderer2.SetPosition(mirrorCount, hit.point);
						mirrorCount++;
						startPoint = hit.point;
						//mirror2 = true;
					}
				}
				if (hit.collider.gameObject.tag == "Obstacle")
				{
					Debug.DrawLine (startPoint, hit.point);

					if(mirror1 == false)
					{
						print ("Whaaat!!");
						//lineRenderer2.SetVertexCount(0);
						lineRenderer.SetVertexCount(glassCount+2);
						lineRenderer.SetPosition(glassCount+1, hit.point);
						glassCount++;
					}
					else
					{
						lineRenderer2.SetVertexCount(mirrorCount+1);
						lineRenderer2.SetPosition(mirrorCount, hit.point);
						mirrorCount++;
					}
				}
				if (hit.collider.gameObject.name == "Sphere")
				{
					if(mirror1 == false)
					{
						lineRenderer.SetVertexCount(glassCount+2);
						lineRenderer.SetPosition(glassCount+1, hit.point);
						glassCount++;
					}
					else
					{
						lineRenderer2.SetVertexCount(mirrorCount+1);
						lineRenderer2.SetPosition(mirrorCount, hit.point);
						mirrorCount++;
					}
					Application.Quit();
				}
     	 	 }
				else
				{
				if(mirror1 == false)
				{
				Debug.DrawLine (startPoint, rayDir*1000f);
					lineRenderer.SetVertexCount(glassCount+2);
					lineRenderer.SetPosition(glassCount+1, rayDir*1000f);
					glassCount++;
				}
				else
				{
					lineRenderer2.SetVertexCount(mirrorCount+1);
					lineRenderer2.SetPosition(mirrorCount, rayDir*1000f);
					mirrorCount++;
				}
				}
    	}
	}

}
