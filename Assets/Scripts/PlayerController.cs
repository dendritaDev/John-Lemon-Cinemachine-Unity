using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;
    private Animator _animator;
    private Rigidbody _rigidBody;
    private AudioSource _audioSource;

    [SerializeField]
    private float turnSpeed;
    private float walkSpeed = 1.5f;
    
    private Quaternion rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }


    private void OnAnimatorMove()
    {
        //Posicion: Posicion actual + direccion nueva * delta de posicion de la animacion
        //Esta es la manera de sincronizar velocidad de animacion y player. En funcion de la velocidad de la animación, se moverá más rápido
        //Por tanto lo que podemos hacer es añadir un multiplier a la speed de la animation y modificar eso si queremos aumentar/disminuir la velocidad del player y la animacion
        _rigidBody.MovePosition(_rigidBody.position + movement * _animator.deltaPosition.magnitude);

        _rigidBody.MoveRotation(rotation);

        _animator.SetFloat("Speed Modifier", walkSpeed);
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement.Set(horizontal, 0, vertical);
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        _animator.SetBool("IsWalking", isWalking);

        if(isWalking)
        {
            if(!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.fixedDeltaTime, 0);

        rotation = Quaternion.LookRotation(desiredForward);
    }
}
