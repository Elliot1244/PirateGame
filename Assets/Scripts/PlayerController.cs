using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        private void Update()
        {
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
                if (target == null) continue; //Si l'it�ration actuelle du tableau _hits n'est pas une target, passe � la prochaine it�ration.

                if(Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target); //On utiliser la fonction Attack pour attaquer la cible.
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
            return Camera.main.ScreenPointToRay(Input.mousePosition); //R�cup�ration de la localisation de la souris lors du click.
        }
    }
}
