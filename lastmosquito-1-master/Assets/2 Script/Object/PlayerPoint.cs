using UnityEngine;
using System.Collections;

public class PlayerPoint : MonoBehaviour
{
    public enum ePointState {  eMenu, eStage}
    public ePointState state;

    public bool isCamStateCollider;

    void Awake()
    {
        CameraCtrl.Instance.GetComponent<CameraEffect>().SetParentCamp();
        PlayerCtrl.Instance.SetStateIdle(true);
        PlayerCtrl.Instance.SetTransform(transform.position, transform.rotation);
    }

    void Start ()
    {
        if (isCamStateCollider)
            CameraCtrl.Instance.SetStateToCollider(true);
        else
            CameraCtrl.Instance.SetStateToCollider(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (state == ePointState.eStage)
            {
                PlayerCtrl.Instance.SetHP(75);
                PlayerCtrl.Instance.iBlood = 0;
                PlayerCtrl.Instance.fStamina = 200;
                PlayerCtrl.Instance.SetStateIdle(false);
                gameObject.SetActive(false);    //비활성화 
            }
            else
            {
                GameObject _obj = null;
                if(_obj = CollisionManager.Instance.Get_MouseCollisionObj(3000f, "RAINDROP"))
                {
                    PlayerCtrl.Instance.SetStateIdle(false);
                    gameObject.SetActive(false);
                }
            }
            
        }



    }

}
