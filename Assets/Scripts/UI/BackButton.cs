using System;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    private Stack<Action> _backButtonActions = new Stack<Action>();
    
    private static BackButton _instance;
    public static BackButton Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_backButtonActions.Count == 1)
                _backButtonActions.Peek().Invoke();
            else
                _backButtonActions.Pop().Invoke();
        }
    }

    //Refactor Classes for unite common logic.
    public void AddBackButtonAction(Action action) => _backButtonActions.Push(action);

    public void SetDefaultAction(Action action)
    {
        _backButtonActions.Clear();

        AddBackButtonAction(action);
    }

    public void RemoveLastAction()
    {
        if (_backButtonActions.Count == 1)
            return;

        _backButtonActions.Pop();
    }
}
