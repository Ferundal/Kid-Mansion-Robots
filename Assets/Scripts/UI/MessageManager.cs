using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class MessageManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text messageText;
        
        private LinkedList<KeyValuePair<string, float>> _messageQueue;
        
        private void Awake()
        {
            _messageQueue = new LinkedList<KeyValuePair<string, float>>();
            messageText.gameObject.SetActive(false);
        }

        public void SetNewPrompt(KeyValuePair<string, float> keyValuePair)
        {
            _messageQueue.AddFirst(keyValuePair);
            if (messageText.gameObject.activeSelf) return;
            StartCoroutine(ShowMessages());
        }

        private IEnumerator ShowMessages()
        {
            while (_messageQueue.Count > 0)
            {
                messageText.gameObject.SetActive(true);
                messageText.SetText(_messageQueue.Last.Value.Key);
                yield return new WaitForSeconds(_messageQueue.Last.Value.Value);
                _messageQueue.RemoveLast();
            }
            messageText.gameObject.SetActive(false);
        }
    }
}
