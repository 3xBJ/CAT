namespace CAT.GUI.Functionalities.Analyzers.Drawables.Util;

internal class DrawableNode
{
    public DrawableNode(string name, string prettyName, int x, int y)
    {
        FullName = name;
        PrettyName = prettyName;
        Coordinates.X = x;
        Coordinates.Y = y;
    }

    public string FullName { get; }
    public string PrettyName { get; }

    public Coordinates Coordinates { get; } = new Coordinates();

    public HashSet<DrawableNode> PointTo { get; } = new HashSet<DrawableNode>();
    public HashSet<DrawableNode> Pointed { get; } = new HashSet<DrawableNode>();

    public override bool Equals(object obj)
    {
        if (obj is string name)
        {
            return FullName == name;
        }
        else if (obj is DrawableNode dNode)
        {
            return FullName == dNode.FullName;
        }

        return false;
    }

    public override int GetHashCode() => FullName.GetHashCode();
}
