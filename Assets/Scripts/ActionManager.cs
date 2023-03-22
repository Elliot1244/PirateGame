using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionManager : MonoBehaviour
    {

        IAction _currentAction;
        public void StartAction(IAction action)
        {
            if (_currentAction == action) return; //Si l'action en cours est la même que l'action d'avant, on ne fait rien.
            if(_currentAction != null)
            {
                _currentAction.Cancel();
            }
            _currentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}
