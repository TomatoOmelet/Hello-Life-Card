﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactsUIManager : MonoBehaviour
{
    public GameObject contactPanel;
    public GameObject[] contactsPages;
    // Start is called before the first frame update
    public void UpdateContacts()
    {
        int contactsNum = SystemManager.instance.contactsManager.contactsList.Count;
        //update page with contacts' info
        for(int x = 0; x< contactsNum; ++x)
        {
            contactsPages[x].SetActive(true);
            contactsPages[x].GetComponent<ContactsPage>().UpdatePage(SystemManager.instance.contactsManager.contactsList[x]);
        }
        //hide the following slots
        for(int x = contactsNum; x< contactsPages.Length; ++x)
        {
            contactsPages[x].SetActive(false);
        }
    }

    public void OpenContactPanel()
    {
        contactPanel.SetActive(true);
        UpdateContacts();
    }

    public void CloseContactPanel()
    {
        contactPanel.SetActive(false);
    }
}
