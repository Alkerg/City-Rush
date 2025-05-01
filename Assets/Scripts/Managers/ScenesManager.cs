using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Supabase;

public class ScenesManager : MonoBehaviour
{
    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        var session = DBManager.Instance.supabase.Auth.CurrentSession;
        DBManager.Instance.SaveSession(session);
        Application.Quit();
    }

}
