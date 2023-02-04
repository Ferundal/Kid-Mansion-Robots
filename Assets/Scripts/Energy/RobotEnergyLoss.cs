using System.Collections;
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
            if (_onCooldown || !other.CompareTag("Player")) return;
            Debug.Log("RobotCollision");
            _roomba.GoAway(other.gameObject);
            _gameManager.Energy -= energyLossAmount;
            _onCooldown = true;
            StartCoroutine(ResetCooldown());
        }

        private IEnumerator ResetCooldown()
        {
            yield return new WaitForSeconds(cooldownTime);
            _onCooldown = false;
        }
    }
}