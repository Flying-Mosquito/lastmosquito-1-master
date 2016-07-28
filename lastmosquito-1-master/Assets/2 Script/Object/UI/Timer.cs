using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : Singleton<Timer>
{

    public Text timerText;
    public float startTime;
    public Transform gameover;
    public Transform gameClear;
    public float totaltime = 60;
   public bool isEnable = false;
  

  

    // Use this for initialization


    void onEnable()
    {
        startTimer();
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {

        startTime = Time.time;

        startTimer();

    }

    // Update is called once per frame
    void Update()
    {
       
       
       
        if (PlayerCtrl.Instance.iHP <5|| totaltime < 1)
        {
            gameover.gameObject.SetActive(true);
           
            StopTimer();
           
        }
       
        else
        {
            gameover.gameObject.SetActive(false);
        }
       
        if (PlayerCtrl.Instance.iBlood > 190)
        {
            gameClear.gameObject.SetActive(true);
            StopTimer();



        }
       
        else 
            gameClear.gameObject.SetActive(false);
       


        if (isEnable)
        {
             totaltime -=Time.deltaTime;
        }



        string minutes = ((int)totaltime / 60).ToString();
        string seconds = (totaltime % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;




    }
    
    public void startTimer()
    {
       
        isEnable = true;
    }
    public void StopTimer()
    {
      
        isEnable = false;
    }
}
