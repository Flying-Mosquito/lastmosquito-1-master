using UnityEngine;
using System.Collections;

public class Spray : MonoBehaviour {

    public Transform OnSpray;
  
    // Use this for initialization
  

    // Update is called once per frame
    void Update () {
	if(EnemyAI.Instance.angrygauge>70 && EnemyAI.Instance.angrygauge<90)
        {
            OnSpray.gameObject.SetActive(true);
            EnemyAI.Instance.state = EnemyAI.State.CHASE;
        }
    else
        {
            OnSpray.gameObject.SetActive(false);
        }

    }
}
