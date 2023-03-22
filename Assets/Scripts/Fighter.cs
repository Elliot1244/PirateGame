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

        Health _target;
        float _timeSinceLastAttack = Mathf.Infinity;
        float _damage = 5f;
        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            if (_target == null) return;

            if (_target.IsDead()) return;

            if (!GetDistance())
            {
                GetComponent<PlayerMouvement>().MoveTo(_target.transform.position); //On fait appel à la fonction MoveTo pour aller vers la position de la cible.
            }
            else
            {
                GetComponent<PlayerMouvement>().Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            transform.LookAt(_target.transform.position);

            if (_timeSinceLastAttack >= _timeBetweenAttacks)
            {
                //Active le PunchHit() Event
                TriggerAttack();
                _timeSinceLastAttack = 0f;
            }

        }

        private void TriggerAttack()
        {
            _animator.ResetTrigger("StopAttack");
            _animator.SetTrigger("Punch");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) { return false; }
           
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionManager>().StartAction(this);
            _target = combatTarget.GetComponent<Health>(); //On récupère le composant Health que l'on attaque, que l'on store dans _target.
        }

        //Animation Event
        void PunchHit()
        {
            if(_target == null) { return; }
            _target.TakeDamage(_damage);
        }


        private bool GetDistance()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < _weaponRange;  
            //Calcule la distance entre notre position et celle de la cible. Retourne true si la distance est inférieure à _weaponRange
        }

        
        public void Cancel()
        {
            StopAttack();
            _target = null;
        }

        private void StopAttack()
        {
            _animator.ResetTrigger("Punch");
            _animator.SetTrigger("StopAttack");
        }
    }
}
