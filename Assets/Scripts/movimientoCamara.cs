using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoCamara : MonoBehaviour
{
    //Define la zona muerta(por defecto un 50%)
    [Range(0f, 1f)] public float deadZoneFactor = 0.5f;
    [Range(0f, 1f)] public float smoothFactor = 0.1f;

    //es la mitad de la altura y anchura de la dead zone
    private float deadZoneHeight, deadZoneWidth;

    private Transform target;
    private Bounds limiteZonaMuerta;

    // Start is called before the first frame update
    void Start()
    {
        deadZoneHeight = Camera.main.orthographicSize * deadZoneFactor;
        deadZoneWidth = deadZoneHeight * Camera.main.aspect;
        limiteZonaMuerta = new Bounds(transform.position, new Vector3(deadZoneWidth * 2, deadZoneHeight * 2, 1f));

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        limiteZonaMuerta = new Bounds(transform.position, new Vector3(deadZoneWidth * 2, deadZoneHeight * 2, 1f));
        float incrX, incrY;
        if (!IsIndeadZone(out incrX, out incrY))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(incrX, incrY, 0f), smoothFactor);
        }



    }

    private bool IsIndeadZone(out float incrX, out float incrY)
    {
        limiteZonaMuerta.center = transform.position;
        if (!limiteZonaMuerta.Contains(target.position))
        {
            //guardanos el punto mas cercano del target al cubo
            Vector3 closestPoint = limiteZonaMuerta.ClosestPoint(target.position);
            incrX = target.position.x - closestPoint.x;
            incrY = target.position.y - closestPoint.y;

            return false;
        }
        incrX = 0;
        incrY = 0;
        return true;

    }
    private void OnDrawGizmosSelected()
    {
        deadZoneHeight = Camera.main.orthographicSize * deadZoneFactor;
        deadZoneWidth = deadZoneHeight * Camera.main.aspect;

        //Dibujara una linea que significara la zona muerta.La primera es para darle un color y la segunda es para dibujar una linea
        Gizmos.color = Color.magenta;

        // Gizmos.DrawLine(transform.position, transform.position + new Vector3(0f, deadZoneHeight, 0f));

        Gizmos.DrawWireCube(transform.position, new Vector3(2 * deadZoneWidth, deadZoneHeight * 2.0f));

    }
}
