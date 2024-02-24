using UnityEngine;

[RequireComponent(typeof(MoveAction))]
public class Unit : MonoBehaviour
{

    private SpinAction _spinAction;
    private MoveAction _moveAction;
    private GridPosition _gridPosition;
    private BaseAction[] _baseActionArray;

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

    
}
