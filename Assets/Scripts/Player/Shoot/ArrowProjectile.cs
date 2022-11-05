using System.Collections;
using UnityEngine;

namespace Player.Shoot
{
    public class ArrowProjectile : MonoBehaviour
    {
        public float force;
        private Rigidbody _rigidbodyComponent;
        private Transform _player;

        [SerializeField] private GameObject ready;
        [SerializeField] private GameObject used;
        [SerializeField] private float wait = 10.0f;
    
        private void Awake()
        {
            _rigidbodyComponent = GetComponent<Rigidbody>();
            _rigidbodyComponent.centerOfMass = transform.position;
        }

        public void Fire(Transform player)
        {
            _player = player;
            used.SetActive(false);
            ready.SetActive(true);
            _rigidbodyComponent.AddForce(transform.forward * (force * Random.Range(1.3f, 1.7f)), ForceMode.Impulse);
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name != "Player")
            {
                used.SetActive(true);
                ready.SetActive(false);
                _rigidbodyComponent.isKinematic = true;
            
                if (collision.gameObject.CompareTag("Item") && collision.gameObject.GetComponent<Rigidbody>())
                {

                    Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                
                    _rigidbodyComponent.isKinematic = false;
                    rb.velocity = (collision.gameObject.transform.position - _player.transform.position).normalized * -1.5f;
                }
            
                StartCoroutine(Countdown());
            }
        }

        private IEnumerator Countdown()
        {
            yield return new WaitForSeconds(wait);
            Destroy(gameObject);
        }
    
    }
}