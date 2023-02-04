using System;
using System.Collections;
using UnityEngine;

namespace Player.Shoot
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float reloadTime = 1.0f;
        [SerializeField] private Transform projectileStartTransform;
        private bool _isReloading = false;
        private Laser _laser;

        private void Awake()
        {
            _laser = GetComponentInChildren<Laser>();
        }

        private void Start()
        {
            _laser.gameObject.SetActive(false);
        }

        public bool FireProjectile()
        {
            if (_isReloading) return false;
            _isReloading = true;
            Instantiate(projectilePrefab, projectileStartTransform.position, projectileStartTransform.rotation);
            StartCoroutine(Reload());
            return true;
        }

        public void SetLaser(bool isActive)
        {
            _laser.gameObject.SetActive(isActive);
        }
        private IEnumerator Reload()
        {
            yield return new WaitForSeconds(reloadTime);
            _isReloading = false;
        }
    }
}