using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;


public class ReacherGoalMan : MonoBehaviour
{

    public GameObject agent;
    public GameObject hand;
    public GameObject goalOn;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == hand)
        {
            goalOn.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == hand)
        {
            goalOn.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == hand)
        {
            Debug.LogWarning("tocado only");
        
        float bonus = Mathf.Clamp01(Vector3.Dot(goalOn.transform.up.normalized, hand.transform.up.normalized)); //Si la pinza apunta correctamente hacia el vector superior del objetivo, le damos una bonificación.
            if (bonus > 0.5)
            {
                agent.GetComponent<ReacherRobotMan>().AddReward(0.5f + bonus);
                Debug.LogWarning("tocado > 0.5 : " + bonus);
            }

        }
    }

}
