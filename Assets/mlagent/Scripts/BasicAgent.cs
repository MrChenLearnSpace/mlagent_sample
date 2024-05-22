using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;
public class BasicAgent : Agent {
    public Transform goal;
    public MeshRenderer MeshRenderer;
    public EnvironmentParameters m_ResetParams;
    public Material winMaterial;
    public Material loseMaterial;

    public float lastDistance = float.MaxValue;
    // Start is called before the first frame update
    public override void Initialize() {
        m_ResetParams = Academy.Instance.EnvironmentParameters;
        SetResetParameters();
    }
    public override void OnEpisodeBegin() {
        SetResetParameters();
    }
    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(goal.localPosition);
        sensor.AddObservation(transform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actionBuffers) {
        var actionZ = 1f * Mathf.Clamp(actionBuffers.ContinuousActions[0], -1f, 1f);
        var actionX = 1f * Mathf.Clamp(actionBuffers.ContinuousActions[1], -1f, 1f);
        GetComponent<Rigidbody>().velocity += new Vector3(actionX, 0, actionZ);
        if (Vector3.Distance(transform.localPosition, goal.localPosition) < lastDistance) {
            SetReward(0.2f);
        }
        else {
            SetReward(-0.1f);
        }
        lastDistance = Vector3.Distance(transform.localPosition, goal.localPosition);
    }
    public override void Heuristic(in ActionBuffers actionsOut) {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = -Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
    public void SetResetParameters() {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.localPosition = new Vector3(0,1,0) ;
        goal.transform.position = new Vector3(Random.Range(-2.5f,2.5f),0,Random.Range(-2.5f, 2.5f))+ transform.position;
        lastDistance = float.MaxValue;
    }

    //private void OnTriggerStay(Collider other) {
    //    print("OnTriggerStay");
    //    if (other.tag == "goal") {
    //        SetReward(1f);
    //        EndEpisode();
    //    }
    //    if (other.tag == "wall") {
    //        SetReward(-1f);
    //        EndEpisode();
    //    }
    //}
    private void OnCollisionStay(Collision collision) {
        if (collision.collider.tag == "goal") {
            MeshRenderer.material = winMaterial;
            SetReward(1f);
            EndEpisode();
            print("OnCollisionStay");

        }
        if (collision.collider.tag == "wall") {
            MeshRenderer.material = loseMaterial;
            SetReward(-1f);
            EndEpisode();
            print("OnCollisionStay");

        }
    }
}
