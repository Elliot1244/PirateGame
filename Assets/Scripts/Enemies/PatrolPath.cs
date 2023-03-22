using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {

        const float _wayPointRaduis = 0.1f;
        private void OnDrawGizmos()
        {
           for(int i = 0; i < transform.childCount; i++ )
            {
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(transform.GetChild(i).position, _wayPointRaduis);
            }
        }
    }

}