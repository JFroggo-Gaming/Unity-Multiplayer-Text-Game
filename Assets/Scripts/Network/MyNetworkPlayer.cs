using Mirror;
using TMPro;
using UnityEngine;
using System;

public class MyNetworkPlayer : NetworkBehaviour
{
// In this script, there are all things related to the player, such as display name, assigning the ownership of the lobby, etc.
#region Variables
[SerializeField] private TMP_Text displayNameText= null; // A text field with the player's name

[SyncVar(hook = nameof(AuthorityHandlePartyOwnerStateUpdated))]
private bool isPartyOwner = false;

[SyncVar(hook = nameof(ClientHandleDisplayNameUpdated))] // Syncvar that will allow other players to store our name
public string displayName;

// This below is the event created in order to handle the changes of client info
// eg. his displayed name
public static event Action ClientOnInfoUpdated;

public string GetDisplayName() // Public string, which returns the display name
{
    return displayName;
}

public bool GetIsPartyOwner()
{
    return isPartyOwner;
}
public static event Action<bool> AuthorityOnPartyOwnerStateUpdated;

#endregion

#region Serwer 

public override void OnStartServer()
{
    DontDestroyOnLoad(gameObject);    
}

[Server]

public void SetPartyOwner(bool state) // Checking who the party owner is
{
    isPartyOwner = state;
}

[Server]
public void SetDisplayName(string newDisplayName) // Assigning a display name
{
    displayName = newDisplayName;   
}
#endregion

#region Commands

[Command]
private void CmdSetDisplayName(string newDisplayName) // This function allows us to setup our display name(We dont use this at the moment)
{
    // Here I add the codition saying that the player length can not be shorter than 2 characters
    // if(newDisplayName.Length <2 || newDisplayName.Length > 20){ return; }
    RpcLogNewName(newDisplayName);
    SetDisplayName(newDisplayName);
}
[ClientRpc]


private void RpcLogNewName(string newDisplayName)  // Make our new name be visible for other players (We dont use this at the moment)
{
    Debug.Log(newDisplayName);
}
[Command]
public void CmdStartGame() // Client can tell the server, that he wants to start the game
{
    if(!isPartyOwner) { return; } // if he is not the party owner, he can not do that
    ((MyNetworkManager)NetworkManager.singleton).StartGame(); // if he is, StartGame
}


#endregion

#region Authorities
private void AuthorityHandlePartyOwnerStateUpdated(bool oldState, bool newState)
{
    // It allows you to change who the party owner is during the lobby
    // I plan to add a button with this change in the future, as long as this feature is not used
    if(!hasAuthority) {return; }
    AuthorityOnPartyOwnerStateUpdated?.Invoke(newState);

}
#endregion

#region Client

private void HandleDisplayNameUpdated(string oldName, string newName) // Change the player's name
{
   displayNameText.text = newName;
}

public override void OnStartClient() // Connect the reference from MyNetworkManager 

{ 
    if (NetworkServer.active) {return;}
    // Whenever a client starts and we're not the host, if we just end up a client, we can 
    // be added to our list of the players
    ((MyNetworkManager)NetworkManager.singleton).players.Add(this);
    // This line below prevcents our player objects from being destroyed when
    // changing the scanes
      DontDestroyOnLoad(gameObject);   
}
private void ClientHandleDisplayNameUpdated(string oldDisplayName, string newDisplayName)
{   // We need to display people's name when they got updated (in the UI)
    ClientOnInfoUpdated?.Invoke();
}
public override void OnStopClient()
{   
     ClientOnInfoUpdated?.Invoke();
    // If we are not the server we do this for everyone:
    if (!isClientOnly) {return; }
    // Here we remove a player from the list of players
    ((MyNetworkManager)NetworkManager.singleton).players.Remove(this);
    // If we are the server we do this for everyone:
    ClientOnInfoUpdated?.Invoke();
    if (!hasAuthority) { return; }
}

#endregion
}
