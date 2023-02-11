using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Unity.VisualScripting;
using UnityEngine;

public class WireDetector : MonoBehaviour
{
    [SerializeField] private Grabbable _grabbable;
    [SerializeField] private List<MeshRenderer> _renderers;
    [SerializeField] private CheckPointSystem _checkPointSystem;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Vector3 _initialScale;

    private void OnEnable()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _initialScale = transform.localScale;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WireStick"))
        {
           _checkPointSystem.CheckWaypoint(other.gameObject);
        }
        
        if (other.CompareTag("Wire"))
        {
            Debug.Log("Stay");
            foreach (var render in _renderers)
            {
                render.material.color = Color.red;
            }
            _grabbable.enabled = false;
            
            transform.position = _initialPosition;
            transform.rotation = _initialRotation;
            transform.localScale = _initialScale;
            
            _checkPointSystem.ResetWayPoint();
            
            StartCoroutine(WaitToReset());
        }
        
    }

    private IEnumerator WaitToReset()
    {
        yield return new WaitForSeconds(0.5f);
        _grabbable.enabled = true;
        foreach (var render in _renderers)
        {
            render.material.color = Color.white;
        }
    }
}
