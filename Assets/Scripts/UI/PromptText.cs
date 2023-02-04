using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PromptText : MonoBehaviour
    {
        [TextArea(15,20)]
        [SerializeField] private string promptText;
        [SerializeField] private float promptTime = 5.0f;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Fire"))
            {
                MessageManager messageManager = FindObjectOfType<MessageManager>();
                messageManager.SetNewPrompt(new KeyValuePair<string, float>(promptText, promptTime));
                Destroy(gameObject);
            }
        }
    }
}