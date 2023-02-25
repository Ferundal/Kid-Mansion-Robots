using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace AI
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Waiter : MonoBehaviour
    {
        [FormerlySerializedAs("_pathArray")]
        [Header("Path options")]
        [SerializeField] private List<GameObject> pathArray;
        [SerializeField] private float _precision = 0.2f;
        [Header("Work Stop Points")]
        [SerializeField] private string _workPointTag = "Work_Point";
        [SerializeField] private float _workTime = 5.0f;
#if UNITY_EDITOR
        [Header("Editor visualization")]
        [SerializeField] private float _sphereVerticalOffset = 0.2f;
        [SerializeField] private float _sphereRadius = 0.2f;
#endif
        [Header("Player Detection")]
        [SerializeField] private GameObject _player;
        [SerializeField] private List<GameObject> _waiterAlarmMarkers;
        
        [SerializeField][Range(-5, 5)] private float _verticalOffset = 1.0f;
        [SerializeField][Range(0, 5)] private float _rayOffset = 1.0f;


        private GameManager _gameManager;
        
        private bool _isWorking = false;
        private bool _isFixing = false;
        private bool _isPlayerSpoted = false;

        private GameObject _currentTarget;
        
        private NavMeshAgent _agent;
        private int _i = 0;
        private Vector3 _rayBaseDirection = Vector3.forward * 1000;
        private Vector3 _rayLeftPositionOffset;
        private Vector3 _rayRightPositionOffset;
        private RaycastHit _rightHit;
        private RaycastHit _leftHit;

        private Queue<KeyValuePair<Transform, Rigidbody>> _taskQueue;

        public void AddFixFallenItemTask(Transform startPosition, Rigidbody movedObject)
        {
            _isFixing = true;
            _taskQueue.Enqueue(new KeyValuePair<Transform, Rigidbody>(startPosition, movedObject));
        }
        
        // Start is called before the first frame update
        private void Awake()
        {
            _gameManager = GameManager.FindObjectOfType<GameManager>();
            _agent = GetComponent<NavMeshAgent>();
            _agent.SetDestination(pathArray[_i].transform.position);
            _rayRightPositionOffset = new Vector3(_rayOffset, _verticalOffset, 0);
            _rayLeftPositionOffset = new Vector3(-_rayOffset, _verticalOffset, 0);
            foreach(GameObject waiterAlarmMarker in _waiterAlarmMarkers)
            {
                waiterAlarmMarker.SetActive(false);
            }

            _taskQueue = new Queue<KeyValuePair<Transform, Rigidbody>>();
        }
        
        private void LateUpdate()
        {
            if (_isPlayerSpoted || _isWorking) return;

            if (Math.Abs(transform.position.x - _currentTarget.transform.position.x) < _precision 
                && Math.Abs(transform.position.z - _currentTarget.transform.position.z) < _precision)
            {
                if (_isFixing)
                {
                    var task = _taskQueue.Dequeue();
                    task.Value.position = task.Key.position;
                    task.Value.rotation = task.Key.rotation;
                    if (_taskQueue.Count == 0)
                    {
                        _isFixing = false;
                    }
                    else
                    {
                        
                    }
                }
                if (_currentTarget.gameObject.CompareTag(_workPointTag))
                {
                    _isWorking = true;
                    StartCoroutine(WorkCoorutine());
                }
                else
                {
                    GoToNextPoint();
                }
            }
            if (Physics.Raycast(transform.position + transform.rotation * _rayRightPositionOffset, 
                    transform.rotation * _rayBaseDirection, out _rightHit)
                && Physics.Raycast(transform.position + transform.rotation * _rayLeftPositionOffset, 
                    transform.rotation * _rayBaseDirection, out _leftHit)
                && _rightHit.collider.gameObject == _player 
                && _leftHit.collider.gameObject == _player)
            {
                PlayerDetected();
            }
        }

        IEnumerator WorkCoorutine()
        {
            yield return new WaitForSeconds(_workTime);
            GoToNextPoint();
            _isWorking = false;
        }

#if UNITY_EDITOR
        private bool _isDetectRaySet = false;
        
        void OnDrawGizmos()
        {
            if (!_isDetectRaySet)
            {
                _isDetectRaySet = true;
                _rayRightPositionOffset = new Vector3(_rayOffset, _verticalOffset, 0);
                _rayLeftPositionOffset = new Vector3(-_rayOffset, _verticalOffset, 0);
            }
            else
            {
                _rayRightPositionOffset.x = _rayOffset;
                _rayRightPositionOffset.y = _verticalOffset;
                _rayLeftPositionOffset.x = -_rayOffset;
                _rayLeftPositionOffset.y = _verticalOffset;
            }
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
                        currentSpherePositin.y = pathPoint.transform.position.y + _sphereVerticalOffset;
                        currentSpherePositin.z = pathPoint.transform.position.z;
                    }
                    else
                    {
                        previusSpherePositin.x = currentSpherePositin.x;
                        previusSpherePositin.y = currentSpherePositin.y;
                        previusSpherePositin.z = currentSpherePositin.z;

                        currentSpherePositin.x = pathPoint.transform.position.x;
                        currentSpherePositin.y = pathPoint.transform.position.y + _sphereVerticalOffset;
                        currentSpherePositin.z = pathPoint.transform.position.z;
                        Gizmos.DrawLine(previusSpherePositin, currentSpherePositin);
                        if (lastPoint != firstPoint)
                        {
                            isMoreThanTwoPoints = true;
                        }
                        lastPoint = pathPoint;
                    }
                    if (pathPoint.gameObject.CompareTag(_workPointTag))
                    {
                        Gizmos.color = Color.yellow;
                        Gizmos.DrawSphere(currentSpherePositin, _sphereRadius);
                        Gizmos.color = Color.green;
                    }
                    else
                    {
                        Gizmos.DrawSphere(currentSpherePositin, _sphereRadius);
                    }
                }
            }
            if (isMoreThanTwoPoints)
            {
                previusSpherePositin.x = firstPoint.transform.position.x;
                previusSpherePositin.y = firstPoint.transform.position.y + _sphereVerticalOffset;
                previusSpherePositin.z = firstPoint.transform.position.z;
                Gizmos.DrawLine(previusSpherePositin, currentSpherePositin);
            }
            Gizmos.DrawRay(transform.position + transform.rotation * _rayRightPositionOffset, transform.rotation * _rayBaseDirection);
            Gizmos.DrawRay(transform.position + transform.rotation * _rayLeftPositionOffset, transform.rotation * _rayBaseDirection);
        }
#endif
        
        
        private void GoToNextPoint()
        {
            if (++_i >= pathArray.Count)
            {
                _i = 0;
            }

            PathGeneration(pathArray[_i].transform.position);
        }

        private void PathGeneration(Vector3 point)
        {
            _agent.SetDestination(point);
        }

        private void PlayerDetected()
        {
            _isPlayerSpoted = true;
            _agent.SetDestination(_player.transform.position);
            foreach (GameObject waiterAlarmMarker in _waiterAlarmMarkers)
            {
                waiterAlarmMarker.SetActive(true);
            }
            _gameManager.EnableUserControl(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _agent.isStopped = true;
                StartCoroutine(_gameManager.ReturnToRoom());
            }
        }
    }
}
