using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Animator _animator;
    private int _paramID;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_animator == null)
            return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            _animator.SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-_speed * Time.deltaTime, 0, 0);
            _animator.SetBool("run", true);
        }
        else
        {
            transform.Translate(0, 0, 0);
            if (_animator.GetBool("run"))
                _animator.SetBool("run", false);
        }

    }
}
