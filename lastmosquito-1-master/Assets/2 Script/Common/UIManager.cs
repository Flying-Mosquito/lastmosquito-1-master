using UnityEngine;
using System.Collections;


//UICanvas에 넣어준다
public class UIManager : MonoBehaviour {

    public static bool isFadeOut;
    public static string strSceneName;
    private Animator Anim;



    // Use this for initialization
    void Awake()
    {
        Anim = GetComponent<Animator>();
        isFadeOut = false;
        strSceneName = null;
        FadeIn();
    }
	// Update is called once per frame
	void Update ()
    {


        if (isFadeOut)
        {
            if (strSceneName != null)
            {
                FadeOut();
            }
        
        }
        else
        {
            FadeIn();
        }
    }

    private void FadeOut()  // 장면이 어두워진다
    {
        Anim.SetBool("isFadeOut", true);
        GameManager.Instance.SetWaitForLoadingSeconds(1.5f);
        GameManager.Instance.StartCoroutine("StartLoad", strSceneName);
    }

    private void FadeIn()   // 장면이 밝아진다
    {
        Anim.SetBool("isFadeOut", false);
    }

    private void Scene_change()
    {

    }

}
