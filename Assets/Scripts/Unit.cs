using UnityEngine;

[RequireComponent(typeof(MoveAction))]
public class Unit : MonoBehaviour
{

    private SpinAction _spinAction;
    private MoveAction _moveAction;
    private GridPosition _gridPosition;
    private BaseAction[] _baseActionArray;
    [SerializeField] private int _actionPoints;

    public int ActionPoints { private set { _actionPoints = value; } get { return _actionPoints; } }
    public BaseAction[] Actions { private set { _baseActionArray = value; } get { return _baseActionArray; } }
    public GridPosition GridPosition { get { return _gridPosition; } }
    public MoveAction MoveAction { get { return _moveAction; } }
    public SpinAction SpinAction { get { return _spinAction; } }


    // Start is called before the first frame update
    private void Awake()
    {
        _spinAction = GetComponent<SpinAction>();
        _moveAction = GetComponent<MoveAction>();
        _baseActionArray = GetComponents<BaseAction>();
    }

    private void Start()

    {
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_gridPosition, this);
    }

    // Update is called once per frame
    void Update()
    {
        
       

        var newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(_gridPosition != newGridPosition) 
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, _gridPosition, newGridPosition);
            _gridPosition = newGridPosition;

        }
        
    }

    

    public override string ToString()
    {
        return name;
    }

    private bool CanSpendActionPoints(BaseAction action) 
    {
        return (_actionPoints >= action.ActionPointsCost);
    }
    
    private void SpendActionPoints(BaseAction action) 
    {
        _actionPoints -= action.ActionPointsCost;
    }

    public bool TrySpendActionPoints(BaseAction action) 
    {
        if(CanSpendActionPoints(action)) 
        {
            SpendActionPoints(action);
            return true;
        }
        return false;
    }
}
