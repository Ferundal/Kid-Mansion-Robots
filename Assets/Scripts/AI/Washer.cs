using System;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Animator))]
    public class Washer : MonoBehaviour
    {

        private Animator _animator;
        private static readonly int IsTryingToWash = Animator.StringToHash("isTryingToWash");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void ReactToWashStart()
        {
            _animator.SetBool(IsTryingToWash, false);
            Debug.Log("WashIsStarted");
        }
    }
}
