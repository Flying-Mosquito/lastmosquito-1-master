using UnityEngine;
using System.Collections;

public class Smoke : MonoBehaviour {


    void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag == "PLAYER")
        {
            PlayerCtrl.Instance.Damaged(30);


        }
    }
}
