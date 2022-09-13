using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotControllerSam : MonoBehaviour
{
    [SerializeField] private List<GameObject> pathArray;
    [SerializeField] private float precision = 0.2f;

    private NavMeshAgent _agent;
    private int _i = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(pathArray[_i].transform.position);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (Math.Abs(transform.position.x - pathArray[_i].transform.position.x) < precision && Math.Abs(transform.position.z - pathArray[_i].transform.position.z) < precision)
        {
            if (++_i >= pathArray.Count)
            {
                _i = 0;
            }
            
            PathGeneration(pathArray[_i].transform.position);
        }
    }

    private void PathGeneration(Vector3 point)
    {
        _agent.SetDestination(point);
        
        if (Debug.isDebugBuild)
            Debug.Log("Next Point: " + point);
    }
}
