using UnityEngine;

public class Pipe : MonoBehaviour
{
    private ConnectionPuzzle _connectionPuzzle;
    private int _row;
    private int _column;
    private PipeOrientationType _orientation = PipeOrientationType.Up;

    void Awake()
    {


        switch (transform.localEulerAngles.z)
        {
            case 0f:  SetOrientation(PipeOrientationType.Up); break;
            case 90f:  SetOrientation(PipeOrientationType.Right); break;
            case 180f:  SetOrientation(PipeOrientationType.Down); break;
            case 270f:  SetOrientation(PipeOrientationType.Left); break;
            default: break;
        }
    }

    public void Setup(ConnectionPuzzle connectionPuzzle, int row, int column)
    {
        _connectionPuzzle = connectionPuzzle;
        _row = row;
        _column = column;
    }

    public void Selected()
    {
        RotatePipe();
        _connectionPuzzle.PipePressed(_row, _column, _orientation);
    }

    private void RotatePipe()
    {
        switch (_orientation)
        {
            case PipeOrientationType.Left: SetOrientation(PipeOrientationType.Up); SetRotation(0f); break;
            case PipeOrientationType.Up:  SetOrientation(PipeOrientationType.Right); SetRotation(90f); break;
            case PipeOrientationType.Right:  SetOrientation(PipeOrientationType.Down); SetRotation(180f); break;
            case PipeOrientationType.Down:  SetOrientation(PipeOrientationType.Left); SetRotation(270f); break;
        }
    }

    private void SetOrientation(PipeOrientationType orientation) { _orientation = orientation; }

    public void SetRotation(float angle) { transform.localEulerAngles = new Vector3(0f, 0f, angle); }
}
