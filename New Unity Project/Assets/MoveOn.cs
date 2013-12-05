using UnityEngine;
using System.Collections;

public class MoveOn : MonoBehaviour {
	public Transform rbutton;
	void Update () {
		this.transform.position = Vector3.Normalize(rbutton.position+(new Vector3(0,10,0)));
	}
}
