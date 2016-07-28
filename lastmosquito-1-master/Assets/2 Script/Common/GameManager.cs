﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    /*
   
    */
    public float waitForLoadingSeconds;
    private bool isLoadGame = false;                                    // loding drag
    private float time;
    

    // public Image fadeImage;
    void Awake()
    {
        DontDestroyOnLoad(this);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
      
        // Init_Singleton();
    }

    public IEnumerator StartLoad(string strSceneName)
    {
        /*if ( "00 Logo" == Application.loadedLevelName )
        {
          
        }
        */

        if (isLoadGame == false)
        {
            isLoadGame = true;

            AsyncOperation async = SceneManager.LoadSceneAsync(strSceneName);//Application.LoadLevelAsync(strSceneName);


            async.allowSceneActivation = false; // 씬을 로딩후 자동으로 넘어가지 못하게 한다.

            while (!async.isDone)
            {
                time += Time.deltaTime;

                if (time >= waitForLoadingSeconds)  // waitForLoadingSeconds는 현재 2으로 설정해 놓았다.
                {
                    isLoadGame = false;
                    async.allowSceneActivation = true;  // 2초 후에 씬을 넘김 
                    UIManager.isFadeOut = false;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public void SetWaitForLoadingSeconds(float _fTime)
    {
        waitForLoadingSeconds = _fTime;
    }





}

