using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CollisionManager : Singleton<CollisionManager>
{

    private RaycastHit hit; // Ray가 맞은 위치 정보를 받아올 구조체
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public bool Check_MouseCollision(Component _Obj) // 마우스 클릭시 충돌체가 있다면 true리턴 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //   Debug.DrawRay(ray.origin, ray.direction, Color.green); // 여기서 해도 안보여요 
        Physics.Raycast(ray, out hit, 2000.0f, ~(1 << LayerMask.NameToLayer("PLAYER")));
        if (hit.collider == _Obj.GetComponent<Collider>())
            return true;
        return false;
    }
    public bool Check_MouseCollision(string _tag, float _fDist = 2000f) // 마우스 클릭시 충돌체가 있다면 true리턴 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //   Debug.DrawRay(ray.origin, ray.direction, Color.green); // 여기서 해도 안보여요 

        Physics.Raycast(ray, out hit, _fDist);
        if (null == _tag || null == hit.collider)
            return false;


        if (hit.collider.tag == _tag)
            return true;

        return false;
    }

    public GameObject Get_RaycastCollisionObj(Vector3 _pos, Vector3 _vDir, float _fDist)
    {
        if (Physics.Raycast(_pos, _vDir, out hit, _fDist))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    public GameObject Get_MouseCollisionObj(float _fDist = 3000f, string _Layer = "")   // _Layer 레이어를 가진 물체중에서 마우스와 충돌된 물체 리턴 
    {
        bool bRaycast;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        // _tag가 있으면 레이어 비교, _tag가 없으면 그냥 Raycast
        if ("" != _Layer)
        {
            bRaycast = Physics.Raycast(ray, out hit, _fDist, 1 << LayerMask.NameToLayer(_Layer));
        }
        else
        {
            bRaycast = Physics.Raycast(ray, out hit, _fDist);
        }

        if (!bRaycast)  // bRaycast가 false면 null 리턴 
            return null;

        return hit.collider.gameObject;

        // return null;
    }
    public RaycastHit[] Get_RayCollisionsAFromB(Transform _startTr, Transform _targetTr)
    {
        // List<RaycastHit[]> objArr = new List<RaycastHit[]>();

        Vector3 vec = _targetTr.position - _startTr.position;
        vec.Normalize();
        float fDist = Vector3.Distance(_startTr.position, (_targetTr.position - vec)); // 플레이어보다 더 긴 거리를 체크하는 것 같아서 빼줌 
                                                                                       // print("길이 : " + fDist);
                                                                                       //objArr = Physics.RaycastAll(_targetTr.position, vec, fDist);//Physics.Raycast(_targetTr.position, vec, out hit, fDist))
                                                                                       //  objArr.Add( Physics.RaycastAll(_targetTr.position, vec, fDist) );
        return Physics.RaycastAll(_startTr.position, vec, fDist); // objArr;

    }

    public bool Check_RayHit(Component _Obj, string _Tag, float _fDist = 0) // _Obj의 forward방향, _fDist길이 만큼 만든 Ray가 충돌한 충돌체가 _Tag이름과 같다면 true리턴
    {
        // 특정 거리가 입력되지 않았다면 거리를 길게 잡음 
        if (0 == _fDist)
            _fDist = 2000f;

        if (Physics.Raycast(_Obj.transform.position, _Obj.transform.forward, out hit, _fDist))
            if (hit.collider.tag == _Tag)
                return true;

        return false;
    }

    public Vector3 Get_RayCollisionPositionFromObj(Vector3 _pos, Vector3 _vDir, float _fDist, string _tag = "")
    {
        if (Physics.Raycast(_pos, _vDir, out hit, _fDist, 1 << LayerMask.NameToLayer(_tag)))
        {

            return hit.point;
        }

        return _pos + (_vDir * _fDist);
    }
    public bool Check_RayHit(Vector3 _position, Vector3 _Forward, string _Tag, float _fDist = 0) // _Obj의 forward방향, _fDist길이 만큼 만든 Ray가 충돌한 충돌체가 _Tag이름과 같다면 true리턴
    {

        // 특정 거리가 입력되지 않았다면 거리를 길게 잡음 
        if (0 == _fDist)
            _fDist = 2000f;

        if (Physics.Raycast(_position, _Forward, out hit, _fDist))
            if (hit.collider.tag == _Tag)
                return true;

        return false;
    }


    public bool Check_Sight(Transform _trPoint, Vector3 _targetPos, float _fLength = 10, float _fAngle = 40)    // Check_Sight(탐색을 시작할 점, 
    {
        Vector3 vDir = _targetPos - _trPoint.transform.position;   // 플레이어 
                                                                   //  print("vDir Length = " + Vector3.Distance(_Player.transform.position, tr.position).ToString());
        vDir.Normalize();

        if (Physics.Raycast(_trPoint.transform.position, vDir, out hit, _fLength) && (Vector3.Angle(_trPoint.forward, vDir) < _fAngle))   // 범위안에 들어와 있으면서, 각도가 40보다 작다
            return true;

        return false;

    }
}
