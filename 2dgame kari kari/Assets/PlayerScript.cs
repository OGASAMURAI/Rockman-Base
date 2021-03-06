using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

	public float speed = 4f;//歩くスピード
	public float jumpPower = 5;//ジャンプ力
	public float time = 2;//対空時間みたいなやつ
    public LayerMask groundLayer;//Linecastで判定するLayer

    public GameObject bullet;

    public Rigidbody2D rigidbody2D;
    private Animator anim;
    private bool isGrounded; //着地判定
	private float sp;//空中にいるときに移動制限する用
	private Renderer renderer;

    //********** 開始 **********//
    private bool gameClear = false; //ゲームクリアーしたら操作を無効にする
    public Text clearText; //ゲームクリアー時に表示するテキスト
    //********** 終了 **********//


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
		rigidbody2D.gravityScale = 3;//落下を強化
    }

    void Update()
	{
		//Linecastでユニティちゃんの足下に地面があるか判定
		isGrounded = Physics2D.Linecast (
			transform.position + transform.up * 1,
			transform.position - transform.up * 0.05f,
			GroundLayer);

        //********** 開始 **********//
        //ジャンプさせない
        if (!gameClear)
        {
        //********** 終了 **********//


            //対空時間外なら上に加速しない
            if (time > 0)
			{
				if (Input.GetKey("z"))
				{
					rigidbody2D.gravityScale = 1;
					time -= Time.deltaTime;
					if (isGrounded)
					{
						//Dashアニメーションを止めて、Jumpアニメーションを実行
						anim.SetBool("Dash", false);
						anim.SetTrigger("Jump");
						//着地判定をfalse
						isGrounded = false;
						rigidbody2D.velocity = new Vector2(0, jumpPower);
					}
					else
					{
						rigidbody2D.velocity = new Vector2(0, jumpPower);
					}
				}
				else
				{
					rigidbody2D.gravityScale = 3;
				}
			}
			//地上にいるときにチャージ
			if (isGrounded)
			{
				if (time < 2)
				{
					time += 0.1f;
				}
			}
        //********** 開始 **********//
        }
        //********** 終了 **********//
        //********** 開始 **********//
        //弾を打たせない、ゲームオーバーにさせない
        if (!gameClear)
        {
        //********** 終了 **********//
            if (Input.GetKeyDown("x"))

        {
            anim.SetTrigger("Shot");
            Instantiate(bullet, transform.position + new Vector3(0f, 1.2f, 0f), transform.rotation);
        
}
            //********** 開始 **********//
        }
        //********** 終了 **********//

    }
    void FixedUpdate()
    {
        //********** 開始 **********//
        //左右に移動させない　MainCameraを動かさない
        if (!gameClear)
        {
            //********** 終了 **********//

            //左キー：-1,右キー：1
            float x = Input.GetAxisRaw ("Horizontal");
			//移動制限
			if (isGrounded == false) {
				sp = 0.5f;
			} else {
				sp = 1f;
			}
			//左か右入力したら
			if (x != 0) {
				//入力方向へ移動
				rigidbody2D.velocity = new Vector2 (x * speed * sp, rigidbody2D.velocity.y);
				//localScale.xを-1にすると画像が反転する。
				Vector2 temp = transform.localScale;
				temp.x = x;
				transform.localScale = temp;

				//			//Wait->Dash
				anim.SetBool ("Dash", true);

				//左も右も入力していなかった
			} else {
				//横移動の速度を0にしてピタッと止まれるように
				rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
				//Dash->Wait
				anim.SetBool ("Dash", false);
			}
            //********** 開始 **********//
        }
        else {
            //クリアーテキストを表示
            clearText.enabled = true;
            //アニメーションは走り
            anim.SetBool("Dash", true);
            //右に進み続ける
            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
            //5秒後にタイトル画面へ戻るCallTitleメソッドを呼び出す
            Invoke("CallTitle", 5);
        }
        //********** 終了 **********//

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

    //********** 開始 **********//
    void OnTriggerEnter2D(Collider2D col)
    {
        //タグがClearZoneであるTriggerにぶつかったら
        if (col.tag == "ClearZone")
        {
            //ゲームクリアー
            gameClear = true;
        }
    }

    void CallTitle()
    {
        //タイトル画面へ
        Application.LoadLevel("Title");
    }
    //********** 終了 **********//

}