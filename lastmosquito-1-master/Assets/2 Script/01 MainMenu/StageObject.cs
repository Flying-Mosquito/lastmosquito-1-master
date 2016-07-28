using UnityEngine;
using System.Collections;

// 스테이지에 사용될 - 클릭될 물체에 사용
public class StageObject : MonoBehaviour {
    private  Component col;
    public int stageNum;
    public bool bCheck; // OnCollision에서 코루틴을 사용하면 플레이어가 다음 씬에서 사라지는 현상이 발생하여, 플레이어가 충돌했는지 체크 후 Update쪽에서 코루틴을 돌려주기 위한 변수

    void Awake()
    {
        col = GetComponent<Collider>();
        bCheck = false;
    
    }

    void Update()
    {
       if (Input.GetMouseButtonDown(0))
        {
             bool bMouseCollision = CollisionManager.Instance.Check_MouseCollision(col);

            //선택됐다면 씬 변경, ID가 있어서 씬 선택을 하게 될거야
            if (bMouseCollision)
            {
                // print("셋아이들 펄스");
                //PlayerCtrl.Instance.SetStateIdle(false);
                // print("씬이 바뀔때야");
                PlayerCtrl.Instance.SetParentNull();
           // gameObject.SetActive(false);
            GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stageNum.ToString());
            
                // GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stageNum.ToString())
            }

        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.CompareTag("PLAYER"))
        {
            // if (Input.GetMouseButtonDown(0))
            // {
            // bool bCheck = CollisionManager.Instance.Check_MouseCollision(col);
            print("비체크는 트루");
            // 선택됐다면 씬 변경, ID가 있어서 씬 선택을 하게 될거야
            bCheck = true;
            // if (bCheck)
//print("씬이 바뀔때야 , tage: " + transform.gameObject.tag);
                 //   GameManager.Instance.StartCoroutine("StartLoad", "Stage" + stageNum.ToString());
                
            //}
        }
    }
}
