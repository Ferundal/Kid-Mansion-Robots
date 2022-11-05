using System.Collections;
using System.Collections.Generic;
using Player.Shoot;
using UnityEngine;
using UnityEngine.UI;

public class SelectCamera : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject aimCamera;
    public GameObject aimReticle;

    public GameObject player;
    public Image crossHair;

    public GameObject look;
    public GameObject arrowPrefab;
    public Transform fireTransform;

    public bool mouseButtonIsPressed = false;

    private bool mouseFire = false;
    
    // Start is called before the first frame update
    void Start()
    {
        aimCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit _raycastHit;
        Vector3 fwd = look.transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(look.transform.position, fwd, out _raycastHit))
        {
            if (_raycastHit.transform.CompareTag("Item"))
            {
                crossHair.color = Color.green;
            }
            else
            {
                crossHair.color = Color.white;
            }
        }

        if (Input.GetKeyDown("mouse 1") && !mouseButtonIsPressed)
        {
            mouseButtonIsPressed = true;

            // aimCamera.transform.position = mainCamera.transform.position;
            // aimCamera.transform.rotation = mainCamera.transform.rotation;
            
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
            
            StartCoroutine(ShowReticle());
        }
        else if (Input.GetKeyDown("mouse 1") && mouseButtonIsPressed)
        {
            mouseButtonIsPressed = false;
            
            // mainCamera.transform.position = aimCamera.transform.position;
            // mainCamera.transform.rotation = aimCamera.transform.rotation;
            
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
            aimReticle.SetActive(false);
        }
        
        if (mouseButtonIsPressed && Input.GetKeyDown("mouse 0"))
        {
            StartCoroutine(FireArrow());
        }
    }
    
    IEnumerator FireArrow()
    {
        if (mouseFire)
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        GameObject projectile = Instantiate(arrowPrefab);
        mouseFire = true;

        projectile.transform.forward = look.transform.forward;
        projectile.transform.position = fireTransform.position + fireTransform.forward;
        
        
        //Wait for the position to update
        yield return new WaitForSeconds(0.05f);

        projectile.GetComponent<ArrowProjectile>().Fire(player.transform);
        mouseFire = false;
    }
    
    IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(0.25f);
        aimReticle.SetActive(enabled);
    }
}
