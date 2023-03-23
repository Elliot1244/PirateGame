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
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNexIndex(i);
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(GetWayPoint(i), _wayPointRaduis);
                Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));
            }
        }

        public  int GetNexIndex(int i)
        {
            if(i + 1 == transform.childCount)
            {
                return 0;                       //Si l'index i est égal au nombre de waypoints total, alors on le remet à 0.
            }
            return i + 1;
        }

        public Vector3 GetWayPoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }

}