using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios
{
    /// <summary>
    /// Base class for all dynamic objects in game
    /// </summary>
    public class Entity : MonoBehaviour, IEntity
    {
        GameObject IEntity.GameObject => gameObject;
    }

    public interface IEntity
    {
        GameObject GameObject { get; }
    }
}
