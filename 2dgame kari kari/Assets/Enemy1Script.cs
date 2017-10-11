using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Script : MonoBehaviour {

	Rigidbody2D rigidbody2D;
	public int speed = -3;
	public GameObject exploson;
	public GameObject item;
	public int attackPoint = 10;
	private LifeScript lifeScript;

	//メインカメラのタグ名　constは定数
	private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
	//カメラに映っているの判定
	private bool _isRendered=false;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
		lifeScript = GameObject.FindGameObjectWithTag ("HP").GetComponent<LifeScript> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (_isRendered) {
			rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
		
		}
		if (gameObject.transform.position.y < Camera.main.transform.position.y - 8 || gameObject.transform.position.x < Camera.main.transform.position.x - 10) {
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter2D (Collider2D col)
	{
			if(_isRendered){
		if (col.tag == "Bullet") {
			Destroy (gameObject);
			Instantiate (exploson, transform.position, transform.rotation);

			//四分の一の確率で回復アイテムを落とす
			if (Random.Range (0, 4) == 0) {
				Instantiate (item, transform.position, transform.rotation);
			}
		}
	}
		}
	void OnCollisionEnter2D(Collision2D col)
	{
		//Playerとぶつかった時
		if (col.gameObject.tag == "Player") {
			//LifeScriptのlifeDownメソッドを実行
			lifeScript.LifeDown (attackPoint);

		}
	}
	//Rendererがカメラに映っている間だけ呼ばれる
	void OnWillRenderObject()
	{
		//メインカメラに映ったときだけ_isRenderedをtrue
		if (Camera.current.tag == MAIN_CAMERA_TAG_NAME) {
			_isRendered = true;
		}
	}
}

