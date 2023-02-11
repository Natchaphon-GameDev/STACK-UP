using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class StackRay : MonoBehaviour
// {
//     [SerializeField] private float _distance;
//     [SerializeField] Vector3 _dir = Vector3.up;
//     [SerializeField] private LayerMask mask;
//
//     void FixedUpdate()
//     {
//         RaycastHit hit;
//         var direction = transform.TransformDirection(_dir) * _distance;
//         var ray = Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, mask);
//
//         Debug.DrawRay(transform.position, direction, Color.red);
//         Debug.Log(ray);
//         if (ray)
//         {
//             hit.transform.parent.GetChild(0).position = (transform.position + new Vector3(0,0.2f,0));
//         }
//     }
// }