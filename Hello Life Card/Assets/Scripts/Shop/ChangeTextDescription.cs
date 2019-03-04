using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChangeTextDescription : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    public TextMeshProUGUI description;
    private void OnMouseOver()
    {
        Debug.Log("here");
        panel.SetActive(true);
        description.text = "hello";
    }

    private void OnMouseExit()
    {
        panel.SetActive(false);
    }
}
