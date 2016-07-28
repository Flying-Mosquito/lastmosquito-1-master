using UnityEngine;
using System.Collections;
using System;



    public class EnemyAI  : Singleton<EnemyAI>
{

    public Transform target;
    public int moveSpeed;
    public int rotationSpeed;

    public float angrygauge = 0;
    
    Randommove movetransform;
   



    public Transform myTransform;
    public PlayerCtrl _Player;

    public Enemy character;

        public enum State
        {
            PATROL,CHASE,ATTACK,FOOT
        }

        public State state;
        private bool alive;
       
      

        //partol
        public GameObject[] Randommove;
    private int moveNumbe;
        public float patrolSpeed = 0.5f;

    //chase
    // Use this for initialization
    void Awake()
    {
        myTransform = transform;
    }
    void Start()
        {
      
        state = EnemyAI.State.PATROL;
            alive = true;
            StartCoroutine("FSM");
    }
    void Update()
      
    {
      
        if ((PlayerCtrl.Instance.state == Constants.ST_BLOOD))
        {

            PlayerCtrl.Instance.iBlood += 1;
           angrygauge += 1;

        }
       
        if (angrygauge>10)
        {
           
            angrygauge -= 4 * Time.deltaTime;
           
            if(Vector3.Distance(new Vector3(this.transform.position.x, 0, 0), new Vector3(target.transform.position.x, 0, 0)) <5)
            {
                state = EnemyAI.State.ATTACK;
            }
            else if (Vector3.Distance(this.transform.position, target.transform.position) > 5)
            {
                state = EnemyAI.State.CHASE;
            }

            

        }
        //patrol

        else if(angrygauge<10)
        {
            state = EnemyAI.State.PATROL;
        }

        //Foot
        if(angrygauge>10 && (Vector3.Distance(new Vector3(this.transform.position.x, 0, 0), new Vector3(target.transform.position.x, 0, 0)) < 5)&&PlayerCtrl.Instance.transform.position.y<5)
        {
            state = EnemyAI.State.FOOT;
        }
    }


   

    //   myTransform.rotation = Quaternion.Euler(0, target.position.z - myTransform.position.z * rotationSpeed * Time.deltaTime, 0);
    // myTransform.rotation = Quaternion.LookRotation(new Vector3(0, target.position.y,0) - myTransform.position);


    IEnumerator FSM()
        {
            while(alive)
            {
                switch(state)
                {
                    case State.PATROL:
                        
                            Patrol();
                            break;
                        
                    case State.CHASE:
                            Chase();
                            break;
                case State.ATTACK:
                    Attact();
                    break;

                case State.FOOT:
                    Foot();
                    break;
                }

                yield return null;
            }
        }

    void Foot()
    {

    }

    void Patrol()
        {
    GameObject go = GameObject.FindGameObjectWithTag("Move");

    target = go.transform;
    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
        myTransform.position += new Vector3(myTransform.forward.x * moveSpeed * Time.deltaTime, 0, myTransform.forward.z * moveSpeed * Time.deltaTime);
        //if (Vector3.Distance(this.transform.position,Randommove[moveNumber].transform.position)>=2)
        //        {


        //        }
    }



    void Chase()
        {
        GameObject go = GameObject.FindGameObjectWithTag("PLAYER");

        target = go.transform;
        WaitForSeconds.Equals(1, 5);

        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
        myTransform.rotation = Quaternion.Euler(new Vector3(0f, myTransform.rotation.eulerAngles.y, 0f));
        myTransform.position += new Vector3(myTransform.forward.x * moveSpeed * Time.deltaTime, 0, myTransform.forward.z * moveSpeed * Time.deltaTime);

    }

    void Attact()
    {

    }
    

        // Update is called once per frame

    }
