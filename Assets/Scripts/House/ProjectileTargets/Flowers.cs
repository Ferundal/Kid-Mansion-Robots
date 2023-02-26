using System;
using AI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace House.ProjectileTargets
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    
    public class Flowers : AProjectileTarget
    {
        private GameObject _startingPosition;
        private Rigidbody _rigidbody;
        [SerializeField] private Waiter waiter;

        private void Awake()
        {
            _startingPosition = new GameObject();
            _startingPosition.transform.position = gameObject.transform.position;
            _startingPosition.transform.rotation = gameObject.transform.rotation;
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        public override void Activate()
        {
            waiter.AddFixFallenItemTask(_startingPosition.transform, _rigidbody);
        }
    }
}
