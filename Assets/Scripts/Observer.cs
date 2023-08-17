using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Observer : MonoBehaviour
{

    public Transform player;

    private bool isPlayerInRange;

    public GameEnding gameEnding;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            isPlayerInRange = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if(isPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up; //le añadimos el vector up pq el transform del player esta en los pies Y como tiene 1 meto y poco de altura pues le sumamos un v3.up

            Ray ray = new Ray(transform.position, direction);

            Debug.DrawRay(transform.position, direction, 
                Color.green, Time.deltaTime, true); //true es para que el rayo si pasa algun objeto se vea como mas oscurecido

            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit))
            //comprobamos si hay algo entre ese raycast que se ejecuta, le damos el rayo y le pasamos raycastHit
            //Lo que hará la funcion por un lado es comprobar si el ray choca con algo, que se podria hacer solo con: if(Physics.Raycast(ray)
            //Y, además, en caso de ser así, nos modificará la variable raycastHit que le hemos enviado:
            //Creo que sería mejorar práctica definir el raycasthit directamente como parámetro: if(Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if(raycastHit.collider.transform == player)
                {
                    gameEnding.CatchPlayer();
                }
            } 


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 0.1f);

        Gizmos.DrawLine(transform.position, player.position + Vector3.up);
    }

}
