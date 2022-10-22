//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//
//protected class Enemy : MonoBehaviour
//{
//    protected Rigidbody2D pig;
//    protected Transform lefttransform, righttransform;
//    protected float speed;
//    protected bool Faceleft;
//    protected Animator anim;
//    protected float time = 0f;
//    protected bool alive = true;
//    // Start is called before the first frame update
//    protected virtual void Start()
//    {
//        pig = GetComponent<Rigidbody2D>();
//        speed = 2.3f;
//        Faceleft = true;
//        anim = GetComponent<Animator>();
//        transform.DetachChildren();//不让子类继承，不然左右两点会跟随pig一起动
//    }
//
//    // Update is called once per frame
//    protected virtual void Update()
//    {
//        if(alive)
//        {
//            Movement();
//        }
//        anim.SetFloat("running", Mathf.Abs(pig.velocity.x));
//        
//    }
//
//    protected void Movement()
//    {
//        
//        if(Faceleft)
//        {
//            pig.velocity = new Vector2(-speed, pig.velocity.y);
//            if(transform.position.x < lefttransform.position.x)
//            {
//                pig.velocity = new Vector2(0, pig.velocity.y);
//                time += Time.deltaTime;
//                if(time > 1.5)
//                {
//                    transform.localScale = new Vector3(-1,1,1);
//                    Faceleft = false;
//                    time = 0f;
//                }
//            }
//        }
//        else
//        {
//            pig.velocity = new Vector2(speed, pig.velocity.y);
//            if(transform.position.x > righttransform.position.x)
//            {
//                pig.velocity = new Vector2(0, pig.velocity.y);
//                time += Time.deltaTime;
//                if(time > 1.5)
//                {
//                    transform.localScale = new Vector3(1,1,1);
//                    Faceleft = true;
//                    time = 0f;
//                }
//            }
//        }
//        
//    }
//
//    //动画die
//    public void pigdestroy()
//    {
//        pig.velocity = new Vector2(0, pig.velocity.y);
//        alive = false;
//        anim.SetTrigger("dying");
//    }
//
//    //真实die
//    public void pigdie()
//    {
//        Destroy(gameObject);
//    }
//
//}
//
