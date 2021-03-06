using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float speed = 4f;//歩くスピード

    public float jumpPower = 700;//ジャンプ力
    public LayerMask groundLayer;//Linecastで判定するLayer

    public GameObject bullet;

    public Rigidbody2D rigidbody2D;
    private Animator anim;
    private bool isGrounded; //着地判定
	private Renderer renderer;


    public LayerMask GroundLayer
    {
        get
        {
            return groundLayer;
        }

        set
        {
            groundLayer = value;
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();        rigidbody2D = GetComponent<Rigidbody2D>();
		renderer = GetComponent<Renderer> ();
    }

    void Update()
	{
		//Linecastでユニティちゃんの足下に地面があるか判定
		isGrounded = Physics2D.Linecast (
			transform.position + transform.up * 1,
			transform.position - transform.up * 0.05f,
			GroundLayer);
		//スペースキーを押し、
		if (Input.GetKeyDown ("space")) {
			//着地していたとき
			if (isGrounded) {
				//Dashアニメーションを止めて、Jumpアニメーションを実行
				anim.SetBool ("Dash", false);
				anim.SetTrigger ("Jump");
				//着地判定をfalse                                             
				isGrounded = false;
				//AddForceにて上方向へ力を加える
				rigidbody2D.AddForce (Vector2.up * jumpPower);
			} else {
				anim.SetTrigger ("Jump");
				//着地判定をfalse                                             
				isGrounded = false;
				//AddForceにて上方向へ力を加える
				rigidbody2D.AddForce (Vector2.up * jumpPower);
			}

			//上下への移動速度を取得
			float velY = rigidbody2D.velocity.y;
			//移動速度が0.1より大きければ上昇
			bool isJumping = velY > 0.1f ? true : false;
			//移動速度が-0.1より小さければ下降
			bool isFalling = velY > 0.1f ? true : false;
			//結果をアニメータービューの変数へ反映する
			anim.SetBool ("isJumping", isJumping);
			anim.SetBool ("isFalling", isFalling);
		}
		if (Input.GetKeyDown("left shift"))

        {
            anim.SetTrigger("Shot");
            Instantiate(bullet, transform.position + new Vector3(0f, 1.2f, 0f), transform.rotation);
        }


    
	}
    void FixedUpdate()
    {

        //左キー：-1,右キー：1
        float x = Input.GetAxisRaw("Horizontal");

        //左か右入力したら
        if (x != 0)
        {
            //入力方向へ移動
            rigidbody2D.velocity = new Vector2(x * speed, rigidbody2D.velocity.y);
            //localScale.xを-1にすると画像が反転する。
            Vector2 temp = transform.localScale;
            temp.x = x;
            transform.localScale = temp;

            //			//Wait->Dash
            anim.SetBool("Dash", true);

            //左も右も入力していなかった
        }
        else
        {
            //横移動の速度を0にしてピタッと止まれるように
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            //Dash->Wait
            anim.SetBool("Dash", false);
        }
    }
	void OnCollisionEnter2D(Collision2D col)
	{
		//Enemyとぶつかったときにコルーチンを実行
		if (col.gameObject.tag == "Enemy") {
			StartCoroutine ("Damage");
		}
	}

	IEnumerator Damage()
	{
		//レイヤーをPlayerDamageに変更
		gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
			//while文を10回ループ
			int count=10;
		while (count>0){
			//透明にする
			renderer.material.color = new Color (1,1,1,0);
			//0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			//元に戻す
			renderer.material.color = new Color (1,1,1,1);
		    //0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			count--;
		}
		//レイヤーをPlayerに戻す
		gameObject.layer = LayerMask.NameToLayer("Player");
	}
}
