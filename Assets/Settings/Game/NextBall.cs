using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBall : MonoBehaviour
{
    public List<GameObject> objectList = new List<GameObject>();
    private GameObject previewObject;
    private byte randomObjectIndex;

    public GameObject CreatePreviewBall()
    {
        randomObjectIndex = (byte)Random.Range(0, objectList.Count);
        GameObject newPreviewObject = Instantiate(objectList[randomObjectIndex], transform.position, Quaternion.identity);
        return newPreviewObject;
    }

    public GameObject ThrowBall(byte angle, Vector2 position, byte type)
    {
        Destroy(previewObject);

        GameObject newThrowObject = Instantiate(objectList[type], position, Quaternion.identity);

        Rigidbody2D rb = newThrowObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        CircleCollider2D cc = newThrowObject.AddComponent<CircleCollider2D>();


        float angleRad = angle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        rb.AddForce(direction * 10f, ForceMode2D.Impulse);

        // Establecer el nuevo objeto como la previsualización actual
        previewObject = CreatePreviewBall();

        return newThrowObject;
    }
    
    public byte GetRandomObjectIndex()
    {
        return this.randomObjectIndex;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si la colisión es con un objeto que tiene un BoxCollider2D
        if (collision.gameObject.GetComponent<BoxCollider2D>() != null)
        {
            // Obtener el normal de la colisión y la velocidad actual del Rigidbody2D
            Vector2 normal = collision.contacts[0].normal;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            // Calcular la velocidad reflejada usando Vector2.Reflect
            Vector2 reflectedVelocity = Vector2.Reflect(rb.velocity, normal);

            // Aplicar la velocidad reflejada al Rigidbody2D del objeto actual
            rb.velocity = reflectedVelocity;
        }
    }
}

