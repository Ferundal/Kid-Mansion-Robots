using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PromptManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text promptText;
        
        private List<string> _allPrompts;

        private void Awake()
        {
            _allPrompts = new List<string>();
        }

        public void GetNewPrompt(string newPrompt)
        {
            _allPrompts.Add(newPrompt);
            promptText.SetText(newPrompt);
        }
    }
}
