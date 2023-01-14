using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [HideInInspector] public bool isActive = true;
    [SerializeField] private Transform eyeTransform;
    [SerializeField] private float highOffset = 1.0f;
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
        if (other.gameObject.CompareTag("Player"))
        {
            eyeTransform.LookAt(_head.transform);
            Debug.Log("Camera See you");
        }
    }
}
