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
        [SerializeField] Animator _animator;
        [SerializeField] float _timeBetweenAttacks = 1f;

        Transform _target;
        float _timeSinceLastAttack;
        float _damage = 5f;
        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            if (_target == null) return;

            if (!GetDistance())
            {
                GetComponent<PlayerMouvement>().MoveTo(_target.position); //On fait appel à la fonction MoveTo pour aller vers la position de la cible.
            }
            else
            {
                GetComponent<PlayerMouvement>().Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            if(_timeSinceLastAttack >= _timeBetweenAttacks)
            {
                //Active le PunchHit() Event
                _animator.SetTrigger("Punch");
                _timeSinceLastAttack = 0f;
            }
            
        }

        //Animation Event
        void PunchHit()
        {
            Health _healthComponent = _target.GetComponent<Health>();
            _healthComponent.TakeDamage(_damage);
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionManager>().StartAction(this);
            _target = combatTarget.transform; //On récupère la position de la cible que l'on attaque, que l'on store dans _target.
        }


        private bool GetDistance()
        {
            return Vector3.Distance(transform.position, _target.position) < _weaponRange;  
            //Calcule la distance entre notre position et celle de la cible. Retourne true si la distance est inférieure à _weaponRange
        }

        
        public void Cancel()
        {
            _target = null;
        }
    }
}
