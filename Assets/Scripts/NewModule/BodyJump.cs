using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyJump : MonoBehaviour
{
    [SerializeField] private float _power;
    [SerializeField] private float _groundDist;
    [SerializeField] private float _maxDistToLeg;
    private Rigidbody _rb;
    private LegPositioning[] _legs;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null) Debug.LogError("No rigid body found");
        _legs = FindObjectsOfType<LegPositioning>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded() && LegsInTheAir())
            {
                Vector3 dir1 = (transform.position - _legs[0].transform.position).normalized;
                dir1.z = 0;
                Vector3 dir2 = (transform.position - _legs[1].transform.position).normalized;
                dir2.z = 0;
                Vector3 endDir = (Vector3.Lerp(dir1, dir2, 0.5f)).normalized;
                _rb.AddForce(endDir * _power, ForceMode.Impulse);
            }
        }
    }

    private bool LegsInTheAir()
    {
        for (int i = 0; i < _legs.Length; i++)
        {
            if (_legs[i].GetGrounded()) return false;
        }
        return true;
    }
    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _groundDist);
    }
    public float GetMaxDist() => _maxDistToLeg;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -_groundDist, 0));
    }
}
