using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Threading.Tasks;

public class LeaderboardManager : MonoBehaviour
{
    public List<User> leaderboard = new List<User>();
    public List<TextMeshProUGUI> usernames;
    public List<TextMeshProUGUI> scores;
    public GameObject entryPrefab;
    public Transform leaderboardTransform;

    public void GetLeaderboard()
    {
        LoadLeaderboard();
    }

    private async void LoadLeaderboard()
    {

        List<DBManager.PlayerModel> players = await DBManager.Instance.GetLeaderboard();
        foreach(DBManager.PlayerModel player in players)
        {
            GameObject entry = Instantiate(entryPrefab, leaderboardTransform);
            TextMeshProUGUI tmproUGUI = entry.GetComponent<TextMeshProUGUI>();
            tmproUGUI.text = player.Username + " - " + player.BestScore.ToString();
        }
    }

    public void DestroyItems()
    {
        foreach (Transform child in leaderboardTransform)
        {
            Destroy(child.gameObject);
        }
    }
   
}
