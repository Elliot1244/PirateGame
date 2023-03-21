using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float _weaponRange = 2f;

        Transform _target;
        private void Update()
        {
            if (_target == null) return;

            if (!GetDistance())
            {
                GetComponent<PlayerMouvement>().MoveTo(_target.position); //On fait appel à la fonction MoveTo pour aller vers la position de la cible.
            }
            else
            {
                GetComponent<PlayerMouvement>().Cancel();
            }
            
        }

        private bool GetDistance()
        {
            return Vector3.Distance(transform.position, _target.position) < _weaponRange;  
            //Calcule la distance entre notre position et celle de la cible. Retourne true si la distance est inférieure à _weaponRange
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionManager>().StartAction(this); 
            _target = combatTarget.transform; //On récupère la position de la cible que l'on attaque, que l'on store dans _target.
        }

        public void Cancel()
        {
            _target = null;
        }
    }
}
