using DevionGames.LoginSystem.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

namespace DevionGames.LoginSystem
{
    public class UserManager : MonoBehaviour
    {
		
	   public static Text dialogText;
		private void Awake()
		{
			
		
		}

        private void Start()
        {
			//TODO pass the login if logged in
			
			

		}

	
public  void CreateAccount(string username, string password, string email)
{
    var request = new RegisterPlayFabUserRequest { Username = username, Password = password, Email = email };
    PlayFabClientAPI.RegisterPlayFabUser(request, OnCreateAccountSuccess, OnCreateAccountFailure);
}

private  void OnCreateAccountSuccess(RegisterPlayFabUserResult result)
{

        Debug.Log("[CreateAccount] Account creation was successful!");
		dialogText.text = "Account creation was successful!";

    EventHandler.Execute("OnAccountCreated");
}

private  void OnCreateAccountFailure(PlayFabError error)
{
  
        Debug.Log("[CreateAccount] Failed to create account. Error: " + error.ErrorMessage);
		dialogText.text = "Failed to create account.";

    EventHandler.Execute("OnFailedToCreateAccount");
}


public  static void LoginAccount(string username, string password)
{
    LoginAccountInternal(username, password);
}

private  static void LoginAccountInternal(string username, string password)
{


    Debug.Log("[LoginAccount] Trying to login using username: " + username + " and password: " + password + "!");

    var request = new LoginWithPlayFabRequest { Username = username, Password = password };
    PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
}

private static void OnLoginSuccess(LoginResult result)
{

        Debug.Log("[LoginAccount] Login was successful!");
		//TODO Open Menu

    EventHandler.Execute("OnLogin");
}

private static void OnLoginFailure(PlayFabError error)
{

        Debug.Log("[LoginAccount] Failed to login. Error: " + error.ErrorMessage);
		
		dialogText.text = "Failed to login.";
	

    EventHandler.Execute("OnFailedToLogin");
}



		/// <summary>
		/// Validates the email.
		/// </summary>
		/// <returns><c>true</c>, if email was validated, <c>false</c> otherwise.</returns>
		/// <param name="email">Email.</param>
		public  bool ValidateEmail(string email)
		{
			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			System.Text.RegularExpressions.Match match = regex.Match(email);
			if (match.Success)
			{
			
					Debug.Log("Email validation was successfull for email: " + email + "!");
			}
			else
			{
				
					Debug.Log("Email validation failed for email: " + email + "!");
			}

			return match.Success;
		}
	}
}