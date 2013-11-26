using UnityEngine;
using System.Collections;
[RequireComponent(typeof(RotateButton))]
public class RotateOnMouseCS : MonoBehaviour {
	
	float sensitivityZ = 15;
	Transform mytransform;
	Transform  referenceCamera;
	GameObject rbutton;
	RotateButton rb;

	void Start () 
	{
		rbutton = GameObject.Find("RButton");
		rb = rbutton.GetComponent<RotateButton>();
		mytransform = this.transform;
		referenceCamera = Camera.main.transform;
	}
	
	void Update () 
	{
		float rotationZ = Input.GetAxis("Mouse Y") * sensitivityZ;
		if(rb)
		{
//			print ("WoWoWEEWaa");
			if(rb.getStatus() == true)
			{
				mytransform.RotateAround( referenceCamera.forward , Mathf.Deg2Rad * rotationZ);
			}
		}
	}
}
