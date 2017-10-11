using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour {

	RectTransform rt;


	void Start () {
		rt = GetComponent<RectTransform> ();
		
	}

	public void LifeDown(int ap){

		rt.sizeDelta -= new Vector2 (0, ap);
	}
	public void LifeUp(int hp)
	{
		//RectTransformのサイズを習得し、プラスする
		rt.sizeDelta += new Vector2 (0, hp);
		//最大値を超えたら、最大値で上書きする
		if (rt.sizeDelta.y > 240f) {
			rt.sizeDelta = new Vector2 (51f, 240f);
		}
	}
}

