using UnityEngine;
using System.Collections;

public class EnemyAnimaion : MonoBehaviour
{
    public Animator anim;
    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
    
    }
    /*
	void Start () {
        StartCoroutine("PlayerAnim"); // ?? 어떤게 낫지..
	}*/
    // Update is called once per frame
    void Update()
    {
        

        switch (EnemyAI.Instance.state)
        {
            case EnemyAI.State.PATROL:
                {
                    anim.Play("walk");
                    //  anim.speed = PlayerCtrl.Instance.fSpeed * 0.2f;
                    // print("Player speed : " + PlayerCtrl.Instance.fSpeed);
                    // print("anim speed : " + anim.speed);
                    break;
                }

            case EnemyAI.State.ATTACK:
                {
                    anim.Play("swing");
                    //  anim.speed = PlayerCtrl.Instance.fSpeed * 0.2f;
                    // print("Player speed : " + PlayerCtrl.Instance.fSpeed);
                    // print("anim speed : " + anim.speed);
                    break;
                }

            case EnemyAI.State.CHASE:
                {
                    anim.Play("find");
                    break;
                }
            case EnemyAI.State.FOOT:
                {
                    anim.Play("foot");
                    break;
                }


        }
    }
}
