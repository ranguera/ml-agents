using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class FaltaGoalie : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        var agent = collision.gameObject.GetComponent<Agent>();
        if (agent != null)
        {
            agent.AddReward(.1f);
        }
    }
}
