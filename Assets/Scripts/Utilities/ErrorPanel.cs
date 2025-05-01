using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorPanel : MonoBehaviour
{
    public static ErrorPanel Instance { get; private set; }
    public TMP_Text Title;
    public TMP_Text ErrorMessage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(5, 30);
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMessage(string message)
    {
        ErrorMessage.text = message;
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
