using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour {
    public Button exit;
    public Button back;
    // Use this for initialization


    public bool backBool;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Click()
    {
       
        SceneManager.LoadScene(3);

        //PlayerCtrl.Instance.state = Constants.ST_IDLE;
       PlayerCtrl.Instance.variable &= ~(Constants.BV_Stick);
        PlayerCtrl.Instance.SetStateIdle(true);
        Timer.Instance.totaltime = 60;
        Timer.Instance.isEnable = true;
    }
    public void backLobby()
    {
        SceneManager.LoadScene(13);
    }
}
