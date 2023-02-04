using System.Collections;
using House.ProjectileTargets;
using UnityEngine;


namespace Player.Shoot
{
    [RequireComponent(typeof(Rigidbody))]
    public class ArrowProjectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed;
        [SerializeField] private GameObject ready;
        [SerializeField] private GameObject used;
        [SerializeField] private float wait = 10.0f;
        [SerializeField] private AudioSource popSoundEffect;

        
        private Transform _player;
        private Rigidbody _rigidbody;
        private bool _isGlued = false;

        private void Awake()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _rigidbody.AddRelativeForce(Vector3.forward * projectileSpeed, ForceMode.Impulse);
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (_isGlued) return;
            if (collision.gameObject.CompareTag("Player")) return;
            _isGlued = true;
            used.SetActive(true);
            ready.SetActive(false);
            popSoundEffect.Play();
            
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            Transform currentTransform = gameObject.transform;
            currentTransform.position = collision.GetContact(0).point;
            currentTransform.rotation = Quaternion.LookRotation(-collision.GetContact(0).normal);
            currentTransform.parent = collision.gameObject.transform;
            if (collision.gameObject.CompareTag("Item"))
            {
                AProjectileTarget projectileTarget = collision.gameObject.GetComponent<AProjectileTarget>();
                projectileTarget.Activate();
                return;
            }
            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            yield return new WaitForSeconds(wait);
            Destroy(gameObject);
        }
    
    }
}