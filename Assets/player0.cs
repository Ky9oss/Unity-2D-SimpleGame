using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player0 : MonoBehaviour
{
    
    private Rigidbody2D player;
    private float speed;
    private float jumpspeed;
    private LayerMask ground;
    private LayerMask enemy;
    private LayerMask eats;
    private Collider2D foot;
    private Collider2D body;
    private Animator anim;
    private int xueliang;
    private int score;
    private int boxcount=0;
    public Image image1;
    public Image image2;
    public Image image3;
    public Sprite hurtheart;
    public Sprite goodheart;
    public Text scoreNumber;
    private bool ishurt;
    public AudioSource bgm;
    public AudioSource jumpaudio;
    public AudioSource hurtaudio;
    public AudioSource eataudio;


    // Start is called before the first frame update
    // 第一帧开始前的动作
    void Start()
    {
        speed = 8;
        jumpspeed = 15;
        score = 0;
        xueliang = 3;
        ishurt = false;
        player = GetComponent<Rigidbody2D>();
        foot = GetComponent<EdgeCollider2D>();
        body = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        ground = LayerMask.GetMask("Ground");
        enemy = LayerMask.GetMask("EnemyM");
        eats = LayerMask.GetMask("eats");
    }

    // Update is called once per frame
    //每一帧都会执行的动作
    void Update()
    {
        if(!ishurt)
        {
                Movement();
        }
        SwitchAnim();
        Checkxueliang();

    }

    //玩家移动
    void Movement()
    {
        float horizontal;
        float direction;
        bool jumpornot;

        //收到玩家的指令，左为-1，右为1，不动为0
        horizontal = Input.GetAxis("Horizontal");
        direction = Input.GetAxisRaw("Horizontal");
        jumpornot = Input.GetButtonDown("Jump");

        //玩家移动
        if (horizontal != 0)
        {
            player.velocity = new Vector2(horizontal * speed, player.velocity.y );
            anim.SetFloat("running", Mathf.Abs(direction));
        }
        //玩家朝向
        if(direction != 0)
        {
            transform.localScale = new Vector3(direction, 1, 1);
        }
        //玩家跳跃
        if (jumpornot)
        {
                if (foot.IsTouchingLayers(ground))
                {
                        player.velocity = new Vector2(player.velocity.x, jumpspeed);
                        anim.SetBool("jumping", true);
                        jumpaudio.Play();
                }
        }

    }

    //改变动画
    void SwitchAnim()
    {
        if (player.velocity.y < 0)
        {
                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);
        }

        if(anim.GetBool("falling"))
        {
                if (foot.IsTouchingLayers(ground))
                {
                        anim.SetBool("idling", true);
                        anim.SetBool("falling", false);
                }
        }


        if(ishurt)
        {
                if(Mathf.Abs(player.velocity.x) < 0.5f)
                {
                        ishurt = false;
                        anim.SetBool("hurting", false);
                }
        }

    }

    //吃物品
    private void OnTriggerEnter2D(Collider2D eat)
    {
            if (eat.tag == "Eat")
            {
                if(body.IsTouchingLayers(eats))
                {
                    Destroy(eat.gameObject);
                    eataudio.Play();
                    if (xueliang < 3)
                    {
                        xueliang += 1;
                    }
                }
            }

            if(eat.tag == "Diamond")
            {
                if(body.IsTouchingLayers(eats))
                {
                    Destroy(eat.gameObject);
                    score += 100;
                    eataudio.Play();
                    scoreNumber.text = score.ToString();
                }
            }
    }

    //检查血量
    private void Checkxueliang()
    {
            if(xueliang == 3)
            {
                    image1.sprite = goodheart;
                    image2.sprite = goodheart;
                    image3.sprite = goodheart;
            }
            else if(xueliang == 2)
            {
                    image3.sprite = hurtheart;
                    image2.sprite = goodheart;
                    image1.sprite = goodheart;
            }
            else if(xueliang == 1)
            {
                    image3.sprite = hurtheart;
                    image2.sprite = hurtheart;
                    image1.sprite = goodheart;
            }
            else if(xueliang == 0)
            {
                    image3.sprite = hurtheart;
                    image2.sprite = hurtheart;
                    image1.sprite = hurtheart;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }

    }




    //敌人--受伤与踩头
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //普通pig
        if(collision.gameObject.tag == "Enemy")
        {
                if(body.IsTouchingLayers(enemy))
                {

                        if(xueliang > 0)
                        {
                                xueliang = xueliang-1;
                        }
                }
                pig0 pig = collision.gameObject.GetComponent<pig0>();
                //boxpig0 boxpig = collision.gameObject.GetComponent<boxpig0>();
                if(anim.GetBool("falling") && foot.IsTouchingLayers(enemy))
                {
                        pig.pigdestroy();
                        player.velocity = new Vector2(player.velocity.x, jumpspeed);
                        score += 20;
                        scoreNumber.text = score.ToString();
                }else if (transform.position.x < collision.gameObject.transform.position.x)
                {
                        player.velocity = new Vector2(-10, player.velocity.y);
                        ishurt = true;
                        anim.SetBool("hurting", true);
                        hurtaudio.Play();
                }else if (transform.position.x > collision.gameObject.transform.position.x)
                {
                        player.velocity = new Vector2(10, player.velocity.y);
                        ishurt = true;
                        hurtaudio.Play();
                        anim.SetBool("hurting", true);
                }
        }

        //箱子pig
        if(collision.gameObject.tag == "Enemy1")
        {
                boxpig1 boxpig = collision.gameObject.GetComponent<boxpig1>();
                if(anim.GetBool("falling") && foot.IsTouchingLayers(enemy))
                {
                        if(boxcount==2)
                        {
                            boxpig.boxpigdestroy();
                            boxcount=0;
                            score += 50;
                        }else
                        {
                            boxcount += 1;
                            boxpig.boxdieaudio.Play();
                        }
                        player.velocity = new Vector2(player.velocity.x, jumpspeed);
                        scoreNumber.text = score.ToString();
                }else if (transform.position.x < collision.gameObject.transform.position.x)
                {
                        player.velocity = new Vector2(-20, player.velocity.y);
                        ishurt = true;

                if(body.IsTouchingLayers(enemy))
                {
                        if(xueliang > 1)
                        {
                                xueliang = xueliang-2;
                        }else
                        {
                            xueliang = 0;
                        }
                
                        anim.SetBool("hurting", true);
                        hurtaudio.Play();
                }
                }else if (transform.position.x > collision.gameObject.transform.position.x)
                {
                        player.velocity = new Vector2(20, player.velocity.y);
                        ishurt = true;
                        if(xueliang > 1)
                        {
                                xueliang =xueliang-2;
                        }
                        else
                        {
                            xueliang = 0;
                        }
                        anim.SetBool("hurting", true);
                }
        }
    }

}

