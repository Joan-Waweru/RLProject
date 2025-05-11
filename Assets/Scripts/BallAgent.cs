using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using TMPro;
using System.Collections.Generic;

public class BallAgent : Agent
{
    private Rigidbody rBody;

    [Header("Agent References")]
    public TMP_Text rewardText;  // Assign in Inspector
    public Transform target;     // Assign the green cube

    [Header("Movement Settings")]
    public float speed = 20f;

    [Header("Obstacle Settings")]
    public GameObject obstaclePrefab;
    private List<GameObject> spawnedObstacles = new();

    // Optional: for reward shaping
    private float previousDistance;

    public int minObstacles = 2;
    public int maxObstacles = 3;



    void Start()
    {
        rBody = GetComponent<Rigidbody>();

    var areaManager = GetComponentInParent<TrainingAreaManager>();
    if (areaManager != null)
    {
        areaManager.ConfigureAgent(this);
    }
        if (rBody == null)
        {
            Debug.LogError("Rigidbody not found on the agent!");
        }
    }

    public override void OnEpisodeBegin()
    {
        // Reset agent physics
        rBody.angularVelocity = Vector3.zero;
        rBody.linearVelocity = Vector3.zero;
        this.transform.localPosition = new Vector3(-9f, 0.5f, 0f);

        // Reset target to random location
        target.localPosition = new Vector3(
            Random.Range(12f, 20f),
            Random.Range(0.5f, 3f),
            Random.Range(-5f, 5f)
        );

        // Reset previous distance for reward shaping
        previousDistance = Vector3.Distance(this.transform.localPosition, target.localPosition);

        // Clear previously spawned obstacles
        foreach (var obj in spawnedObstacles)
        {
            Destroy(obj);
        }
        spawnedObstacles.Clear();

        // Randomly spawn 1–3 obstacles
int obstacleCount = Random.Range(minObstacles, maxObstacles + 1);
        int attempts = 0;

        for (int i = 0; i < obstacleCount; i++)
        {
            bool placed = false;
            while (!placed && attempts < 15)
            {
                attempts++;

                Vector3 randomPos = new Vector3(
                    Random.Range(-5f, 5f),
                    0.5f,
                    Random.Range(-4f, 4f)
                );

                bool overlap = false;
                foreach (var existing in spawnedObstacles)
                {
                    if (Vector3.Distance(existing.transform.position, randomPos) < 1.5f)
                    {
                        overlap = true;
                        break;
                    }
                }

                if (!overlap)
                {
                    GameObject newObstacle = Instantiate(obstaclePrefab, randomPos, Quaternion.identity, transform.parent);
                    spawnedObstacles.Add(newObstacle);
                    placed = true;
                }
            }
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(rBody.linearVelocity);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;
        var continuousActions = actionBuffers.ContinuousActions;

        // X and Z movement
        controlSignal.x = continuousActions[0];
        controlSignal.z = -continuousActions[1];

        // Uncomment this section to add jumping (third action)
        /*
        float jumpSignal = continuousActions[2];
        if (this.transform.localPosition.y <= 0.51f && jumpSignal > 0.5f)
        {
            rBody.AddForce(Vector3.up * jumpSignal * 300f);
        }
        */

        rBody.AddForce(controlSignal * speed);

        // Reward shaping
        float currentDistance = Vector3.Distance(this.transform.localPosition, target.localPosition);
        float distanceDelta = previousDistance - currentDistance;
        AddReward(distanceDelta * 0.01f);
        previousDistance = currentDistance;

        // Fail condition: agent fell off
        if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }

        // Show cumulative reward
        if (rewardText != null)
        {
            rewardText.text = "Reward: " + GetCumulativeReward().ToString("F2");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            SetReward(1.0f);
            EndEpisode();
        }

        // Optional: penalty for hitting obstacles
        /*
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AddReward(-0.2f);
        }
        */
    }

    // public override void Heuristic(in ActionBuffers actionsOut)
    // {
    //     var continuousActionsOut = actionsOut.ContinuousActions;
    //     continuousActionsOut[0] = Input.GetAxis("Vertical");
    //     continuousActionsOut[1] = Input.GetAxis("Horizontal");
    //     // continuousActionsOut[2] = Input.GetKey(KeyCode.Space) ? 1f : 0f; // Optional jump
    // }
}