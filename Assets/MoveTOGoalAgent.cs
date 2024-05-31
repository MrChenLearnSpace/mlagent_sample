using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class MoveTOGoalAgent : Agent
{
    // Start is called before the first frame update
    public override void OnActionReceived(ActionBuffers actions) {
        for (int i = 0; i < actions.ContinuousActions.Length; i++) {
            print("num " + i + " : " + actions.ContinuousActions[i]);
        }
    }
}
