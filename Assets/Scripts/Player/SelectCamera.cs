using System.Collections;
using Player.Shoot;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class SelectCamera : MonoBehaviour
    {
        public GameObject mainCamera;
        public GameObject aimCamera;

        public GameObject player;
    
        public GameObject playerBody;
        [SerializeField] private float aimRotationAngle;
        [SerializeField] private float aimRotationTime;
        private Vector3 _rotateAimOnEngle;
        private Vector3 _rotateAimOffEngle;
    
        [SerializeField] private Gun gun;
        [SerializeField] private Transform weaponPosition;

        public bool mouseButtonIsPressed = false;
        private bool _isWeaponChanging = false;

        private void Awake()
        {
            _rotateAimOnEngle = new Vector3(0, -aimRotationAngle, 0);
            _rotateAimOffEngle = -_rotateAimOnEngle;
            gun = Instantiate(gun, weaponPosition.position, weaponPosition.rotation, weaponPosition);
            gun.gameObject.SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {
            aimCamera.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown("mouse 1") && !mouseButtonIsPressed && !_isWeaponChanging)
            {
                StartCoroutine(WeaponOn());
            }
            else if (Input.GetKeyDown("mouse 1") && mouseButtonIsPressed && !_isWeaponChanging)
            {
                StartCoroutine(WeaponOff());
            }
        
            if (mouseButtonIsPressed && !_isWeaponChanging && Input.GetKeyDown("mouse 0"))
            {
                gun.FireProjectile();
            }
        }

        private IEnumerator WeaponOn()
        {
            _isWeaponChanging = true;
            gun.gameObject.SetActive(true);
        
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
        
            mouseButtonIsPressed = true;
        
            yield return new WaitForSeconds(aimRotationTime);
            
            playerBody.transform.Rotate(_rotateAimOnEngle);
            _isWeaponChanging = false;
            gun.SetLaser(true);
        }

        private IEnumerator WeaponOff()
        {
            _isWeaponChanging = true;
            gun.SetLaser(false);
        
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
        
            mouseButtonIsPressed = false;

            yield return new WaitForSeconds(aimRotationTime);
            
            playerBody.transform.Rotate(_rotateAimOffEngle);
            _isWeaponChanging = false;
            gun.gameObject.SetActive(false);
        }
    }
}
