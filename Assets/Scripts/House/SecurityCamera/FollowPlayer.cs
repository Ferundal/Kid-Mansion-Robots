using UnityEngine;

namespace House.SecurityCamera
{
    public class FollowPlayer : MonoBehaviour
    {
        [HideInInspector] public bool isActive = true;
        [SerializeField] private Transform eyeTransform;
        private Transform _startEyeTransform;
        private GameObject _head;
        private Vector3 _offset;
        private void Awake()
        {
            _startEyeTransform = eyeTransform;
            _head = GameObject.FindWithTag("Head");
        }
    
        private void OnTriggerStay(Collider other)
        {
            if (isActive && other.gameObject.CompareTag("Player"))
            {
                eyeTransform.LookAt(_head.transform);
            }
        }
    }
}
