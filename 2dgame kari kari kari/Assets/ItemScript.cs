using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

	public int healPoint = 20;
	//Prefab化するとinspectorから指定できないためprivate化
	private LifeScript lifeScript;

	void State()
	{
		//HPタグのついているオブジェクトのlifeScriptを取得

	//	Debug.Log ("こんにちは : "+lifeScript);
	}

	void OnCollisionEnter2D (Collision2D col) {
		//Debug.Log(col.gameObject.tag) ;
		lifeScript = GameObject.FindGameObjectWithTag ("HP").GetComponent<LifeScript> ();
		//ユニティちゃんと衝突した時
		if(col.gameObject.tag =="Player"){
			//LifeUpmeメソッドを呼び出す　引数はhealPoint

			lifeScript.LifeUp(healPoint);
			//アイテム削除する
		Destroy(gameObject);
		}
	}
}