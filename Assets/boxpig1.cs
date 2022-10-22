using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class boxpig1 : MonoBehaviour
{
    private Rigidbody2D pig;
    public Transform lefttransform, righttransform;
    private float speed;
    private bool Faceleft;
    private Animator anim;
    private float time = 0f;
    private bool alive = true;
    public AudioSource boxdieaudio;
    // Start is called before the first frame update
    protected void Start()
    {
        // base.Start();
        pig = GetComponent<Rigidbody2D>();
        speed = 0.8f;
        Faceleft = true;
        anim = GetComponent<Animator>();
        transform.DetachChildren();//不让子类继承，不然左右两点会跟随pig一起动
    }

    // Update is called once per frame
    void Update()
    {
        if(alive)
        {
            boxMovement();
        }
        anim.SetFloat("running", Mathf.Abs(pig.velocity.x));
        
    }

    void boxMovement()
    {
        
        if(Faceleft)
        {
            pig.velocity = new Vector2(-speed, pig.velocity.y);
            if(transform.position.x < lefttransform.position.x)
            {
                pig.velocity = new Vector2(0, pig.velocity.y);
                time += Time.deltaTime;
                if(time > 1.5)
                {
                    transform.localScale = new Vector3(-1,1,1);
                    Faceleft = false;
                    time = 0f;
                }
            }
        }
        else
        {
            pig.velocity = new Vector2(speed, pig.velocity.y);
            if(transform.position.x > righttransform.position.x)
            {
                pig.velocity = new Vector2(0, pig.velocity.y);
                time += Time.deltaTime;
                if(time > 1.5)
                {
                    transform.localScale = new Vector3(1,1,1);
                    Faceleft = true;
                    time = 0f;
                }
            }
        }
        
    }

    //动画die
    public void boxpigdestroy()
    {
        boxdieaudio.Play();
        pig.velocity = new Vector2(0, pig.velocity.y);
        alive = false;
        anim.SetTrigger("dying");
    }

    //真实die
    void boxpigdie()
    {
        Destroy(gameObject);
    }

}


