using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMeshAgent))]
public class Roomba : MonoBehaviour
{
    [Header("Collision to player options")] [SerializeField]
    private float delayTime = 1.0f;
    
    [Header("Path options")]
    [SerializeField] private List<GameObject> pathArray;
    [SerializeField] private float precision = 0.2f;

    [Header("Editor visualization")]
    [SerializeField] private float sphereVerticalOffset = 0f;
    [SerializeField] private float sphereRadius = 0.2f;

    private NavMeshAgent _agent;
    private int _i = 0;
    private bool _isPatrolling = true;

    public void GoAway(GameObject avoidObject)
    {
        Debug.Log("ShouldStop");
        _isPatrolling = false;
        _agent.isStopped = true;
        StartCoroutine(FindNewPath(avoidObject));
    }

    // Start is called before the first frame update
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(pathArray[_i].transform.position);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_isPatrolling) return;
        if (!IsNear(pathArray[_i].transform.position)) return;
        if (++_i >= pathArray.Count) 
        { 
            _i = 0; 
        }
        PathGeneration(pathArray[_i].transform.position);
    }

    private bool IsNear(Vector3 point)
    {
        if (Math.Abs(transform.position.x - pathArray[_i].transform.position.x) < precision && Math.Abs(transform.position.z - pathArray[_i].transform.position.z) < precision)
            return true;
        return false;
    }
    
    private void PathGeneration(Vector3 point)
    {
        _agent.SetDestination(point);
    }

    private IEnumerator FindNewPath(GameObject avoidObject)
    {
        yield return new WaitForSeconds(delayTime);
        _isPatrolling = true;
        int prevIndex = --_i;
        if (prevIndex < 0)
            _i = pathArray.Count - 1;
        PathGeneration(pathArray[_i].transform.position);
    }
    
    

#if UNITY_EDITOR 
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.green;
        GameObject firstPoint = null;
        GameObject lastPoint = null;
        bool isMoreThanTwoPoints = false;
        Vector3 currentSpherePositin = new Vector3();
        Vector3 previusSpherePositin = new Vector3();
        foreach (GameObject pathPoint in pathArray)
        {
            if (pathPoint != null)
            {
                if (firstPoint == null)
                {
                    firstPoint = pathPoint;
                    currentSpherePositin.x = pathPoint.transform.position.x;
                    currentSpherePositin.y = pathPoint.transform.position.y + sphereVerticalOffset;
                    currentSpherePositin.z = pathPoint.transform.position.z;
                } else
                {
                    previusSpherePositin.x = currentSpherePositin.x;
                    previusSpherePositin.y = currentSpherePositin.y;
                    previusSpherePositin.z = currentSpherePositin.z;

                    currentSpherePositin.x = pathPoint.transform.position.x;
                    currentSpherePositin.y = pathPoint.transform.position.y + sphereVerticalOffset;
                    currentSpherePositin.z = pathPoint.transform.position.z;
                    Gizmos.DrawLine(previusSpherePositin, currentSpherePositin);
                    if (lastPoint != firstPoint)
                    {
                        isMoreThanTwoPoints = true;
                    }
                    lastPoint = pathPoint;
                }
                Gizmos.DrawSphere(currentSpherePositin, sphereRadius);
            }
        }
        if (isMoreThanTwoPoints)
        {
            previusSpherePositin.x = firstPoint.transform.position.x;
            previusSpherePositin.y = firstPoint.transform.position.y + sphereVerticalOffset;
            previusSpherePositin.z = firstPoint.transform.position.z;
            Gizmos.DrawLine(previusSpherePositin, currentSpherePositin);
        }
    }
#endif
}
