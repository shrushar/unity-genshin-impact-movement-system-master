using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace GenshinImpactMovementSystem
{
    [Serializable]
    public class UICounter
    {
        public TextMeshProUGUI textComponent;

        public void SetCounter(int current, int all)
        {
            textComponent.text = current + "/" + all;
        }

    }
    
}
