using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalieMovement : MonoBehaviour
{
    public float span = 6f;
    public float speed = 10f;

    private Vector3 horz;

    private void Start()
    {
        this.horz = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        this.horz.z = Mathf.PingPong(Time.time * speed, span) - 3f;
        this.transform.localPosition = this.horz;
    }
}
