using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMouvement : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) //Lors du click gauche de la souris
        {
            MoveTowardcursor();
        }

        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        Vector3 _velocity = GetComponent<NavMeshAgent>().velocity; //On récupère la vélocité du navMeshagent.
        Vector3 _localeVelocity = transform.InverseTransformDirection(_velocity);//On converti cette vélocité en vélocité locale grace à "InverseTransformDirection()".
        float _speed = _localeVelocity.z;
        GetComponent<Animator>().SetFloat("ForwardSpeed", _speed); //On applique la vitesse au Blendtree.
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
