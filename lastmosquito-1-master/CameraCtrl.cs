using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CameraCtrl : Singleton<CameraCtrl>
{
    [System.Serializable]
    public enum eMoveState { TRIGGER, COLLIDER }
    // LateUpdate쪽으로 바꿔줘야 하는데 .. 무엇을..?
    private Transform targetTr;
    private Transform rayTarget;
    public bool isLookFar;
    Vector3 vLerp;

    private List<GameObject> preRayHitObjList = new List<GameObject>();      //이전에 충돌한 gameObject를 가지고 있는 List
                                                                             //private GameObject Camera;


    public eMoveState moveState = eMoveState.COLLIDER;

    public float fTargetDist;
    public float fNormalDist;
    public float fFarDist;
    public float fTargetHeight;
    private float fDampTrace;

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        //  Camera = 
        targetTr = GameObject.Find("CamPivot").transform;
        transform.LookAt(targetTr);
        fTargetDist = 0.8f;//1.5f;
        fNormalDist = 0.8f;//1.5f;
        fFarDist = 18f;
        fTargetHeight = 0.2f;
        fDampTrace = 5f;
        isLookFar = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void LateUpdate()
    {
        //if (targetTr != null)           // 여기도 수정해줘야하지.. 카메라 상태에 따라서 멀리 있는 것 투명하게 만들 것인지, 아니면 카메라가 물체를 못 뚫게 할 것인지 정해줘야 한다.
        //{
        //prePos = transform.position;
        if (PlayerCtrl.Instance.state != Constants.ST_CLING)        // 붙어있지 않을 때의 카메라 효과(위치)
        {
            isLookFar = false;
            if (fTargetDist != fNormalDist)
                fTargetDist = Mathf.Lerp(fTargetDist, fNormalDist, 0.3f);
            
        }
       
        transform.position = targetTr.position + (-targetTr.forward * fTargetDist) + Vector3.up * fTargetHeight;

        transform.LookAt(targetTr.position);
        // transform.Rotate(-20, 0, 0);
        // transform.rotation = targetTr.rotation;


        rayTarget = PlayerCtrl.Instance.transform;    // 수정필요, rayTarget을 잡아주는 함수 
        if (moveState == eMoveState.TRIGGER)
        {
            if (PlayerCtrl.Instance.state == Constants.ST_CLING)
            {
                if (isLookFar != true)
                {
                     Transform parentTr  = PlayerCtrl.Instance.GetParent();
                    if ( (parentTr != null ) && parentTr.CompareTag("RAINDROP"))
                        StartCoroutine("DelayLerpPosition", 0f);
                    else
                        StartCoroutine("DelayLerpPosition", 1f);
                }
                else
                {

                    if (fTargetDist < fFarDist)
                        fTargetDist = Mathf.Lerp(fTargetDist, fFarDist, 0.3f);
                    transform.position = targetTr.position + (-targetTr.forward * fTargetDist) + (targetTr.up * fTargetHeight);
                }
           
            }
            MakeObjTransform();
        }
        else  // movecState == eMoveState.COLLIDER;
        {
            // transform.position = CollisionManager.Instance.Get_RayCollisionPositionFromObj(transform.position,  )
            if (PlayerCtrl.Instance.state == Constants.ST_CLING)
            {
                // 카메라가 멀리 떨어지게함...
                if (isLookFar != true)
                {
                    StartCoroutine("DelayLerpPosition", 1.5f);
                    //tr.localPosition = Vector3.Lerp(tr.localPosition
                    //                   , FirstLocalPosition + new Vector3(Target_fXAngle * 0.8f, 0.01f, -Target_fSpeed * 0.1f)
                    //                 , 0.1f);
                }
                else
                {
                    // if (isCollisionOthers != true)  // 카메라 충돌 체크, 플레이어가 가려지는 일이 발생하지 않게 함
                    //  {
                    Vector3 vDir = transform.position - targetTr.position;
                    vDir.Normalize();

                    //if (fTargetDist < fFarDist)
                     //   fTargetDist += Time.deltaTime;

                    if (fTargetDist < fFarDist)
                        fTargetDist = Mathf.Lerp(fTargetDist, fFarDist, 0.3f);

                    //이게 정상동작 코드
                    // transform.position = CollisionManager.Instance.Get_RayCollisionPositionFromObj(targetTr.position, vDir, fFarDist, "CAMERA");
                    transform.position = CollisionManager.Instance.Get_RayCollisionPositionFromObj(targetTr.position, vDir, fTargetDist, "CAMERA");

                    //Vector3 vRayPos = CollisionManager.Instance.Get_RayCollisionPositionFromObj(targetTr.position, vDir, fFarDist, "CAMERA");
                 //   print("position : " + transform.position + ", vRay : " + vRayPos);
                
                 
                    //transform.position = Vector3.Lerp(transform.position, vRayPos, 0.1f);
                   // transform.position = vLerp;
                    print("position : " + transform.position);
                    //transform.position = CollisionManager.Instance.Get_RayCollisionPositionFromObj(transform.position,  )

                    /*
                      //tr.localPosition = Vector3.Lerp(tr.localPosition
                      //                     , FirstLocalPosition + new Vector3(Target_fXAngle * 0.8f, 0.15f, -20f)
                          //                   , 0.05f);
                      tr.localPosition = Vector3.Lerp(tr.localPosition
                      , FirstLocalPosition + new Vector3(Target_fXAngle * 0.8f, Target_fYAngle * 0.8f, -20f)
                      , 0.05f);
                    */
                    //   }

                }
            }
        





        }

        // }

    }

    public void SetTarget(GameObject _obj)
    {
        if (targetTr != null)
            targetTr = null;

        targetTr = _obj.transform;
        transform.LookAt(targetTr);
    }

    public void SetPosition(Vector3 _pos)
    {
        transform.position = _pos;
    }

    public void SetLocalPosition(Vector3 _pos)
    {
        transform.localPosition = _pos;
    }
    private void MakeObjTransform()
    {
        RaycastHit[] RayHit = CollisionManager.Instance.Get_RayCollisionsAFromB(this.transform, rayTarget);     // 여기에 충돌한 정보가 들어감 
        List<GameObject> newRayHitObjList = new List<GameObject>();                                             // RayHit에서 충돌한 충돌체들을 넣어줄 것임 

        // RayHit에서 충돌한 충돌체들을 newRayHitObjList에 넣어줌
        for (int i = 0; i < RayHit.Length; ++i)
        {
            if (RayHit[i].collider.gameObject.CompareTag("PLAYER"))
                continue;
            else
                newRayHitObjList.Add(RayHit[i].collider.gameObject);
        }

        // newRayHitObjList에 들어가 있는 Obj들의 셰이더를 투명하게 바꿈 
        for (int i = 0; i < newRayHitObjList.Count; ++i)
        {
            MeshRenderer renderer = newRayHitObjList[i].GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.shader = Shader.Find("Transparent/VertexLit");
                // renderer.material.shader = Shader.Find("Mobile/Particles/Multiply");// Transparent/VertexLit");

                // print("투명투명");

                if (renderer.material.HasProperty("_Color"))
                {
                   // print("rayHitObjList[i].collider.gameObject.name : " + newRayHitObjList[i].name);

                    Color prevColor = renderer.material.GetColor("_Color");
                    //print("color : " + prevColor);
                    //renderer.material.SetFloat("_Mode", 3f);
                    renderer.material.SetColor("_Color", new Color(prevColor.r, prevColor.g, prevColor.b, 0.5f));
                    //   print("컬러에 들어옴");
                }
            }
        }



        // preRayHitObjList와 newRayHitObjList를 비교하여, Ray가 닿지 않은 Obj라면 셰이더 상태를 원래대로 돌려 놓는다.
        for (int i = 0; i < preRayHitObjList.Count; ++i)
        {
            if (!newRayHitObjList.Find(delegate (GameObject _Obj) { return (_Obj.name == preRayHitObjList[i].name); })) //preRayHitObjList[i]);
            {
                string nameShader = "Mobile/Unlit (Supports Lightmap)";
                //string nameShader = "Standard";
                MeshRenderer renderer = preRayHitObjList[i].GetComponent<MeshRenderer>();
                if (renderer != null)
                    renderer.material.shader = Shader.Find(nameShader);
                // renderer.sharedMaterial 을 사용하래  // 수정 
            }
        }

        // swap
        preRayHitObjList = newRayHitObjList;

    }


    private IEnumerator DelayLerpPosition(float fTime)
    {

        yield return new WaitForSeconds(fTime);

        isLookFar = true;

    }

    public void SetStateToCollider(bool _bool)
    {
        if (_bool)
            moveState = eMoveState.COLLIDER;
        else
            moveState = eMoveState.TRIGGER;
    }

}
