using System.Collections;
using UnityEngine;

namespace UI.Fade
{
    public abstract class AFadeScreen : MonoBehaviour
    {
        public abstract IEnumerator Fade(bool isFadeOut);
    }
}
