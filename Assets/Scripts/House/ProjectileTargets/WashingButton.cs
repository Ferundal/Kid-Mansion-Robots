using System;
using AI;
using UnityEngine;

namespace House.ProjectileTargets
{
    public class WashingButton : AProjectileTarget
    {
//        [SerializeField] private float pressTime = 0.3f;
        [SerializeField] private Transform button;
        [SerializeField] private Washer washer;


        private float _moveOffset = 0.05f;

        public override void Activate()
        {
            Vector3 newButtonPosition = button.localPosition;
            newButtonPosition.y -= _moveOffset;
            button.localPosition = newButtonPosition;
            washer.ReactToWashStart();
            enabled = false;
        }
    }
}
