using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContactsPage : MonoBehaviour
{
    public Text nameText;
    public Text trustText;

    public Image deleteButtonImage;
    public Color deleteButtonColor;
    public Text deleteText;


    // Update is called once per frame
    public void UpdatePage(Contacts contacts)
    {
        nameText.text = contacts.data.name;
        trustText.text = "Trust: " + contacts.trust;
        //if trust is enough, Delete becom sacrifice
        if(contacts.trust > SystemManager.instance.contactsManager.trustToSacrifice)
        {
            deleteButtonImage.color = Color.red;
            deleteText.text = "Sacrifice";
        }else{
            deleteButtonImage.color = deleteButtonColor;
            deleteText.text = "Delete";
        }
    }
}
