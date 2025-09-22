using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;

public class RelayInitializer : MonoBehaviour
{
    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Unity Gaming Services initialized and signed in anonymously.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error initializing Unity Gaming Services: {e.Message}");
        }
    }
}