using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> Services = new();
    
    public static void Register<T>(T service) where T : class
    {
        var type = typeof(T);

        if (Services.ContainsKey(type))
        {
            Debug.LogWarning($"[ServiceLocator] Overwriting existing service of type {type.Name}.");
        }

        Services[type] = service;
        Debug.Log($"[ServiceLocator] Registered: {type.Name}");
    }
    
    public static T Get<T>() where T : class
    {
        var type = typeof(T);

        if (Services.TryGetValue(type, out var service))
        {
            return service as T;
        }

        Debug.LogError($"[ServiceLocator] Service not found: {type.Name}");
        return null;
    }
    
    public static void Unregister<T>() where T : class
    {
        var type = typeof(T);

        if (Services.Remove(type))
        {
            Debug.Log($"[ServiceLocator] Unregistered: {type.Name}");
        }
    }
    
    public static void Clear()
    {
        Services.Clear();
        Debug.Log("[ServiceLocator] All services cleared.");
    }
}