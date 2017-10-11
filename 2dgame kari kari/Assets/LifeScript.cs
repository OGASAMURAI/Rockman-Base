using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour {

	RectTransform rt;

	public GameObject Player;
	public GameObject explosion;
	public Text gameOverText;
	private bool gameOver = false;

	void Start () {
		rt = GetComponent<RectTransform> ();
		
	}

	void Update()
	{
		//ライフが0以下になったとき
		if(rt.sizeDelta.y<=0){
			//ゲームオーバー判定がfalseなら爆発アニメーションを生成
			//GameOverメソッドでtureになるので一回のみ実行
			if(gameOver == false){
				Instantiate (explosion, Player.transform.position + new Vector3 (0, 1, 0), Player.transform.rotation);
					}
					//ゲームオーバー判定をtureにしプレイヤーを消去
					GameOver ();
					}
				//ゲームオーバー判定がtureのとき
					if(gameOver){
						//ゲームオーバーの文字を表示
						gameOverText.enabled = true;
						//Enterキーを押すと
						if(Input.GetKeyDown("Enter")){
							//タイトルへ戻る
							Application.LoadLevel("Title");
						}
					}
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

	public void GameOver()
	{
		gameOver = true;
		Destroy (Player);
	}
}

