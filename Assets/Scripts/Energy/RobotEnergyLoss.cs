using System;
using System.Collections;
using AI;
using UnityEngine;

namespace Energy
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Roomba))]
    public class RobotEnergyLoss : MonoBehaviour
    {
        [SerializeField] private float energyLossAmount = 20.0f;
        [SerializeField] private float cooldownTime = 3.0f;
        private bool _onCooldown = false;
        private GameManager _gameManager;
        private Roomba _roomba;

        private void Awake()
        {
            _gameManager = GameManager.FindObjectOfType<GameManager>();
            _roomba = GetComponent<Roomba>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_onCooldown || !other.gameObject.CompareTag("Player")) return;
            _onCooldown = true;
            _roomba.GoAway(other.gameObject);
            _gameManager.Energy -= energyLossAmount;
            StartCoroutine(ResetCooldown());
        }
        
        private IEnumerator ResetCooldown()
        {
            yield return new WaitForSeconds(cooldownTime);
            _onCooldown = false;
        }
    }
}