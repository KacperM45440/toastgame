using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandMovementController : MonoBehaviour
{
    //dodaj nowy obiekt - skrypt goni¹cy zawsze kursor myszy
    //model dloni musi gonic tamten element albo trzymany przedmiot
    //trzymany przedmiot musi gonic tamten element na podsstawie spring jointa

    public float moveSpeed = 20f;

    public GameObject handModel;
    public Camera mainCamera;
    public Rigidbody handRigidbody;

    private Joint jointRef;
    private GrabbableObject heldGrabbable;

    private Vector2 mousePosition;

    private Vector3 startPos;

    private bool inControl = false;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        startPos = transform.position;
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
        if (!inControl)
        {
            return;
        }

        Vector3 targetPosition;
        if (heldGrabbable == null)
        {
            mousePosition = Input.mousePosition;
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(
                mousePosition.x,
                mousePosition.y,
                Mathf.Abs(mainCamera.transform.position.z - handRigidbody.position.z)
            ));

            targetPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, startPos.z);
        }
        else
        {
            targetPosition = new Vector3(heldGrabbable.transform.position.x, heldGrabbable.transform.position.y, heldGrabbable.transform.position.z - 0.5f);
        }

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
                // Sprawdza, czy trafiony obiekt ma tag 'test'
                if (hit.collider.CompareTag("Grabbable"))
                {
                    heldGrabbable = hit.collider.gameObject.GetComponent<GrabbableObject>();
                    heldGrabbable.Grabbed(this);
                    gameObject.transform.parent = heldGrabbable.mainRb.transform;
                    handRigidbody.isKinematic = true;
                    Debug.Log("Trafiono obiekt z tagiem 'Grabbable': " + hit.collider.name);
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
                heldGrabbable.Dropped();
                heldGrabbable = null;
                gameObject.transform.parent = null;
                handRigidbody.isKinematic = false;
                Debug.Log("puszczam");
            }
        }
    }
}