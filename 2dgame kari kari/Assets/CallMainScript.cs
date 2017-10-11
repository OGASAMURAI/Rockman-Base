using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMainScript : MonoBehaviour {


	// Update is called once per frame
	IEnumerator Start () {
		yield return new WaitForSeconds (2);
		Application.LoadLevel ("test");
	}		
}
