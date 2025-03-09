using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterManager : MonoBehaviour
{
    /*public static int isRegistered = 0;
    public TMP_InputField inputField;
    public DBManager DBmanager;
    public GameObject registerPanel;
    public GameObject menuPanel;

    private void Awake()
    {
        isRegistered = PlayerPrefs.GetInt("isRegistered", 0);

        Debug.Log(isRegistered);
        if(isRegistered == 0)
        {
            // Enter username the first time
            registerPanel.SetActive(true);
            menuPanel.SetActive(false);
        }
        else
        {
            registerPanel.SetActive(false);
            menuPanel.SetActive(true);
        }
    }

    public void RegisterUser()
    {
        PlayerPrefs.SetInt("isRegistered", 1);
        PlayerPrefs.SetString("username", inputField.text);
        PlayerPrefs.SetFloat("score", 0);
        DBmanager.CreateUser(inputField.text, 0);
    }*/
}
