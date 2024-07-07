using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    #region SINGLETON

    private static GameUiManager _instance;

    public static GameUiManager Instance => _instance;

    #endregion

    #region GAMEOBJECT_REFERENCES

    [SerializeField] private GameboardScreen _gameboardScreen;

    #endregion

    private void Awake()
    {
        _instance = this;
    }

    public void ShowGameboardScreen()
    {
        _gameboardScreen.gameObject.SetActive(true);
        _gameboardScreen.InitGameboard();
    }
}
