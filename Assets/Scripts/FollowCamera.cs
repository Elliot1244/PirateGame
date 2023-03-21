using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform _target;

        void LateUpdate()
        {
            transform.position = _target.position;
        }
    }

