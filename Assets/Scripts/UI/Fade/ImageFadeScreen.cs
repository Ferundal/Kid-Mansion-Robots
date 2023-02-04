using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Fade
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Animator))]
    public class ImageFadeScreen : AFadeScreen
    {
        private Animator _animator;
        private static readonly int IsFaded = Animator.StringToHash("isFaded");
        private bool _isAnimationFinished;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override IEnumerator Fade(bool isFadeOut)
        {
            _isAnimationFinished = false;
            _animator.SetBool(IsFaded, isFadeOut);
            yield return new WaitUntil(() => _isAnimationFinished);
        }


        public void AnimationFinished()
        {
            _isAnimationFinished = true;
        }
    }
}
