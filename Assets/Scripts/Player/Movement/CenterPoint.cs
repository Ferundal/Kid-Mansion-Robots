using Player;
using UnityEngine;


public class CenterPoint : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float maxSearchDistance = 500.0f;
    [SerializeField] private SelectCamera selectCamera;

    [SerializeField] private LayerMask layerToHit;

    [SerializeField] private Transform defaultTransform;
    [SerializeField] private float movementStep;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (!selectCamera.mouseButtonIsPressed)
        {
            gameObject.transform.position = defaultTransform.position;
            return;
        }

        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out var hit, maxSearchDistance, layerToHit))
        {
            if (hit.collider)
            {
                //gameObject.transform.position = hit.point;
                transform.position = hit.point;
            }
        }
        else
        {
            gameObject.transform.position = mainCamera.transform.position;
            gameObject.transform.Translate(Vector3.forward * maxSearchDistance, Space.Self);
        }
    }
}
