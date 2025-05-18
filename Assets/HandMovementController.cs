using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class HandMovementController : MonoBehaviour
{
    //dodaj nowy obiekt - skrypt goni¹cy zawsze kursor myszy
    //model dloni musi gonic tamten element albo trzymany przedmiot
    //trzymany przedmiot musi gonic tamten element na podsstawie spring jointa

    public float moveSpeed = 20f;

    public GameObject cursorObject;
    public GameObject handModel;
    public Camera mainCamera;
    public Rigidbody handRigidbody;

    private Joint jointRef;
    private GrabbableObject heldGrabbable;

    private Vector2 mousePosition;
    private Vector3 startPos;
    private Vector3 startScale;
    private Quaternion startRot;

    private bool inControl = false;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        startPos = transform.position;
        startRot = transform.rotation;
        startScale = transform.lossyScale;
        handRigidbody = GetComponent<Rigidbody>();

        inControl = true;
    }

    void Update()
    {
        CheckGrabbing();
    }

    void FixedUpdate()
    {
        MoveHand();
    }

    private void MoveHand()
    {
        if (!inControl || heldGrabbable != null)
        {
            return;
        }

        Vector3 targetPosition = new Vector3(cursorObject.transform.position.x, cursorObject.transform.position.y, startPos.z);
        Vector3 newPosition = Vector3.MoveTowards(handRigidbody.position, targetPosition, moveSpeed * Time.deltaTime);
        handRigidbody.MovePosition(newPosition);
    }

    void CheckGrabbing()
    {
        if (!inControl)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("chwytam");
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Grabbable"))
                {
                    GrabObject(hit.collider.gameObject.GetComponent<GrabbableObject>());
                }
                else
                {
                    Debug.Log("Trafiono obiekt, ale z innym tagiem: " + hit.collider.tag);
                }
            }
            else
            {
                Debug.Log("Pusta ³apa");
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(heldGrabbable != null)
            {
                DropObject(heldGrabbable);
            }
        }
    }

    private void GrabObject(GrabbableObject obj)
    {
        Debug.Log("Trafiono obiekt z tagiem 'Grabbable': " + obj.transform.name);
        heldGrabbable = obj;
        heldGrabbable.Grabbed(this);
        transform.position = new Vector3(transform.position.x, transform.position.y, obj.transform.position.z - 0.5f); // to tez, aby zawsze blizej kamery bylo
        transform.parent = obj.transform;
        Vector3 objScale = obj.transform.lossyScale;
        transform.localScale = new Vector3(
            startScale.x / objScale.x,
            startScale.y / objScale.y,
            startScale.z * objScale.z//ta skala jest z zle zrobiona, trzeba ostatecznie poprawic
        );
        handRigidbody.mass = 0;
        handRigidbody.isKinematic = true;
    }

    private void DropObject(GrabbableObject obj)
    {
        Debug.Log("Puszczam: " + obj.transform.name);
        heldGrabbable.Dropped();
        heldGrabbable = null;
        transform.parent = null;
        handRigidbody.mass = 1;
        handRigidbody.isKinematic = false;
        transform.position = transform.position + new Vector3(0, 0, startPos.z);
        transform.rotation = startRot;
        transform.localScale = startScale;
    }
}