using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.UI;

public class TestOfAngles : MonoBehaviour
{
    public CharacterController player;

    public float speed = 5f;
    public float rotSpeed = 5f;
    
    private Ray m_RayLeft;
    private Ray m_RayRight;
    private Ray m_RayFront;

    public RaycastHit defRayInfo;
    
    private RaycastHit info_RayLeft;
    private RaycastHit info_RayRight;
    private RaycastHit info_RayFront;
    public float angle;
    public float walldir;

    public LayerMask lm;
    void Update()
    {
        m_RayFront = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        m_RayLeft = new Ray(transform.position, transform.TransformDirection(Vector3.left));
        m_RayRight = new Ray(transform.position, transform.TransformDirection(Vector3.right));
        
        //OBRACANIE
        float x;
        if (Input.GetKey(KeyCode.Q))
        {
            x = -1;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            x = 1;
        }
        else
        {
            x = 0;
        }
        
        transform.Rotate(0f, x * Time.deltaTime * rotSpeed, 0f);
        player.Move(new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")) * Time.deltaTime * speed);

        Physics.Raycast(m_RayRight, out info_RayRight);
        Physics.Raycast(m_RayLeft, out info_RayLeft);
        Physics.Raycast(m_RayFront, out info_RayFront);

        if (Physics.Raycast(m_RayRight, out info_RayRight, 1f, lm))
        {
            walldir = 1;
            angle = Vector3.Angle(info_RayFront.normal, transform.position);
        }
        else if (Physics.Raycast(m_RayLeft, out info_RayLeft, 1f, lm))
        {
            walldir = -1;
            angle = Vector3.Angle(info_RayLeft.normal, transform.position);
        }
        else
        {
            walldir = 0;
            angle = 0;
        }

        
        Debug.Log(angle);
        
        //TODO: kurwa mać chciałbym to zrobić ale chuj wie czemu to do kurwy jebanej nie działa, trza obliczyc kąt może to pomoze okreslic pozycje wzgledem sciany
        
        
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right), Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left), Color.black);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.blue);
    }
}
