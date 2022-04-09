using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class ReacherRobot : Agent
{
    public GameObject gol1;
    public GameObject gol2;
    public GameObject gol3;
    public GameObject gol4;
    public GameObject gol5;
    public GameObject gol6;
    public GameObject gol7;

    ArticulationBody al1;
    ArticulationBody al2;
    ArticulationBody al3;
    ArticulationBody al4;
    ArticulationBody al5;
    ArticulationBody al6;
    ArticulationBody al7;


    public GameObject hand; //tool0
    public GameObject goal;

    //altura de la bola
    public float m_GoalHeight = 1.8f;

    //private value
    float m_GoalRadius; //radio goal area
    float m_GoalDegree; //how much the goal rotates 
    float m_GoalSpeed; //speed of the goal rotation
    float m_GoalDeviation; //how much goes up and down (from the goal height)
    float m_GoalDeviationFreq; //frequency of the goal up and down movement

    public override void Initialize()
    {
        //ArticulationBody body = GetComponent<ArticulationBody>();
        al1 = gol1.GetComponent<ArticulationBody>();
        al2 = gol1.GetComponent<ArticulationBody>();
        al3 = gol1.GetComponent<ArticulationBody>();
        al4 = gol1.GetComponent<ArticulationBody>();
        al5 = gol1.GetComponent<ArticulationBody>();
        al6 = gol1.GetComponent<ArticulationBody>();
        al7 = gol1.GetComponent<ArticulationBody>();

        SetResetParameters();
    }

    public override void OnEpisodeBegin()
    {
        gol1.transform.position = new Vector3(0f, 0f, 0f);
        gol1.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        al1.velocity = Vector3.zero; // no movment
        al1.angularVelocity = Vector3.zero;

        gol2.transform.position = new Vector3(0f, 0.36f, -0.00043624f);
        gol2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        al2.velocity = Vector3.zero; // no movment
        al2.angularVelocity = Vector3.zero;

        gol3.transform.position = new Vector3(0f, 0f, 0f);
        gol3.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        al3.velocity = Vector3.zero; // no movment
        al3.angularVelocity = Vector3.zero;

        gol4.transform.position = new Vector3(0f, 0.42f, 0.00043624f);
        gol4.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        al4.velocity = Vector3.zero; // no movment
        al4.angularVelocity = Vector3.zero;

        gol5.transform.position = new Vector3(0f, 0f, 0f);
        gol5.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        al5.velocity = Vector3.zero; // no movment
        al5.angularVelocity = Vector3.zero;

        gol6.transform.position = new Vector3(0f, 0.4f, 0f);
        gol6.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        al6.velocity = Vector3.zero; // no movment
        al6.angularVelocity = Vector3.zero;

        gol7.transform.position = new Vector3(0f, 0f, 0f);
        gol7.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        al7.velocity = Vector3.zero; // no movment
        al7.angularVelocity = Vector3.zero;

        SetResetParameters();
        m_GoalDegree += m_GoalSpeed;
        UpdateGoalPosition();

    }

    public void SetResetParameters()
    {
        //random private values
        m_GoalRadius = Random.Range(0.7f, 1f); //meters de 1 a 3
        m_GoalDegree = Random.Range(0f, 360f);
        m_GoalSpeed = Random.Range(-1.3f, 1.3f); //-2,2
        m_GoalDeviation = Random.Range(-0.2f, 0.2f); //-1,1
        m_GoalDeviationFreq = Random.Range(0f, 3.14f);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(gol1.transform.localPosition);
        sensor.AddObservation(gol1.transform.rotation);
        sensor.AddObservation(al1.angularVelocity);
        sensor.AddObservation(al1.velocity);

        sensor.AddObservation(gol2.transform.localPosition);
        sensor.AddObservation(gol2.transform.rotation);
        sensor.AddObservation(al2.angularVelocity);
        sensor.AddObservation(al2.velocity);

        sensor.AddObservation(gol3.transform.localPosition);
        sensor.AddObservation(gol3.transform.rotation);
        sensor.AddObservation(al3.angularVelocity);
        sensor.AddObservation(al3.velocity);

        sensor.AddObservation(gol4.transform.localPosition);
        sensor.AddObservation(gol4.transform.rotation);
        sensor.AddObservation(al4.angularVelocity);
        sensor.AddObservation(al4.velocity);

        sensor.AddObservation(gol5.transform.localPosition);
        sensor.AddObservation(gol5.transform.rotation);
        sensor.AddObservation(al5.angularVelocity);
        sensor.AddObservation(al5.velocity);

        sensor.AddObservation(gol6.transform.localPosition);
        sensor.AddObservation(gol6.transform.rotation);
        sensor.AddObservation(al6.angularVelocity);
        sensor.AddObservation(al6.velocity);

        sensor.AddObservation(gol7.transform.localPosition);
        sensor.AddObservation(gol7.transform.rotation);
        sensor.AddObservation(al7.angularVelocity);
        sensor.AddObservation(al7.velocity);

        sensor.AddObservation(goal.transform.localPosition);
        sensor.AddObservation(hand.transform.localPosition);

        sensor.AddObservation(m_GoalSpeed);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var continuosActions = actions.ContinuousActions;
        var torque = Mathf.Clamp(continuosActions[0], -1f, 1f) * 15f; //se recibe 15 times
        al1.AddTorque(new Vector3(0f, torque, 0f)); //se convierte en un force

        torque = Mathf.Clamp(continuosActions[1], -1f, 1f) * 15f;
        al2.AddTorque(new Vector3(0f, 0f, torque));

        torque = Mathf.Clamp(continuosActions[2], -1f, 1f) * 15f;
        al3.AddTorque(new Vector3(0f, 0f, torque));

        torque = Mathf.Clamp(continuosActions[3], -1f, 1f) * 15f;
        al4.AddTorque(new Vector3(torque, 0f, 0f));

        torque = Mathf.Clamp(continuosActions[4], -1f, 1f) * 15f;
        al5.AddTorque(new Vector3(0f, torque, 0f));

        torque = Mathf.Clamp(continuosActions[5], -1f, 1f) * 15f;
        al6.AddTorque(new Vector3(0f, 0f, torque));

        torque = Mathf.Clamp(continuosActions[6], -1f, 1f) * 15f;
        al7.AddTorque(new Vector3(0f, torque, 0f));


        ///////NEW
        // Rewards
        float distanceToTarget = Vector3.Distance(hand.transform.localPosition, goal.transform.localPosition);

        // Reached target
        if (distanceToTarget < 1f)
        {
            SetReward(0.8f);
            EndEpisode();
        }
        ///////

        m_GoalDegree += m_GoalSpeed;
        UpdateGoalPosition();

    }

    void UpdateGoalPosition()
    {
        var m_GoalDegree_rad = m_GoalDegree * Mathf.PI / 180F;
        //calcula el circulo por el que se mueve
        var goalX = m_GoalRadius * Mathf.Cos(m_GoalDegree_rad);
        var goalZ = m_GoalRadius * Mathf.Sin(m_GoalDegree_rad);

        var goalY = m_GoalHeight; // + m_GoalDeviation * Mathf.Cos(m_GoalDeviationFreq * m_GoalDegree_rad); //para ups and downs 

        goal.transform.position = new Vector3(goalX, goalY, goalZ);
    }
}
