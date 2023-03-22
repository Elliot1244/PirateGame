using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float _healthPoints = 100f;
        [SerializeField] Animator _animator;

        bool _isDead = false;

        public bool IsDead()
        {
            return _isDead;
        }
        public void TakeDamage(float damage)
        {
            _healthPoints = Mathf.Max(_healthPoints - damage, 0);
            if(_healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (_isDead) return;

            _isDead = true;
            _animator.SetTrigger("Die");
            GetComponent<ActionManager>().CancelCurrentAction();
        }
    }
}
