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
        private Transform _startingPosition;
        private Rigidbody _rigidbody;
        [SerializeField] private Waiter waiter;

        private void Awake()
        {
            _startingPosition = gameObject.transform;
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        public override void Activate()
        {
            waiter.AddFixFallenItemTask(_startingPosition, _rigidbody);
        }
    }
}
