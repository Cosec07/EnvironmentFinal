using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Policies;

public class ShipFollowPath : MonoBehaviour

{
    //[SerializeField] private wayPoint waypoints;
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] private GameObject boat;
    [SerializeField] private float distanceThreshold = 0.1f;
    private Transform currentWaypoint = null;
    private int islandNumber;
    private bool controlShift = false;

    [SerializeField] private Transform destinationIsland;
    //[SerializeField] private Transform islandTransform2;
    //[SerializeField] private Transform islandTransform3;
    //[SerializeField] private Transform islandTransform4;

    //public bool hit = false;
    public sphereRadius sphereradius;
    // Start is called before the first frame update
    void Start()
    {

        islandNumber = Random.Range(1, 4);
        Debug.Log("Going to island" + islandNumber);
        currentWaypoint = GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;
        currentWaypoint = GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
    }
    // Update is called once per frame
    void Update()
    {

        //Debug.Log(sphereradius.hit);
        BehaviorParameters behaviorParameters;
        behaviorParameters = boat.GetComponent<BehaviorParameters>();
        if (sphereradius.hit == false)
        {  
            behaviorParameters.BehaviorType = BehaviorType.HeuristicOnly;
            if (controlShift == true)
            {
                Transform nearestWaypoint = null;
                float minDistance = float.MaxValue;

                foreach (Transform child in destinationIsland)
                {
                    float distance = Vector3.Distance(transform.position, child.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestWaypoint = child;
                    }

                }
                transform.position = Vector3.MoveTowards(transform.position, nearestWaypoint.position, moveSpeed * Time.deltaTime);
                currentWaypoint = GetNextWaypoint(nearestWaypoint);
                transform.LookAt(currentWaypoint);
                controlShift = false;
                /*if (Vector3.Distance(transform.position, nearestWaypoint.position) < distanceThreshold)
                {
                    currentWaypoint = GetNextWaypoint(nearestWaypoint);
                    transform.LookAt(currentWaypoint);
                }*/
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
                {
                    currentWaypoint = GetNextWaypoint(currentWaypoint);
                    transform.LookAt(currentWaypoint);
                }
            }
        }
        else
        {
            controlShift = true;
            behaviorParameters.BehaviorType = BehaviorType.InferenceOnly;
            Debug.Log(controlShift);
        }
    }

    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        if (currentWaypoint == null)
        {
            return destinationIsland.GetChild(0);
        }

        if (currentWaypoint.GetSiblingIndex() < destinationIsland.childCount - 1)
        {
            return destinationIsland.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
        else
        {
            return currentWaypoint;
        }

    }
}