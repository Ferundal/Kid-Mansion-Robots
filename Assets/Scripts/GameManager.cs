using System.Collections;
using StarterAssets;
using UI;
using UI.Fade;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class GameManager : MonoBehaviour
{
    private const float energyMinValue = 0.0f;
    private const float energyMaxValue = 100.0f;
    [SerializeField] private ThirdPersonController thirdPersonController;
    [SerializeField] private PlayerInput playerInput;
    
    [Header("Energy")]
    [SerializeField] private AEnergyView energyView;
    [SerializeField][Range(energyMinValue, energyMaxValue)] private float energy = energyMaxValue;
    [SerializeField] private AFadeScreen fadeScreen;

    private bool _isPlayerActive = true;

    public float Energy
    {
        get => energy;
        set
        {
            Debug.Log(value);
            switch (value)
            {
                case > energyMaxValue:
                    energy = energyMaxValue;
                    break;
                case < energyMinValue:
                    energy = energyMinValue;
                    break;
                default:
                    energy = value;
                    break;
            }
            _startEnergy = energy;
            _startTime = Time.time;
        }
    }
    [SerializeField][Range(0, 50)] private float startEnergyRate = 1.0f;
    private float _energyRate;
    private bool _isMoving;
    private float _startTime;
    private float _startEnergy;
    
    private void Awake()
    {
        _energyRate = startEnergyRate;
        energyView.Energy = (int)energy;
    }

    private void Update()
    {
        if (!_isPlayerActive)
        {
            return;
        }

        if (thirdPersonController.isStop)
        {
            if (_isMoving)
                _isMoving = false;
        }
        else
        {
            if (_isMoving)
            {
                energy = _startEnergy - (Time.time - _startTime) * _energyRate;
            }
            else
            {
                _isMoving = true;
                _startEnergy = energy;
                _startTime = Time.time;
            }
        }
        if (energy <= 0)
        {
            StartCoroutine(ReturnToRoom());
        }
        energyView.Energy = energy;
    }
    
    public void ChangeEnergyRate(float energyRateModificator)
    {
        if (_isMoving)
        {
            _startEnergy = energy;
            _startTime = Time.time;
        }
        _energyRate *= energyRateModificator;
    }

    public void EnableUserControl(bool isEnabled)
    {
        if (isEnabled)
            playerInput.actions.Enable();
        else
            playerInput.actions.Disable();
    }
    
    public IEnumerator ReturnToRoom()
    {
        _isPlayerActive = false;
        yield return StartCoroutine(fadeScreen.Fade(true));
        EnableUserControl(true);
        thirdPersonController.controller.enabled = false;
        thirdPersonController.gameObject.transform.position = thirdPersonController.spawnPoint.transform.position;
        thirdPersonController.gameObject.transform.rotation = thirdPersonController.spawnPoint.transform.rotation;
        thirdPersonController.controller.enabled = true;
        yield return StartCoroutine(fadeScreen.Fade(false));
        EnableUserControl(false);
        _energyRate = startEnergyRate;
        _isMoving = false;
        _isPlayerActive = true;
        energy = energyMaxValue;
    }
    
    
}
