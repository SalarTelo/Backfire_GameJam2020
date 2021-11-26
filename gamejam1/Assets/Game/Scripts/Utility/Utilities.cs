using SpellcastStudios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios
{
    public static class Utilities
    {
        public static T CopyComponent<T>(T original, GameObject destination) where T : Component
        {
            System.Type type = original.GetType();
            Component copy = destination.AddComponent(type);
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(copy, field.GetValue(original));
            }
            return copy as T;
        }

        public static Component CopyComponent(Component original, GameObject destination)
        {
            return CopyComponent<Component>(original, destination);
        }
    }
}