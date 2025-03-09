using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject[] characters;
    
    private GameObject currentCharacter;
    private int currentCharacterIndex = 0;

    void Awake()
    {
        currentCharacterIndex = PlayerPrefs.GetInt("currentCharacterIndexPref");
        characters[currentCharacterIndex].SetActive(true);
    }

    void Start()
    {
        currentCharacter = characters[currentCharacterIndex];
    }

    public void NextCharacter()
    {
        characters[currentCharacterIndex].gameObject.SetActive(false);

        if (currentCharacterIndex == characters.Length-1) currentCharacterIndex = 0;
        else currentCharacterIndex++;
        
        characters[currentCharacterIndex].gameObject.SetActive(true);
        currentCharacter = characters[currentCharacterIndex];
        PlayerPrefs.SetInt("currentCharacterIndexPref", currentCharacterIndex);

    }

    public void PreviousCharacter()
    {
        characters[currentCharacterIndex].gameObject.SetActive(false);

        if (currentCharacterIndex == 0) currentCharacterIndex = characters.Length-1;
        else currentCharacterIndex--;

        characters[currentCharacterIndex].gameObject.SetActive(true);
        currentCharacter = characters[currentCharacterIndex];
        PlayerPrefs.SetInt("currentCharacterIndexPref", currentCharacterIndex);
    }

    public Animator GetCurrentCharacterAnimator()
    {
        return currentCharacter.GetComponent<Animator>();
    }
}
