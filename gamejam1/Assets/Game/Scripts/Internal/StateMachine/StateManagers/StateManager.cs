using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpellcastStudios
{
    public class StateManager : MonoBehaviour
    {
        protected IState currentState;
        protected List<IState> states = new List<IState>();

        protected Dictionary<string, object> blackboard = new Dictionary<string, object>();

        public T GetValue<T>(string key)
        {
            if (!blackboard.ContainsKey(key))
                return default(T);

            Type typeParameterType = typeof(T);

            try
            {
                return (T)blackboard[key];
            }
            catch
            {
                throw new Exception("Cannot cast key of type " + blackboard[key].GetType().Name + " to " + typeof(T).Name);
            }
        }
        public object GetValue(string key)
        {
            if (!blackboard.ContainsKey(key))
                return null;

            return blackboard[key];
        }
        public void SetValue<T>(string key, T value)
        {
            blackboard[key] = value;
        }
        public void AddValue<T>(string key, T value)
        {
            blackboard.Add(key, value);
        }

        private void Awake()
        {
            foreach (var comp in GetComponents<IState>())
            {
                comp.manager = this;
                AddState(comp);
            }
        }
        public void ChangeState(IState state)
        {
            if (currentState == state)
                return;

            if(!states.Contains(state))
            {
                print("The enemy does not have the " + state.ToString() + " state!");
                return;
            }

            if (currentState != null) 
            currentState.Exit();

            currentState = state;

            currentState.Enter();
        }
        public void AddState(IState state) 
        {
            states.Add(state);
        }
        public void RemoveState(IState state)
        {
            states.Remove(state);
        }

        protected virtual void Update()
        {
            if (currentState != null)
            {
                currentState.Input();
            }
        }
        protected virtual void FixedUpdate()
        {
            if (currentState != null)
            {
                currentState.Action();
            }
        }
    }
}