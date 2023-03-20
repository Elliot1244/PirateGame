using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMouvement : MonoBehaviour
{
    [SerializeField] Transform _target;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Lors du click gauche de la souris
        {
            MoveTowardcursor();
        }
    }

    private void MoveTowardcursor()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Récupération de la localisation de la souris lors du click.
        RaycastHit _hit;
        bool _hasHit = Physics.Raycast(_ray, out _hit);
        if(_hasHit == true)
        {
            GetComponent<NavMeshAgent>().destination = _hit.point; //La nouvelle destination du joueur est là où nous avons cliqué.
        }
    }

        
}
