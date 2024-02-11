using UnityEngine;

public class Unit : MonoBehaviour
{

    private Vector3 _targetPosition;
    private float _stoppingDistance = .1f;
    private float moveSpeed = 4f;
    private float rotationSpeed = 5f;
    private GridPosition _gridPosition;

    [SerializeField] private Animator unitAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        _targetPosition = transform.position;
    }

    private void Start()
    {
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_gridPosition, this);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(transform.position, _targetPosition)>_stoppingDistance)
        {
            var moveDirection = (_targetPosition - transform.position).normalized;
            transform.position += moveSpeed * Time.deltaTime * moveDirection;
            unitAnimator.SetBool("IsWalking", true);
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }

        var newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(_gridPosition != newGridPosition) 
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, _gridPosition, newGridPosition);
            _gridPosition = newGridPosition;

        }
        
    }

    public void Move(Vector3 targetPosition)
    {
        this._targetPosition = targetPosition;
    }

    public override string ToString()
    {
        return name;
    }
}
