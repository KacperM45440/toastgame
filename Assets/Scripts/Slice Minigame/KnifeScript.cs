using com.marufhow.meshslicer.core;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class KnifeScript : MonoBehaviour
{
    [SerializeField] private MHCutter mhCutter;
    [SerializeField] private GameObject breadGO;
    [SerializeField] private GameObject outlineGO;
    [SerializeField] private float sideMoveSpeed = 3f;      
    [SerializeField] private float sideMaxDistance = 2f;    
    [SerializeField] private float verticalMoveSpeed = 1.5f;      
    [SerializeField] private float verticalMaxDistance = 3f;

    private Stopwatch sideTime;
    private Stopwatch verticalTime;
    private Vector3 startPosition;
    private IEnumerator MoveRoutine = null;

    private void Start()
    {
        InitializeReferences();
        StartMoving();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0)) // LMB
        {
            StopAllMoving();
            StartSlicing();
        }
    }

    private void InitializeReferences()
    {
        sideTime = new();
        verticalTime = new();
        startPosition = transform.position;
    }

    private void HideOutline()
    {
        outlineGO.SetActive(false);
    }

    private void StartMoving()
    {
        if(MoveRoutine != null)
        {
            return;
        }

        sideTime.Start();
        MoveRoutine = MovingCoroutine();
        StartCoroutine(MoveRoutine);
    }

    private void StopAllMoving()
    {
        StopCoroutine(MoveRoutine);
        MoveRoutine = null;
        sideTime.Stop();
        verticalTime.Stop();
        verticalTime.Reset();
    }

    private void StartSlicing()
    {
        if (MoveRoutine != null)
        {
            return;
        }

        float score = Mathf.Abs(transform.position.x - outlineGO.transform.position.x); //todo: add score
        Debug.Log(score);
        HideOutline();

        verticalTime.Start();
        MoveRoutine = MoveDownRoutine();        
        StartCoroutine(MoveRoutine);
        StartCoroutine(SliceRoutine());
    }

    private IEnumerator MoveDownRoutine()
    {
        while (verticalTime.Elapsed.TotalSeconds <= 2.1f)
        {
            float offset = Mathf.Abs(Mathf.Sin((float)(verticalTime.Elapsed.TotalSeconds * verticalMoveSpeed))) * verticalMaxDistance;
            //transform.position = new Vector3(transform.position.x, startPosition.y - offset, transform.position.z);
            GetComponent<Rigidbody>().position = new Vector3(transform.position.x, startPosition.y - offset, transform.position.z);
            yield return null;
        }
       
        StopAllMoving();
        StartMoving();
    }

    private IEnumerator SliceRoutine()
    {
        yield return new WaitForSeconds(0.7f);
        mhCutter.Cut(breadGO, transform.position, Vector3.right);
    }

    private IEnumerator MovingCoroutine()
    {
        while(true) //todo: change to while game not over
        {
            float offset = Mathf.Sin((float)(sideTime.Elapsed.TotalSeconds * sideMoveSpeed)) * sideMaxDistance;
            //transform.position = startPosition + new Vector3(offset, 0f, 0f);
            GetComponent<Rigidbody>().position = startPosition + new Vector3(offset, 0f, 0f);
            yield return null;
        }
    }
}
