using TMPro;
using UnityEngine;

namespace UI
{
    public class NumericEnergyView : AEnergyView
    {
        [SerializeField] private TMP_Text testField;
        private int _energyValue;
        
        public override float Energy
        {
            set
            {
                var energyValue = (int)value;
                if (_energyValue == energyValue)
                    return;
                _energyValue = energyValue;
                testField.text = energyValue.ToString();
            }
        }
    }
}