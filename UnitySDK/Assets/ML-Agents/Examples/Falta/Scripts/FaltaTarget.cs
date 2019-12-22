using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class FaltaTarget : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        var agent = collision.gameObject.GetComponent<Agent>();
        if (agent != null)
        {
            agent.AddReward(1f);
            agent.Done();
        }
    }
}
