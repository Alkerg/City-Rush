using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject[] characters;
    public Button interactionButton;
    public TextMeshProUGUI interactionButtonText;

    private List<DBManager.CharactersModel> charactersPrices;
    private GameObject currentCharacter;
    private bool isCharacterAvailable = true;
    public int currentCharacterIndex = 0;
    public int characterSelectedIndex = 0;
    public List<int> unlockedCharacters;


    void Awake()
    {
        characterSelectedIndex = PlayerPrefs.GetInt("currentCharacterIndexPref");
        characters[characterSelectedIndex].SetActive(true);
        LoadCharactersPrice();
        LoadPlayerCharacters();
    }

    void Start()
    {
        currentCharacter = characters[characterSelectedIndex];
    }

    public void NextCharacter()
    {
        characters[currentCharacterIndex].gameObject.SetActive(false);

        if (currentCharacterIndex == characters.Length-1) currentCharacterIndex = 0;
        else currentCharacterIndex++;
        
        characters[currentCharacterIndex].gameObject.SetActive(true);
        LoadButtonData();
    }

    public void PreviousCharacter()
    {
        characters[currentCharacterIndex].gameObject.SetActive(false);

        if (currentCharacterIndex == 0) currentCharacterIndex = characters.Length-1;
        else currentCharacterIndex--;

        characters[currentCharacterIndex].gameObject.SetActive(true);
        LoadButtonData();
    }

    public Animator GetCurrentCharacterAnimator()
    {
        return currentCharacter.GetComponent<Animator>();
    }

    public void EnterCharacterSelection()
    {
        characters[characterSelectedIndex].gameObject.SetActive(false);
        characters[currentCharacterIndex].gameObject.SetActive(true);

    }

    public void ExitCharaterSelection()
    {
        characters[currentCharacterIndex].gameObject.SetActive(false);
        characters[characterSelectedIndex].gameObject.SetActive(true);
        currentCharacterIndex = 0;
    }

    public void ActionButton()
    {
        if (isCharacterAvailable) SelectCharacter();
        else BuyCharater();
    }


    private void SelectCharacter()
    {
        currentCharacter = characters[currentCharacterIndex];
        characterSelectedIndex = currentCharacterIndex;
        PlayerPrefs.SetInt("currentCharacterIndexPref", characterSelectedIndex);
    }

    private async void BuyCharater()
    {
        int currentCoins = await DBManager.Instance.GetCoins();
        int currentCharacterPrice = charactersPrices[currentCharacterIndex].Price;
        if (currentCoins >= currentCharacterPrice)
        {
            currentCoins = currentCoins - currentCharacterPrice;
        }
        DBManager.Instance.UpdateCoins(currentCoins);

        Dictionary<String, bool> unlockedCharactersDictionary = await DBManager.Instance.GetUnlockedCharacters();
        unlockedCharactersDictionary.Add(currentCharacterIndex.ToString(), true);
        DBManager.Instance.UpdateUnlockedCharacters(unlockedCharactersDictionary);

        interactionButtonText.text = "SELECT";

        LoadPlayerCharacters();
        Debug.Log("Now you have a new character!");
    }

    private async void LoadCharactersPrice()
    {
        Debug.Log("Loading characters price...");
        charactersPrices = await DBManager.Instance.GetAllCharacters();
        charactersPrices.Sort((a,b) => a.Id.CompareTo(b.Id));
    }

    private async void LoadPlayerCharacters()
    {
        Debug.Log("Loading player characters...");
        Dictionary<String,bool> unlockedCharactersDictionary = await DBManager.Instance.GetUnlockedCharacters();

        foreach(KeyValuePair<String,bool> entry in unlockedCharactersDictionary)
        {
            Debug.Log($"Charater {entry.Key}, unlocked: {entry.Value}");
            unlockedCharacters.Add(int.Parse(entry.Key));
        }
    }

    private void LoadButtonData()
    {
        if (unlockedCharacters.Contains(currentCharacterIndex))
        {
            Debug.Log("The character is available to use");
            isCharacterAvailable = true;
            interactionButtonText.text = "SELECT";
        }
        else
        {
            Debug.Log("The character is locked, buy it to unlock");
            isCharacterAvailable = false;
            int characterPrice = charactersPrices[currentCharacterIndex].Price;
            interactionButtonText.text = $"BUY FOR ${characterPrice}";
        }
    }

}
