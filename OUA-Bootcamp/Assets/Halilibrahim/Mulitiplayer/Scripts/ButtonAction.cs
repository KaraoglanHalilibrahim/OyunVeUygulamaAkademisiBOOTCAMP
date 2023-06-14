using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    private NetworkManager NetworkManager;

    void Start()
    {
        NetworkManager = GetComponentInParent<NetworkManager>();
    }

   public void StartHost()
    {
        NetworkManager.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.StartClient();
    }
}
