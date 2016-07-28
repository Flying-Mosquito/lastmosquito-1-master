using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MyTempScrip : Singleton<MyTempScrip>//MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    RaycastHit2D hit2d;
    //RaycastHit2D hit;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //EventSystemManager.currentSystem.IsPointerOverEventSystemObject()
            // ray = new Ray()
            // ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Vector3 _pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            //    Physics2D.
            Collider2D col;
            if (col = Physics2D.OverlapPoint(Input.mousePosition))//Physics.Raycast(ray, out hit, 30000f))//, 1 << LayerMask.NameToLayer("UI")))
            {
                print(" origin : " + ray.origin);
                print("direction : " + ray.direction);
                /* hit.transform.SendMessage("OnTouchBegin", new Vector2(_fX, _fY));

                 if (hit.transform != null)
                     _name = hit.transform.name.ToString();
                 else
                     _name = "NULL";

                 TempText3.text = "OnTouchBegin : " + _name;
                 */
            }
            else
            {
                print(" origin : " + ray.origin);
                print("direction : " + ray.direction);

            }

            //Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Ray2D ray = new Ray2D(pos, Vector2.zero);
            //print("마우스 : " + pos);

        }
    }
}
    
