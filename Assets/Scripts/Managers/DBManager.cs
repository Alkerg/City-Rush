using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Firebase.Database;
using System.Linq;

public class DBManager : MonoBehaviour
{
   /* private string userID;
    private DatabaseReference databaseReference;
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CreateUser(string name, float score)
    {
        User newUser = new User(name, score);
        string json = JsonUtility.ToJson(newUser);

        databaseReference.Child("leaderboard").Child(userID).SetRawJsonValueAsync(json);
    }

    public void UpdateScore(float score)
    {
        databaseReference.Child("leaderboard").Child(userID).Child("score").SetValueAsync(score);
    }

    */
}
