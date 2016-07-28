using UnityEngine;
using System.Collections;

public class heckC : MonoBehaviour
{

    void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag == "PLAYER" && (EnemyAI.State.ATTACK == EnemyAI.Instance.state))
        {
            PlayerCtrl.Instance.Damaged(30);
        }


        else if (coll.gameObject.tag == "PLAYER" && (EnemyAI.State.FOOT == EnemyAI.Instance.state))
        {
            PlayerCtrl.Instance.Damaged(30);
        }
    }
}