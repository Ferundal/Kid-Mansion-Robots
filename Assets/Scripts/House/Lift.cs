using System;
using UnityEngine;

namespace House
{
    public class Lift : MonoBehaviour
    {
        [SerializeField] private Transform exitPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            Transform playerTransform = other.gameObject.transform;
            playerTransform.position = exitPoint.position;
            playerTransform.rotation = exitPoint.rotation;
        }
    }
}
