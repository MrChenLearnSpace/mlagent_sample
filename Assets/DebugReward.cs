using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugReward : MonoBehaviour
{
    public float moveReward = -0.1f;
    float lastMoveReward = 0;
    public List<Transform> scenes;
    

    // Update is called once per frame
    void Update()
    {
        if(lastMoveReward!= moveReward) {
            lastMoveReward = moveReward;
            foreach(Transform scene in scenes) {
                scene.Find("Agent").GetComponent<BasicAgent>().moveReward = moveReward;
            }
            lastMoveReward = moveReward;
        }
    }
}
