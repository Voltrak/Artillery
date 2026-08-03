using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cañon : MonoBehaviour
{
    [SerializeField] private GameObject BalaPrefab;
    private GameObject puntaCanon;
    private float rotacion;

    // Variables para memorizar la alineación original y no desarmar el modelo
    private float rotacionYOriginal;
    private float rotacionZOriginal;

    private void Start()
    {
        puntaCanon = transform.Find("PuntaCanon").gameObject;
        
        // Tomamos la rotación inicial exacta que tiene en Unity
        rotacion = transform.eulerAngles.x;
        rotacionYOriginal = transform.eulerAngles.y;
        rotacionZOriginal = transform.eulerAngles.z;

        // Ajuste matemático: Unity a veces lee ángulos negativos como números mayores a 300
        if (rotacion > 180f) rotacion -= 360f; 
    }

    void Update()
    {
        // CAMBIO 1: Cambiamos += por -= para invertir los controles y que se sientan naturales
        rotacion -= Input.GetAxis("Horizontal") * AdministradorJuego.VelocidadRotacion;
        
        if (rotacion > 90) rotacion = 90;
        if (rotacion < 0) rotacion = 0;

        // CAMBIO 2: Usamos las rotaciones Y y Z originales en lugar de forzar el "90" y "0"
        transform.eulerAngles = new Vector3(rotacion, 90f, 90f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject temp = Instantiate(BalaPrefab, puntaCanon.transform.position, transform.rotation);
            Rigidbody tempRB = temp.GetComponent<Rigidbody>();
            Vector3 direccionDisparo = transform.rotation.eulerAngles;
            direccionDisparo.y = 90 - direccionDisparo.x;
            tempRB.linearVelocity = direccionDisparo.normalized * AdministradorJuego.VelocidadBala;
        }
    }
}