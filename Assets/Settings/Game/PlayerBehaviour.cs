using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Animator animator;
    
    public NextBall nextball;
    public byte angle;

    private GameObject throwObject;
    private Vector2 newPosition;
    private byte type;

    public float cooldownTime = 0.25f;
    private bool isCooldown = false;


    void Start()
    {
        angle = 90;
        nextball.ThrowBall(angle, newPosition + new Vector2(0, 0.278f), type);
    }

    void Update()
    {
        ThrowBall();
        CannonRotation();
    }
    Vector3 CalculateTipPosition(float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        float x = transform.position.x + Mathf.Cos(angleRad) * 0.2f;
        float y = transform.position.y + Mathf.Sin(angleRad) * 0.2f;
        return new Vector2(x, y);
    }
    private void ThrowBall()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (throwObject == null)
            {
                newPosition = CalculateTipPosition(angle);
                type = nextball.GetRandomObjectIndex();
                throwObject = nextball.ThrowBall(angle, newPosition + new Vector2(0, 0.278f), type);
                Destroy(throwObject, 2);
            }
        }
    }
    private void CannonRotation()
    {
        
        if (Input.GetKey(KeyCode.D) && angle > 0 && !isCooldown)
        {
            StartCoroutine(Cooldown());
            angle -= 10;
        }
            
        if (Input.GetKey(KeyCode.A) && angle < 180 && !isCooldown)
        {
            StartCoroutine(Cooldown());
            angle += 10; }

        animator.SetInteger("Rotation", angle);
    }

    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

}
