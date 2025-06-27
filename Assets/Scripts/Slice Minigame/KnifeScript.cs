using com.marufhow.meshslicer.core;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class KnifeScript : MonoBehaviour
{
    [SerializeField] private MinigameKnifeController controllerRef;
    [SerializeField] private MHCutter mhCutter;
    [SerializeField] private GameObject outlineGO;
    [SerializeField] private GameObject slicedOffParent;
    [SerializeField] private float sideMoveSpeed = 3f;      
    [SerializeField] private float sideMaxDistance = 2f;    
    [SerializeField] private float verticalMoveSpeed = 1.5f;      
    [SerializeField] private float verticalMaxDistance = 3f;

    private GameObject breadGO;
    private bool playerInControl = false;
    private bool slicing = false;
    private Stopwatch sideTime;
    private Stopwatch verticalTime;
    private Vector3 startPosition;
    private IEnumerator MoveRoutine = null;

    public void GainControl(GameObject bread)
    {
        breadGO = bread;
        StartMoving();
        playerInControl = true;
    }

    public void LoseControl()
    {
        StopAllMoving();
        playerInControl = false;
    }

    private void Start()
    {
        InitializeReferences();
    }

    private void Update()
    {
        if(!playerInControl)
        {
            return;
        }
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && !slicing) // LMB
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

        slicing = true;

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

        slicing = false;
        StopAllMoving();
        StartMoving();
    }

    private IEnumerator SliceRoutine()
    {
        yield return new WaitForSeconds(0.7f);
        mhCutter.Cut(breadGO, transform.position, Vector3.right, slicedOffParent);
        playerInControl = false;

        float maxScore = 10f;
        float maxDistance = 0.5f;
        float distance = Mathf.Abs(transform.position.x - outlineGO.transform.position.x);

        int score = Mathf.RoundToInt(Mathf.Clamp01(1f - (distance / maxDistance)) * maxScore);

        controllerRef.GetPoints(score);
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
