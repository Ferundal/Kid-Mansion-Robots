using System;
using UnityEngine;

namespace Player.Shoot
{
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : MonoBehaviour
    {
        [SerializeField] private LayerMask layerToHit;
        [SerializeField] private float maxSearchDistance = 500.0f;
        private LineRenderer _lineRenderer;
        private Vector3 _binEndPoint;


        private void Awake()
        {
            GameObject tmpGameObject;
            _lineRenderer = (tmpGameObject = this.gameObject).GetComponent<LineRenderer>();
            _lineRenderer.SetPosition(0, tmpGameObject.transform.position);
            _binEndPoint = Vector3.zero;
        }

        // Update is called once per frame
        void Update()
        {
            Transform tmpTransform = this.gameObject.transform;
            _lineRenderer.SetPosition(0, tmpTransform.position);

            RaycastHit hit;

            if (Physics.Raycast(tmpTransform.position, transform.forward, out hit, maxSearchDistance, layerToHit))
            {
                if (hit.collider)
                {
                    _lineRenderer.SetPosition(1, hit.point);
                }
            }

        }
    }
}
