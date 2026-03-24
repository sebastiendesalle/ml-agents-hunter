using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class HunterAgent : Agent
{
    public PredatorAgent predator;
    public EnvironmentManager environmentManager;
    public float speedMultiplier = 0.5f;
    public float rotationMultiplier = 5.0f;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-3f, 3f), 0.5f, Random.Range(-3f, 3f));
        transform.localRotation = Quaternion.identity;
        environmentManager.ResetEnvironment();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.z = actionBuffers.ContinuousActions[0];
        transform.Translate(controlSignal * speedMultiplier);
        transform.Rotate(0f, rotationMultiplier * actionBuffers.ContinuousActions[1], 0f);

        AddReward(-1f / MaxStep);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedBlock"))
        {
            AddReward(0.5f);
            Destroy(other.gameObject);

            if (environmentManager.CountRemainingBlocks() == 0)
            {
                AddReward(1.0f);
                predator.AddReward(-1.0f);
                predator.EndEpisode();
                EndEpisode();
            }
        }
        else if (other.CompareTag("Wall"))
        {
            AddReward(-0.5f);
            predator.AddReward(0.5f);
            predator.EndEpisode();
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuous = actionsOut.ContinuousActions;
        continuous[0] = Input.GetAxis("Vertical");
        continuous[1] = Input.GetAxis("Horizontal");
    }
}
