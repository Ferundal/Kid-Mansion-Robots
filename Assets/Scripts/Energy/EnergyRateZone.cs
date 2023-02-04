using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnergyRateZone : MonoBehaviour
{
    [SerializeField] private float rateSpeedModificator = 10.0f;
    private GameManager _gameManager;
    private BoxCollider _collider;
    // Start is called before the first frame update
    private void Awake()
    {
        _gameManager = GameManager.FindObjectOfType<GameManager>();
        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _gameManager.ChangeEnergyRate(rateSpeedModificator);
    }

    private void OnTriggerExit(Collider other)
    {
        _gameManager.ChangeEnergyRate(1/rateSpeedModificator);
    }
}
