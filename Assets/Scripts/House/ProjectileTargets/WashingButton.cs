using UnityEngine;

namespace House.ProjectileTargets
{
    public class WashingButton : MonoBehaviour, IProjectileTarget
    {
//        [SerializeField] private float pressTime = 0.3f;
        [SerializeField] private Transform button;

        private bool _isPressed = false;
        private float _moveOffset = 0.05f;

        public void Activate()
        {
            _isPressed = true;
            Vector3 newButtonPosition = button.localPosition;
            newButtonPosition.y -= _moveOffset;
            button.localPosition = newButtonPosition;
        }
    }
}
