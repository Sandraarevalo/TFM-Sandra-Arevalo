using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReacherGoal : MonoBehaviour
{
    public GameObject agent;
    public GameObject hand;
    public GameObject goalOn;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == hand) //cuando algo entra en contacto con la bola compruebo si es la mano
        {
            goalOn.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f); //aumentamos la bola para que se vea mejor
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == hand)
        {
            goalOn.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f); //when trigger exits the ball measure goes back to the original
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == hand)
        {
            agent.GetComponent<ReacherRobot>().AddReward(1f); //0.01f
        }
    }
}
