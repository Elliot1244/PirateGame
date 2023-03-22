using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine.AI;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float _chaseDistance = 5f;

        Fighter _fighter;
        GameObject _player;
        Health _health;
        PlayerMouvement _mover; 

        Vector3 _guardPosition;

        
        

        private void Start()
        {
            _fighter = GetComponent<Fighter>();
            _player = GameObject.FindWithTag("Player");
            _health = GetComponent<Health>();
            _mover = GetComponent<PlayerMouvement>();
            _guardPosition = transform.position;
        }

        private void Update()
        {
            GameObject _player = GameObject.FindWithTag("Player");

            if (_health.IsDead()) return;

            if (IsInAttackRangeOfPlayer()  && _fighter.CanAttack(_player))
            {
                _fighter.Attack(_player);
            }
            else
            {
                _mover.StartMovementAction(_guardPosition);
            }

        }

        private bool IsInAttackRangeOfPlayer()
        {
            float _distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position); //Récupère la distance entre le joueur et les enemis.
            return _distanceToPlayer <= _chaseDistance;
        }


        //Appeler par Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
    }
}
