using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Toggle aiToggle;
    public Text turnText;
    private GameObject[] localPlayers;
    private bool isPlayerTurn = true;

    private const string aiPlayerPrefsKey = "AI";

    private void Start()
    {
        InitializeAIStatus();
        FindLocalPlayers();
        UpdateTurnText();
    }

    private void Update()
    {
        if (!isPlayerTurn && aiToggle.isOn)
        {
            StartCoroutine(SimulateAITurn());
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void aiToggleChange(bool flag)
    {
        Debug.Log("aiToggleChange " + aiToggle.isOn);
        PlayerPrefs.SetInt(aiPlayerPrefsKey, aiToggle.isOn ? 1 : 0);
    }

    private void InitializeAIStatus()
    {
        int aiStatus = PlayerPrefs.GetInt(aiPlayerPrefsKey, 1);
        aiToggle.isOn = (aiStatus == 1);
    }

    private void FindLocalPlayers()
    {
        localPlayers = GameObject.FindGameObjectsWithTag("LocalPlayer");
    }

    private void UpdateTurnText()
    {
        string playerType = isPlayerTurn ? "Torn del jugador local" : "Torn de la IA";
        string playerName = GetLocalPlayerName();
        turnText.text = $"{playerType}: {playerName}";
    }

    private string GetLocalPlayerName()
    {
        if (localPlayers.Length > 0)
        {
            return localPlayers[0].name;
        }
        else
        {
            return "Desconegut";
        }
    }

    private IEnumerator SimulateAITurn()
    {
        yield return new WaitForSeconds(3f);
        // Lógica de la IA para su jugada aquí
        ChangeTurn();
    }

    private void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        UpdateTurnText();
    }
}
