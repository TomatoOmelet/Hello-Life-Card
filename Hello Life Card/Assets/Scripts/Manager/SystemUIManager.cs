using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class SystemUIManager : MonoBehaviour
{
    public TextMeshProUGUI intelligenceText;


    //update UI elements
    public void UpdateIntelligenceUI(int intelligence)
    {
        intelligenceText.text =  intelligence + " IQ";
    }
}
