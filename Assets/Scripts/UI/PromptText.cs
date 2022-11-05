using UnityEngine;

namespace UI
{
    public class PromptText : MonoBehaviour
    {
        [TextArea(15,20)]
        [SerializeField] private string promptText;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Fire"))
            {
                PromptManager promptManager = FindObjectOfType<PromptManager>();
                promptManager.GetNewPrompt(promptText);
                Destroy(gameObject);
            }
        }
    }
}