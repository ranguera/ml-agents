using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class FaltaAgent : Agent
{
    [Header("Bouncer Specific")]
    public GameObject target;
    public float strength = 10f;

    Rigidbody m_Rb;

    ResetParameters m_ResetParams;

    private Vector3 initial_position;

    public override void InitializeAgent()
    {
        m_Rb = gameObject.GetComponent<Rigidbody>();

        var academy = FindObjectOfType<Academy>();
        m_ResetParams = academy.resetParameters;

        initial_position = transform.localPosition;
    }

    public override void CollectObservations()
    {
        //print("collect");
        AddVectorObs(target.transform.localPosition);
    }

    public override void AgentAction(float[] vectorAction)
    {
        //print("action");
        for (var i = 0; i < vectorAction.Length; i++)
        {
            vectorAction[i] = Mathf.Clamp(vectorAction[i], -1f, 1f);
        }
        var x = vectorAction[0]*strength;
        var y = vectorAction[1]*strength;
        var z = vectorAction[2]*strength;
        //print("force: " + new Vector3(x, y, z));
        m_Rb.AddForce(new Vector3(x, y, z));
        //print(m_Rb.velocity);
        //print(m_Rb.velocity.magnitude);

        /*AddReward(-0.01f * (
            vectorAction[0] * vectorAction[0] +
            vectorAction[1] * vectorAction[1] +
            vectorAction[2] * vectorAction[2]) / 3f);*/
    }

    public override void AgentReset()
    {
        //print("reset");
        transform.localPosition = initial_position;
        //RequestDecision();
    }

    public override void AgentOnDone()
    {
        //print("agentondone");
        //print(transform.localPosition + " - " + initial_position);
        transform.localPosition = initial_position;
        //print(transform.localPosition);
        //RequestDecision();
    }

    // Simulation steps containing reset conditions
    void FixedUpdate()
    {
        //print(m_Rb.velocity);
        //print(m_Rb.velocity.magnitude);

        if (gameObject.transform.localPosition.x > 20f)
        {
            AddReward(-.05f);
            Done();
            return;
        }
        if (gameObject.transform.localPosition.x < -2f)
        {
            AddReward(-.2f);
            Done();
            return;
        }

        if (gameObject.transform.localPosition.z < -9f)
        {
            AddReward(-.1f);
            Done();
            return;
        }

        if (gameObject.transform.localPosition.z > 9f)
        {
            AddReward(-.1f);
            Done();
            return;
        }

        if (m_Rb.velocity.magnitude < .01f)
        {
            AddReward(-.1f);
            Done();
            return;
        }
    }

    // Keyboard entry to help train
    public override float[] Heuristic()
    {
        var action = new float[3];

        action[1] = Input.GetAxis("Horizontal");
        action[0] = Input.GetKey(KeyCode.Space) ? 100.0f : -100.0f;
        action[2] = Input.GetAxis("Vertical");
        return action;
    }

    // Visual effects on movement
    void Update()
    {
        this.transform.Rotate(m_Rb.velocity);

        //print("upd: " + m_Rb.velocity);
        //print("upd: " + m_Rb.velocity.magnitude);
    }
}