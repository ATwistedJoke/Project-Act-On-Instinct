using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Timeline;

public class StateMachine<T>
{
    private T state; 
    private Dictionary<T, Action> onEnterActions;
    private Dictionary<T, Action> onExitActions;
    private HashSet<Tuple<T, T>> transitions;
    private Dictionary<Tuple<T, T>, Action> transitionActions;

    public StateMachine(T initialState)
    {
        state = initialState;
        onEnterActions = new Dictionary<T, Action>();
        onExitActions = new Dictionary<T, Action>();
        transitions = new HashSet<Tuple<T, T>>();
        transitionActions = new Dictionary<Tuple<T, T>, Action>();
    }
    
    public T GetState(){ return state; }

    public void addEnterAction(T state, Action action)
    {
        onEnterActions.Add(state, action); 
    }

    public void addExitAction(T state, Action action)
    {
        onExitActions.Add(state, action); 
    }

    public void removeEnterAction(T state)
    {
        onEnterActions.Remove(state);
    }

    public void removeExitAction(T state)
    {
        onEnterActions.Remove(state); 
    }

    public void AddTransition(T state, T newState)
    {
        transitions.Add(Tuple.Create(state, newState)); 
    }

    public void AddTransitionAction(T state, T newState, Action action)
    {
        Tuple<T,T> transition = Tuple.Create(state, newState); 
        transitionActions.Add(transition, action); 
    }

    public void Transition(T newState)
    {
        Tuple<T,T> transition = Tuple.Create(state, newState);
        if (!transitions.Contains(transition))
        {
            Debug.Log($"Missing transition from {state} to {newState}");
            return; 
        }

        if (onExitActions.ContainsKey(state))
        {
            onExitActions[state].Invoke(); 
        }
        if (transitionActions.ContainsKey(transition))
        {
            transitionActions[transition].Invoke(); 
        }
        if (onEnterActions.ContainsKey(state))
        {
            onEnterActions[state].Invoke(); 
        }

        state = newState;
    }
}
