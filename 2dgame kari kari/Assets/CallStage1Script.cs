using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallStage1Script : MonoBehaviour {

void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel ("Stage1");
		}
	}
}
