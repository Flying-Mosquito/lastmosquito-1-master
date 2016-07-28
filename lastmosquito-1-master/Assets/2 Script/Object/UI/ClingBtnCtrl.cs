using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClingBtnCtrl : BaseButton//MonoBehaviour
{
    public Text Text;
    private Transform tr;
   // private bool isMouseDown = false;
    private PlayerCtrl player;
    // Use this for initialization
    void Start () {
        tr = GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<PlayerCtrl>();
    }
	

    public override void OnTouchBegin(Vector2 _pos)
    {
        isMouseDown = true;
        //isOnTouch = true;
        player.ClingBtDown(); // left가 되었다
        Text.text = "OnTouchBegin";
    }
    /*
    public override void OnTouchStay()
    {
        player.Rightbuttondown(); // left가 되었다
        Text.text = "OnTouchStay";
    }
    */
    public override void OnTouchMove(Vector2 _pos)
    {
        if(isMouseDown)
            player.ClingBtDown();
        Text.text = "OnTouchMove";
    }

    public override void OnTouchEnd(Vector2 _pos)
    {
        isMouseDown = false;
        player.ClingBtUp();
        Text.text = "OnTouchEnd";
    }
}




