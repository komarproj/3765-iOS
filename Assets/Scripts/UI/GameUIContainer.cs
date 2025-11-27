using System;
using System.Collections.Generic;
using UI.Popups;
using UI.Screens;
using UnityEngine;

public class GameUIContainer : MonoBehaviour
{
    [SerializeField] private BaseScreen[] _screens;
    [SerializeField] private BasePopup[] _popups;
    
    private readonly Dictionary<Type, BaseScreen> _screensDict = new();
    private readonly Dictionary<Type, BasePopup> _popupsDict = new();
    
    private void Awake()
    {
        RegisterScreens();
        RegisterPopups();
    }

    public bool TryGetScreen<T>(out T screen) where T : BaseScreen
    {
        if (_screensDict.TryGetValue(typeof(T), out BaseScreen prefab))
        {
            screen = prefab as T;
            return true;
        }
        
        screen = null;
        return false;
    }
    
    public bool TryGetPopup<T>(out T popup) where T : BasePopup
    {
        if (_popupsDict.TryGetValue(typeof(T), out BasePopup prefab))
        {
            popup = prefab as T;
            return true;
        }
        
        popup = null;
        return false;
    }

    private void RegisterScreens()
    {
        foreach (var screen in _screens)
            _screensDict.Add(screen.GetType(), screen);
    }

    private void RegisterPopups()
    {
        foreach (var popup in _popups)
            _popupsDict.Add(popup.GetType(), popup);
    }
}