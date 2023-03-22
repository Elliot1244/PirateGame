using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class PlayerMouvement : MonoBehaviour, IAction
    {

         NavMeshAgent _navMeshAgent;
         Health _health;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _health = GetComponent<Health>();
        }

        void Update()
        {
            if(_health.IsDead())
            {
                _navMeshAgent.enabled = false;
            }
            UpdateAnimations();
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }

        private void UpdateAnimations()
        {
            Vector3 _velocity = _navMeshAgent.velocity; //On r�cup�re la v�locit� du navMeshagent.
            Vector3 _localeVelocity = transform.InverseTransformDirection(_velocity);//On converti cette v�locit� en v�locit� locale grace � "InverseTransformDirection()".
            float _speed = _localeVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", _speed); //On applique la vitesse au Blendtree.
        }

        public void StartMovementAction(Vector3 destination)
        {
            GetComponent<ActionManager>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.destination = destination; //La nouvelle destination du joueur est l� o� nous avons cliqu�.
            _navMeshAgent.isStopped = false;
        }

    }
}
