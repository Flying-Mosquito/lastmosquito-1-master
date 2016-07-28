using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TouchEventManager : Singleton<TouchEventManager>//MonoBehaviour
{
    public Text TempText;
    public Text TempText2;
    public Text TempText3;
    public Text TempText4;

    public bool isTouchBegin3DObj; // 버튼 이외의 곳을 터치했는 지 체크 
    public GameObject raindrop;
    delegate void listener(string _str, float _fX, float _fY, int _iFingerId);
    event listener begin, move, end;

    BaseButton[] touchObject = new BaseButton[5]; //  id마다 터치한 obj
    private readonly float _fDist;

    // Use this for initialization

    void Start()
    {
        DontDestroyOnLoad(this);
        begin += OnTouch;
        move += OnTouch;
        end += OnTouch;

        isTouchBegin3DObj = false;
        raindrop = null;

        Input.multiTouchEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        raindrop = null;
        isTouchBegin3DObj = false;


#if !UNITY_ANDROID
        {
            if (Input.GetMouseButton(0))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    begin("OnTouchBegin", Input.mousePosition.x, Input.mousePosition.y, 0);
                }
                else
                {
                    move("OnTouchMove", Input.mousePosition.x, Input.mousePosition.y, 0);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                begin("OnTouchEnd", Input.mousePosition.x, Input.mousePosition.y, 0);

            }

        }
#else
        int iTouchCount = Input.touchCount;
        if (0 == iTouchCount || iTouchCount > 5)
            return;




        for ( int i = 0; i < iTouchCount; ++i)
        {
            Touch   touch = Input.GetTouch(i);
            int     iId = touch.fingerId;      

            Vector2 touchPosition = touch.position; // 터치 좌표

            if (TempText2 != null && (Input.touchCount > 0))
                TempText2.text = touchPosition.ToString();//Input.touchCount.ToString();


            if (touch.phase == TouchPhase.Began)
            {
                if (TempText != null && (Input.touchCount > 0))
                    TempText.text = Input.GetTouch(0).phase.ToString();

                begin("OnTouchBegin", touchPosition.x, touchPosition.y, iId);
            }
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (TempText != null && (Input.touchCount > 0))
                    TempText.text = Input.GetTouch(0).phase.ToString();

                move("OnTouchMove", touchPosition.x, touchPosition.y, iId);
            } 
            if(touch.phase == TouchPhase.Ended)
            {
                if (TempText != null && (Input.touchCount > 0))
                    TempText.text = Input.GetTouch(0).phase.ToString();

                end("OnTouchEnd", touchPosition.x, touchPosition.y, iId);
                   
             }
          


        }
#endif

    }

    void OnTouch(string _str, float _fX, float _fY, int _iFingerId)
    {
        //Ray ray = Camera.main.ScreenPointToRay(new Vector2(_fX, _fY));
        //RaycastHit hit;

        //   RaycastHit2D hit;

        // string _name;

        Vector2 _pos = new Vector2(_fX, _fY);
        Collider2D col;

        switch (_str)
        {
            case "OnTouchBegin":
                {

                    if (col = Physics2D.OverlapPoint(_pos))//Physics.Raycast(ray, out hit, 30000f))//, 1 << LayerMask.NameToLayer("UI")))
                    {
                        isTouchBegin3DObj = false;
                        if (col.gameObject.name == "HoldButton")
                            TempText4.text = Input.touchCount.ToString();//"HOLDBUTTON down";
                        else
                            TempText4.text = "";

                        touchObject[_iFingerId] = col.gameObject.GetComponent<BaseButton>();//col.gameObject;
                        /* hit.transform.SendMessage("OnTouchBegin", new Vector2(_fX, _fY));

                         if (hit.transform != null)
                             _name = hit.transform.name.ToString();
                         else
                             _name = "NULL";

                         TempText3.text = "OnTouchBegin : " + _name;
                         */
                        touchObject[_iFingerId].OnTouchBegin(_pos);
                        //  col.transform.SendMessage("OnTouchBegin", new Vector2(_fX, _fY));
                    }
                    else  // 터치는 했는데 충돌을 안했으면 버튼에 터치를 안한거지
                    {
                        isTouchBegin3DObj = true;
                        //3d체크, Raindrop만 체크함
                        Ray ray = Camera.main.ScreenPointToRay(_pos);
                        RaycastHit hit;


                        //Physics.Raycast(ray, out hit, _fDist, 1 << LayerMask.NameToLayer(_Layer));
                        if (Physics.Raycast(ray, out hit, 1000f, 1 << LayerMask.NameToLayer("RAINDROP")))
                        {
                            print("raindrop클릭!!!!");
                            raindrop = hit.collider.gameObject;
                        }
                        else
                            print("헛클릭");

                    }

                    break;
                }
            /*
        case "OnTouchStay":
            {
                if( touchObject[_iFingerId] != null)
                {
                   // if(col = Physics2D.OverlapPoint(_pos))
                        touchObject[_iFingerId].OnTouchStay();
                }

                    break;
            }
    */
            case "OnTouchMove":
                {
                    if (touchObject[_iFingerId] != null) // 이미 begin을 거쳐옴 ismousedown == true
                    {
                        touchObject[_iFingerId].OnTouchMove(_pos);
                    }
                    /*
                    if (col = Physics2D.OverlapPoint(_pos))//Physics.Raycast(ray, out hit, 30000f))//, 1 << LayerMask.NameToLayer("UI")))
                    {
                        if(touchObject[_iFingerId].isMouseDown == true)
                        {
                            touchObject[_iFingerId].OnTouchMove(new Vector2(_fX, _fY));
                        }
                     //   print("레이케스트 ok : " + col.gameObject.name);
                     //   col.transform.SendMessage("OnTouchMove", new Vector2(_fX, _fY));
                    }
                    else        // 드래그하면서 터치영역 밖으로 넘어가면
                    {
                        if (!touchObject[_iFingerId])
                        {
                            touchObject[_iFingerId].transform.SendMessage("OnTouchEnd", new Vector2(_fX, _fY), SendMessageOptions.DontRequireReceiver);
                            touchObject[_iFingerId] = null;
                        }
                    }
                    */
                    break;
                }
            case "OnTouchEnd":
                {
                    isTouchBegin3DObj = false;
                    if (touchObject[_iFingerId] != null)
                    {

                        touchObject[_iFingerId].OnTouchEnd(_pos);
                        touchObject[_iFingerId] = null;
                    }
                    /*
                    if (col = Physics2D.OverlapPoint(_pos))//Physics.Raycast(ray, out hit, 30000f))//, 1 << LayerMask.NameToLayer("UI")))
                    {

                        print("레이케스트 ok : " + col.gameObject.name);

                        col.transform.SendMessage("OnTouchEnd", new Vector2(_fX, _fY));
                        touchObject[_iFingerId] = null;
                    }
                */
                    break;
                }
        }
    }
}
