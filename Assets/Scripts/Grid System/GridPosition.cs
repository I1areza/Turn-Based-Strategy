public struct GridPosition
{
    public int x; public int z;

    public GridPosition(int x, int z)
    {
        this.x = x; this.z = z;
    }

    public override string ToString()
    {
        return $"<color=#23D00E><b>x: {x}; z: {z}</b></color>";
    }
}