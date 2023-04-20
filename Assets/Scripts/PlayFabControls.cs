using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabControls : MonoBehaviour
{
    [SerializeField] GameObject signUpTab, loginTab, startPanel, HUD;

    public Text username, userEmail, userPassword, userEmailLogin, userPasswordLogin, errorSignUp, errorLogin;
    string encryptedPassword;
    protected GameObject loadingIndicator;

    public void SwitchSignUpTab()
    {
        signUpTab.SetActive(true);
        loginTab.SetActive(false);
    }

    public void SwitchLoginTab()
    {
        loginTab.SetActive(true);
        signUpTab.SetActive(false);
        Debug.Log("Works");
        errorLogin.text = "";
        errorSignUp.text = "";

    }

    string Encrypt(string pass)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(pass);
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        foreach (byte b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }
        return s.ToString();
    }

    public void signUp()
    {
        var registerRequest = new RegisterPlayFabUserRequest();
        registerRequest.Email = userEmail.text; registerRequest.Password = Encrypt(userPassword.text); registerRequest.Username = username.text;
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, RegisterSuccess, RegisterError);
    }


    public void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        errorLogin.text = "";
        errorSignUp.text = "";
        StartGame();
    }
    public void LoginSuccess(LoginResult result)
    {
        errorLogin.text = "";
        errorSignUp.text = "";
        StartGame();
    }

    public void RegisterError(PlayFabError error)
    {
        errorSignUp.text = error.GenerateErrorReport();
    }


    public void Login()
    {
        var request = new LoginWithEmailAddressRequest();
        request.Email = userEmailLogin.text;
        request.Password = Encrypt(userPasswordLogin.text);
        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LoginError);
    }
    public void LoginError(PlayFabError error)
    {
        errorLogin.text = error.GenerateErrorReport();
    }

    void StartGame()
    {
        Debug.Log("Redirect to menu");
    }
}
