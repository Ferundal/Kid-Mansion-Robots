using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private ThirdPersonController thirdPersonController;
    
    [Header("Energy")]
    [SerializeField] private AEnergyView energyView;
    [SerializeField][Range(0, 100)] private float energy = 100.0f;
    [SerializeField][Range(0, 50)] private float startEnergyRate = 1.0f;
    private float _energyRate;
    private bool _isMoving = false;
    private float _startTime;
    private float _startEnergy;
    
    private void Awake()
    {
        _energyRate = startEnergyRate;
        energyView.Energy = (int)energy;
    }

    private void Update()
    {
        if (thirdPersonController.isStop)
        {
            if (_isMoving)
                _isMoving = false;
        }
        else
        {
            if (_isMoving)
            {
                energy = _startEnergy - (Time.time - _startTime) * _energyRate;
            }
            else
            {
                _isMoving = true;
                _startEnergy = energy;
                _startTime = Time.time;
            }
        }
        if (energy <= 0)
        {
            ReturnToRoom();
            energy = 100.0f;
        }
        energyView.Energy = energy;
    }

    public void ChangeEnergyRate(float energyRateModificator)
    {
        if (_isMoving)
        {
            _startEnergy = energy;
            _startTime = Time.time;
        }
        _energyRate *= energyRateModificator;
        Debug.Log($"Rate {_energyRate}");
    }

    
    private void ReturnToRoom()
    {
        thirdPersonController.controller.enabled = false;
        thirdPersonController.gameObject.transform.position = thirdPersonController.spawnPoint.transform.position;
        thirdPersonController.controller.enabled = true;
        _energyRate = startEnergyRate;
        _isMoving = false;
    }
}
