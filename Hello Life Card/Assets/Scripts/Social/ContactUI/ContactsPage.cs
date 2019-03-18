using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContactsPage : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI trustText;
    public TextMeshProUGUI jobText;

    public GameObject sacrificeButton;


    // Update is called once per frame
    public void UpdatePage(Contacts contacts)
    {
        nameText.text = contacts.data.name;
        trustText.text = "Trust: " + contacts.trust + "/" + SystemManager.instance.contactsManager.trustToSacrifice;
        jobText.text = "Work as: " + contacts.data.job.jobname;
        //if trust is enough, Delete becom sacrifice
        if(contacts.trust >= SystemManager.instance.contactsManager.trustToSacrifice)
        {
            sacrificeButton.SetActive(true);
        }else{
            sacrificeButton.SetActive(false);
        }
    }
}
