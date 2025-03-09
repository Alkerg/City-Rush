using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using Firebase.Database;
using System.Linq;
using System.Threading.Tasks;

public class LeaderboardManager : MonoBehaviour
{
   /* private DatabaseReference databaseReference;
    public List<User> leaderboard = new List<User>();
    public List<TextMeshProUGUI> usernames;
    public List<TextMeshProUGUI> scores;
    public GameObject entryPrefab;
    public Transform leaderboardTransform;

    private void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void GetLeaderboard()
    {
        StartCoroutine(LoadLeaderboard());
    }

    private IEnumerator LoadLeaderboard()
    {
        Task<DataSnapshot> task = databaseReference.Child("leaderboard").GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted); 

        if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            leaderboard.Clear();

            foreach (DataSnapshot child in snapshot.Children)
            {
                string username = child.Child("username").Value.ToString();
                float score = float.Parse(child.Child("score").Value.ToString());
                leaderboard.Add(new User(username, score));
            }

            leaderboard = leaderboard.OrderByDescending(user => user.score).ToList();

            for (int i = 0; i < leaderboard.Count; i++)
            {
                GameObject entry = Instantiate(entryPrefab, leaderboardTransform);
                TextMeshProUGUI tmproUGUI = entry.GetComponent<TextMeshProUGUI>();
                tmproUGUI.text = leaderboard[i].username + " - " + leaderboard[i].score.ToString();
            }

        }
    }

    public void DestroyItems()
    {
        foreach (Transform child in leaderboardTransform)
        {
            Destroy(child.gameObject);
        }
    }
   */
}
