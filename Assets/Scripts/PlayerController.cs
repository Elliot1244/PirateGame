using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health _health;

        private void Start()
        {
            _health = GetComponent<Health>();
        }
        private void Update()
        {
            if (_health.IsDead()) return;
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
            Debug.Log("C'est le vide");
        }

        

        private bool InteractWithCombat()
        {
            RaycastHit[] _hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in _hits)
            {
               CombatTarget target = hit.transform.GetComponent<CombatTarget>(); //Les cibles potentielles sont des GameObjects avec le composant "CombatTarget".
                
                if(target == null)
                {
                    continue;
                }

                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                if(Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject); //On utiliser la fonction Attack pour attaquer la cible.
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit _hit;
            bool _hasHit = Physics.Raycast(GetMouseRay(), out _hit);
            if (_hasHit == true)
            {
                if(Input.GetKey(KeyCode.Mouse0))
                {
                    GetComponent<PlayerMouvement>().StartMovementAction(_hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition); //Récupération de la localisation de la souris lors du click.
        }
    }
}
