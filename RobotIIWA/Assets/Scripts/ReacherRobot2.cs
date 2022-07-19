using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class ReacherRobot2 : Agent
{
  
    public GameObject pendulumA;
    public GameObject pendulumB;
    public GameObject pendulumC;
    public GameObject pendulumD;
    public GameObject pendulumE;
    public GameObject pendulumF;
    public GameObject pendulumG;

    Rigidbody m_RbA;
    Rigidbody m_RbB;
    Rigidbody m_RbC;
    Rigidbody m_RbD;
    Rigidbody m_RbE;
    Rigidbody m_RbF;
    Rigidbody m_RbG;

    public GameObject hand;
    public GameObject goal;

    public float m_GoalHeight = 0.9f;
    float m_GoalRadius; //Radius of the goal zone
    float m_GoalDegree; //How much the goal rotates
    float m_GoalSpeed; //Speed of the goal rotation
    float m_GoalDeviation; //How much goes up and down 
    float m_GoalDeviationFreq; //Frequency of the goal up and down movement

    public bool blHeuristic = false;

    public override void Initialize()
    {
        m_RbA = pendulumA.GetComponent<Rigidbody>();
        m_RbB = pendulumB.GetComponent<Rigidbody>();
        m_RbC = pendulumC.GetComponent<Rigidbody>();
        m_RbD = pendulumD.GetComponent<Rigidbody>();
        m_RbE = pendulumE.GetComponent<Rigidbody>();
        m_RbF = pendulumF.GetComponent<Rigidbody>();
        m_RbG = pendulumG.GetComponent<Rigidbody>();

        SetResetParameters();
    }

    public override void OnEpisodeBegin()
    {
        pendulumA.transform.position = new Vector3(0f, 0f, 0f) + transform.position;
        pendulumA.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        m_RbA.velocity = Vector3.zero;
        m_RbA.angularVelocity = Vector3.zero;

        pendulumB.transform.position = new Vector3(0f, 0.36f, -0.004f) + transform.position;
        pendulumB.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        m_RbB.velocity = Vector3.zero;
        m_RbB.angularVelocity = Vector3.zero;

        pendulumC.transform.position = new Vector3(0f, 0f, 0f) + transform.position;
        pendulumC.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        m_RbC.velocity = Vector3.zero;
        m_RbC.angularVelocity = Vector3.zero;

        pendulumD.transform.position = new Vector3(0f, 0.42f, 0.00043624f) + transform.position;
        pendulumD.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        m_RbD.velocity = Vector3.zero;
        m_RbD.angularVelocity = Vector3.zero;

        pendulumE.transform.position = new Vector3(0f, 0f, 0f) + transform.position;
        pendulumE.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        m_RbE.velocity = Vector3.zero;
        m_RbE.angularVelocity = Vector3.zero;

        pendulumF.transform.position = new Vector3(0f, 0.4f, 0f) + transform.position;
        pendulumF.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        m_RbF.velocity = Vector3.zero;
        m_RbF.angularVelocity = Vector3.zero;

        pendulumG.transform.position = new Vector3(0f, 0f, 0f) + transform.position;
        pendulumG.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        m_RbG.velocity = Vector3.zero;
        m_RbG.angularVelocity = Vector3.zero;

        SetResetParameters();
        m_GoalDegree += m_GoalSpeed;
        UpdateGoalPosition();

    }

    public void SetResetParameters()
    {
        m_GoalRadius = Random.Range(0.6f, 0.6f);
        m_GoalDegree = Random.Range(0f, 360f);
        m_GoalSpeed = Random.Range(-1.5f, 1.5f); //-2,2
        m_GoalDeviation = Random.Range(-0.2f, 0.2f); //-1,1
        m_GoalDeviationFreq = Random.Range(0f, 3.14f); // 0,3.14
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(pendulumA.transform.localPosition);
        sensor.AddObservation(pendulumA.transform.rotation);
        sensor.AddObservation(m_RbA.angularVelocity);
        sensor.AddObservation(m_RbA.velocity);

        sensor.AddObservation(pendulumB.transform.localPosition);
        sensor.AddObservation(pendulumB.transform.rotation);
        sensor.AddObservation(m_RbB.angularVelocity);
        sensor.AddObservation(m_RbB.velocity);

        sensor.AddObservation(pendulumC.transform.localPosition);
        sensor.AddObservation(pendulumC.transform.rotation);
        sensor.AddObservation(m_RbC.angularVelocity);
        sensor.AddObservation(m_RbC.velocity);

        sensor.AddObservation(pendulumD.transform.localPosition);
        sensor.AddObservation(pendulumD.transform.rotation);
        sensor.AddObservation(m_RbD.angularVelocity);
        sensor.AddObservation(m_RbD.velocity);

        sensor.AddObservation(pendulumE.transform.localPosition);
        sensor.AddObservation(pendulumE.transform.rotation);
        sensor.AddObservation(m_RbE.angularVelocity);
        sensor.AddObservation(m_RbE.velocity);

        sensor.AddObservation(pendulumF.transform.localPosition);
        sensor.AddObservation(pendulumF.transform.rotation);
        sensor.AddObservation(m_RbF.angularVelocity);
        sensor.AddObservation(m_RbF.velocity);

        sensor.AddObservation(pendulumG.transform.localPosition);
        sensor.AddObservation(pendulumG.transform.rotation);
        sensor.AddObservation(m_RbG.angularVelocity);
        sensor.AddObservation(m_RbG.velocity);

        sensor.AddObservation(hand.transform.localPosition);
        sensor.AddObservation(goal.transform.localPosition);
        sensor.AddObservation(m_GoalSpeed);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var vectorAction = actions.ContinuousActions;
        var torque = Mathf.Clamp(vectorAction[0], -1f, 1f) * 150f;
        m_RbA.AddTorque(new Vector3(0f, torque, 0f));

        torque = Mathf.Clamp(vectorAction[1], -1f, 1f) * 150f;
        m_RbB.AddTorque(new Vector3(torque, 0f, 0f));

        torque = Mathf.Clamp(vectorAction[2], -1f, 1f) * 150f;
        m_RbC.AddTorque(new Vector3(0f, torque, 0f));

        torque = Mathf.Clamp(vectorAction[3], -1f, 1f) * 150f;
        m_RbD.AddTorque(new Vector3( torque, 0f, 0f));

        torque = Mathf.Clamp(vectorAction[4], -1f, 1f) * 150f;
        m_RbE.AddTorque(new Vector3(0f,  torque, 0f));

        torque = Mathf.Clamp(vectorAction[5], -1f, 1f) * 150f;
        m_RbF.AddTorque(new Vector3( torque, 0f, 0f));

        torque = Mathf.Clamp(vectorAction[6], -1f, 1f) * 150f;
        m_RbG.AddTorque(new Vector3(0f, torque, 0f));

        m_GoalDegree += m_GoalSpeed;
        UpdateGoalPosition();
    }

    public void UpdateGoalPosition()
    {
        if (!blHeuristic)
        {
            var m_GoalDegree_rad = m_GoalDegree * Mathf.PI / 180f;
            var goalX = m_GoalRadius * Mathf.Cos(m_GoalDegree_rad);
            var goalZ = m_GoalRadius * Mathf.Sin(m_GoalDegree_rad);
            var goalY = m_GoalHeight; // + m_GoalDeviation * Mathf.Cos(m_GoalDeviationFreq * m_GoalDegree_rad);

            goal.transform.position = new Vector3(goalX, goalY, goalZ) + transform.position;
        }
    }

    private void Update()
    {
        Debug.DrawLine(hand.transform.position, goal.transform.position, Color.green);
    }

    

}
