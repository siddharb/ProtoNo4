using UnityEngine;
using System.Collections;

public class RotateButton : MonoBehaviour {
	
	public bool isDown;
	public GameObject mirror;
	public Vector3 offset;
//	RotateOnMousecs romcs;
	void Start()
	{
		offset = new Vector3(1,-10,1);
		isDown = false;
	}
	
	void Update()
	{
		this.transform.position = mirror.transform.position+offset;
	}
	
	void OnMouseDown () 
	{
		isDown = true;
	}
 
	void OnMouseUp () 
	{
		isDown = false;
	}
	public bool getStatus()
	{
		return isDown;
	}
}
