using System;
using UnityEngine;


namespace Energy
{
    [RequireComponent(typeof(BoxCollider))]
    public class EnergyBuster : MonoBehaviour
    {
        [SerializeField] private float energyChangeAmount = 30.0f;
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GameManager.FindObjectOfType<GameManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _gameManager.Energy += energyChangeAmount;
            Destroy(gameObject);
        }
    }
}
