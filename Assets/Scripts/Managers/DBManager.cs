using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supabase;
using System.Linq;
using Supabase.Gotrue;
using System.Threading.Tasks;
using Postgrest.Models;
using Postgrest.Attributes;
using System;
using System.Text.Json;
using System.Net.Http;
using static System.Net.WebRequestMethods;
using System.Net.Sockets;
using Supabase.Gotrue.Exceptions;
using static Postgrest.Constants;
using static DBManager;

public class DBManager : MonoBehaviour
{
    public static DBManager Instance { get; private set; }
    public Supabase.Client supabase { get; private set; }
    private readonly string SUPABASE_URL = "";
    private readonly string SUPABASE_KEY = "";
    //public ErrorPanel errorPanel;
    //private Dictionary<string, bool> data = new Dictionary<string, bool>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDatabaseConnection();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    public async void InitializeDatabaseConnection()
    {
        Debug.Log("Initialize connection ...");
        var options = new SupabaseOptions
        {
            AutoConnectRealtime = true
        };
        supabase = new Supabase.Client(SUPABASE_URL, SUPABASE_KEY, options);
        await supabase.InitializeAsync();
        TryRestoreSession();

    }

    // Database manipulation methods
    public async Task<bool> SignIn(string email, string password)
    {
        try
        {
            var session = await Instance.supabase.Auth.SignIn(email, password);
            String username = await GetPlayerUsername();
            Debug.Log($"Logging successfully, {username}");
            SaveSession(session);
            return true;
        }
        catch (Exception error)
        {
            HandleExceptions(error);
            return false;
        }

    }

    public async Task<bool> SignInWithDiscord()
    {
        try
        {
            var signInUrl = await Instance.supabase.Auth.SignIn(Constants.Provider.Discord);
            Debug.Log(signInUrl.Uri);

            return true;
        }
        catch (Exception error)
        {
            HandleExceptions(error);
            return false;
        }
    }

    public async Task<bool> SignUp(string username, string email, string password)
    {

        try
        {
            if (username == null || username == "") {
                throw new UsernameException();
            }
            else
            {
                var session = await supabase.Auth.SignUp(email, password);
                Guid userId = Guid.Parse(session.User.Id);
                CreatePlayer(username, userId, 0, 0);

                Debug.Log($"Player {username} created successfully");

                await supabase.Auth.SignIn(email, password);
                SaveSession(session);
                Debug.Log($"Logging successfully, {username}");

                return true;
            }

        }
        catch(Exception error)
        {
            HandleExceptions(error);
            return false;
        }
    }

    public async Task<bool> SignOut()
    {
        try
        {
            await supabase.Auth.SignOut();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async void SignInAnonymously()
    {
        try
        {
            await supabase.Auth.SignInAnonymously();
        }
        catch (Exception error)
        {
            HandleExceptions(error);
        }
    }

    public async void PromoteAnonymousUser(string email, string password, string username, int coins, float best_score)
    {
        var userId = Guid.Parse(supabase.Auth.CurrentUser.Id);

        await supabase.Auth.Update(new Supabase.Gotrue.UserAttributes
        {
            Email = email,
            Password = password
        });

        CreatePlayer(username, userId, coins, best_score);
    }

    public async void CreatePlayer(string username, Guid user_id, int coins=0, float best_score=0)
    {
        var player = new PlayerModel
        {
            Username = username,
            Coins = coins,
            BestScore = best_score,
            UserId = user_id

        };
        await supabase.From<PlayerModel>().Insert(player);
    }

    public async void UpdateCoins(int newCoinsValue)
    {
        Guid userId = Guid.Parse(Instance.supabase.Auth.CurrentUser.Id);
        var update = await Instance.supabase
            .From<PlayerModel>()
            .Where(playerModel => playerModel.UserId == userId)
            .Set(playerModel => playerModel.Coins, newCoinsValue)
            .Update();
    }

    public async void UpdateBestScore(float newScoreValue)
    {
        Guid userId = Guid.Parse(Instance.supabase.Auth.CurrentUser.Id);
        var update = await Instance.supabase
            .From<PlayerModel>()
            .Where(playerModel => playerModel.UserId == userId)
            .Set(playerModel => playerModel.BestScore, newScoreValue)
            .Update();
    }

    public async void UpdateUnlockedCharacters(Dictionary<string, bool> newUnlockedCharacters)
    {
        Guid userId = Guid.Parse(Instance.supabase.Auth.CurrentUser.Id);
        var update = await Instance.supabase
            .From<PlayerModel>()
            .Where(playerModel => playerModel.UserId == userId)
            .Set(playerModel => playerModel.Unlocked_characters, newUnlockedCharacters)
            .Update();
    }

    public async Task<Dictionary<String,bool>> GetUnlockedCharacters()
    {
        var userId = supabase.Auth.CurrentUser.Id;
        try
        {
            var result = await supabase
                .From<PlayerModel>()
                .Filter("user_id", Postgrest.Constants.Operator.Equals, userId)
                .Get();
            return result.Model.Unlocked_characters;
        }
        catch (Exception error)
        {
            Debug.Log(error.Message);
            return null;
        }
    }

    public async Task<List<CharactersModel>> GetAllCharacters()
    {
        try
        {
            var response = await Instance.supabase
                .From<CharactersModel>()
                .Get();

            if (response != null && response.Models.Count > 0)
            {
                Debug.Log($"{response.Models.Count} characters retrieved.");
                Debug.Log(response.Models.ToString());
                return response.Models;
            }
            else
            {
                Debug.LogWarning("No characters in table");
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            return null;
        }
    }

    // Auxiliar methods
    public async Task<float> GetBestScore()
    {
        var userId = supabase.Auth.CurrentUser.Id;
        try
        {
            var result = await supabase
                .From<PlayerModel>()
                .Filter("user_id", Postgrest.Constants.Operator.Equals, userId)
                .Get();
            return result.Model.BestScore;
        }
        catch (Exception error)
        {
            Debug.Log(error.Message);
            return 0;
        }
    }

    public async Task<int> GetCoins()
    {
        var userId = supabase.Auth.CurrentUser.Id;
        try
        {
            var result = await supabase
                .From<PlayerModel>()
                .Filter("user_id", Postgrest.Constants.Operator.Equals, userId)
                .Get();
            return result.Model.Coins;
        }
        catch (Exception error)
        {
            Debug.Log(error.Message);
            return 0;
        }
    }

    public async Task<String> GetPlayerUsername()
    {
        var currentUser = Instance.supabase.Auth.CurrentUser;
        var userId = Instance.supabase.Auth.CurrentUser.Id;
        try
        {
            var result = await Instance.supabase
                .From<PlayerModel>()
                .Filter("user_id", Postgrest.Constants.Operator.Equals, userId)
                .Get();
            return result.Model.Username;
        }
        catch (Exception error)
        {
            Debug.Log(error.Message);
            return "";
        }
    }

    // Session methods

    public async void TryRestoreSession()
    {
        string accessToken = PlayerPrefs.GetString("access_token", "");
        string refreshToken = PlayerPrefs.GetString("refresh_token", "");

        if (!string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(refreshToken))
        {
            try
            {
                await supabase.Auth.SetSession(accessToken,refreshToken);
                Debug.Log("Session restored successfully: " + supabase.Auth.CurrentUser?.Email);
                ScenesManager.LoadScene(1);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Error restoring session: {ex.Message}");
            }
        }
    }

    public void SaveSession(Session session)
    {
        PlayerPrefs.SetString("access_token", session.AccessToken);
        PlayerPrefs.SetString("refresh_token", session.RefreshToken);
        PlayerPrefs.Save();
    }

    public void DeleteSession()
    {
        PlayerPrefs.SetString("access_token", "");
        PlayerPrefs.SetString("refresh_token", "");
        PlayerPrefs.Save();
    }

    //Leaderboard methods

    public async Task<List<PlayerModel>> GetLeaderboard(int limit = 10)
    {
        try
        {
            var result = await supabase
                .From<DBManager.PlayerModel>()
                .Order(player => player.BestScore, Ordering.Descending) 
                .Limit(limit)
                .Get();

            var leaderboard = result.Models.ToList();

            foreach (var player in leaderboard)
            {
                Debug.Log($"{player.Username}: {player.BestScore}");
            }

            return leaderboard;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error getting leaderboard " + ex.Message);
            return new List<PlayerModel>();
        }
    }


    // Error handling methods
    private void HandleExceptions(Exception error)
    {
        Debug.LogError(error.ToString());

        if (IsNetworkError(error))
        {
            ErrorPanel.Instance.SetMessage("Internet connection error, try to reconnect");
            ErrorPanel.Instance.gameObject.SetActive(true);
            return;
        }

        if(error is UsernameException)
        {
            ErrorPanel.Instance.SetMessage(error.Message);
            ErrorPanel.Instance.gameObject.SetActive(true);
            return;
        }

        var errorData = JsonSerializer.Deserialize<ErrorResponse>(error.Message);
        if (errorData != null)
        {
            ErrorPanel.Instance.SetMessage($"<b>Code:</b>{errorData.code} \n\n<b>Reason:</b> {errorData.msg}");
        }
        else
        {
            ErrorPanel.Instance.SetMessage("<b>Code:</b> Unknown \n<b>Reason:</b> Unknown");
        }
        ErrorPanel.Instance.gameObject.SetActive(true);
    }

    private bool IsNetworkError(Exception error)
    {
        return error.InnerException is HttpRequestException || error.InnerException is SocketException;
    }


    // Models
    [Table("players")]
    public class PlayerModel : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid PlayerId { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("coins")]
        public int Coins { get; set; }

        [Column("best_score")]
        public float BestScore { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("unlocked_characters")]
        public Dictionary<String,bool> Unlocked_characters { get; set; }
    }

    [Table("characters")]
    public class CharactersModel : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("price")]
        public int Price { get; set; }
    }

    // Custom exception classes
    public class ErrorResponse
    {
        public int code { get; set; }
        public string error_code { get; set; }
        public string msg { get; set; }
    }

    public class UsernameException: Exception
    {
        public UsernameException()
        : base("<b>Code: </b>420 \n\n<b>Reason: </b> Username is empty") { }
    }


}
