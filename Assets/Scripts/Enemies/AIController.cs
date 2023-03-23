using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine.AI;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float _chaseDistance = 5f;
        [SerializeField] float _suspicionTime = 3f;
        [SerializeField] float _guardingTime = 3f;
        [SerializeField] PatrolPath _patrolPath;
        [SerializeField] float _wayPointDistance = 0.5f;

        int _currentWayPointIndex = 0;
        float _timeSinceLastSawPlayer = Mathf.Infinity;
        float _timeGuardingWayPoint = Mathf.Infinity;
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

            if (IsInAttackRangeOfPlayer() && _fighter.CanAttack(_player))
            {
                AttackBehavior();
                
            }
            else if (_timeSinceLastSawPlayer <= _suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                PatrolBehavior();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeGuardingWayPoint += Time.deltaTime;
        }

        private void PatrolBehavior()
        {
            Vector3 _nextPosition = _guardPosition;

            if(_patrolPath != null)
            {
                if(AtWayPoint())
                {
                        CycleWayPoint();
                        _timeGuardingWayPoint = 0;
                }
                _nextPosition = GetCurrentWayPoint();
            }
            if (_timeGuardingWayPoint >= _guardingTime)
            {
                _mover.StartMovementAction(_nextPosition);
            }  
        }

        private Vector3 GetCurrentWayPoint()
        {
          return  _patrolPath.GetWayPoint(_currentWayPointIndex);
        }

        private void CycleWayPoint()
        {
            _currentWayPointIndex = _patrolPath.GetNexIndex(_currentWayPointIndex);
        }

        private bool AtWayPoint()
        {
            float _distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return _distanceToWayPoint <= _wayPointDistance;
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionManager>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            _fighter.Attack(_player);
            _timeSinceLastSawPlayer = 0;
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
