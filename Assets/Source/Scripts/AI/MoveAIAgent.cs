/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class MoveAIAgent : Agent
{
    public override void OnEpisodeBegin()
    {

    }
    public override void CollectObservations(VectorSensor sensor)
    {

    }
    public override void OnActionReceived(float[] vectorAction)
    {
        float moveDirection = vectorAction[0];
        
        transform.position += new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime;
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[1] = Input.GetAxis("Horizontal");
        actionsOut[0] = Input.GetAxis("Vertical");
    }
}
*/