using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public DBManager DBManager;
    public TMP_InputField usernameTMP;
    public TMP_InputField emailTMP;
    public TMP_InputField passwordTMP;

    public async void SignInPlayer()
    {
        string email = emailTMP.text;
        string password = passwordTMP.text;

        bool success = await DBManager.Instance.SignIn(email, password);
        if (success)
        {
            ScenesManager.LoadScene(1);
        }
    }

    public async void SignUpPlayer()
    {
        string username = usernameTMP.text;
        string email = emailTMP.text;
        string password = passwordTMP.text;
        
        bool success = await DBManager.Instance.SignUp(username, email, password);
        if (success)
        {
            ScenesManager.LoadScene(1);
        }
    }

    public async void SignOutPlayer()
    {
        var success = await DBManager.Instance.SignOut();
    }

    public async void GetUnlockedCharacters()
    {
        var json = await DBManager.Instance.GetUnlockedCharacters();
    }

    public async void GetAllCharacters()
    {
        var list = await DBManager.Instance.GetAllCharacters();
    }

    public void SignInAnonymously()
    {
        ScenesManager.LoadScene(1);
    }

    public void PromoteUserPlayer()
    {
        DBManager.Instance.PromoteAnonymousUser(emailTMP.text, passwordTMP.text, usernameTMP.text, 4,5f);
    }

    public async void SignInWithDiscordPlayer()
    {
        var success = await DBManager.Instance.SignInWithDiscord();
    }

}
