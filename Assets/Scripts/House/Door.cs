using System;
using UnityEngine;

namespace House
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour
    {
        [field: SerializeField]
        public bool IsClosed { set; get; } = false;
        private Animator _animator;
        private static readonly int IsPlayerNear = Animator.StringToHash("IsPlayerNear");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (IsClosed || !other.CompareTag("Player")) return;
            _animator.SetBool(IsPlayerNear, true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _animator.SetBool(IsPlayerNear, false);
        }
    }
}
