namespace CAT.TreeStructure
{
    internal class Info
    {
        public Info(string name, int level) => (Name, Level) = (name, level);

        public string Name { get; }
        public int Level { get; }

        public override bool Equals(object? obj)
        {
            if (obj is Info info)
            {
                return info.Name == Name;
            }

            return false;
        }

        public override int GetHashCode() => Name.GetHashCode();
    }
}
