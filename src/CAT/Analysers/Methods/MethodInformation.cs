using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CAT.Analysers.Methods;

public class MethodInformation
{
    public MethodInformation(string fullName, List<MethodInformation> methodsCalled)
    {
        string[] splited = fullName.Split("::");
        Location = splited[0];
        Name = splited[1];
        CalledMethods = methodsCalled;
        FullName = fullName;
    }

    public MethodInformation(string fullName)
    {
        FullName = fullName;
        string[] util = fullName.Split("::");
        Location = util[0];
        Name = util[1];
        CalledMethods = new List<MethodInformation>();
    }

    [JsonPropertyName("FullName")]
    public string FullName { get; }

    /// <summary>
    /// Location of the method
    /// </summary>
    /// <remarks>
    /// Location is a combination of the namespace and the class name
    /// </remarks>
    [JsonIgnore]
    public string Location { get; }

    /// <summary>
    /// Method name
    /// </summary>
    [JsonIgnore]
    public string Name { get; }

    /// <summary>
    /// Method name
    /// </summary>
    [JsonIgnore]
    public string PrettyName
    {
        get
        {
            if (Name.StartsWith(".ctor"))
            {
                return Location.Split('.').Last();
            }

            return Name;
        }

    }
    /// <summary>
    /// Methods that <see cref="FullName"/> calls
    /// </summary>
    [JsonPropertyName("CalledMethods")]
    public List<MethodInformation> CalledMethods { get; }

    /// <summary>
    /// Methods that calls <see cref="FullName"/>
    /// </summary>
    [JsonPropertyName("CalledBy")]
    public HashSet<MethodInformation> CalledBy { get; } = new();

    /// <summary>
    /// Compares <paramref name="obj"/> whith <see cref="this"/>
    /// </summary>
    /// <param name="obj">A string containing the <see cref="FullName"/> or an <seealso cref="MethodInformation"/> instance</param>
    /// <returns>True if <paramref name="obj"/> is equals to this</returns>
    public override bool Equals(object? obj)
    {
        if (obj is string fullName)
        {
            return FullName == fullName;
        }
        else if(obj is MethodInformation methodInfo)
        {
            return methodInfo.FullName == FullName;
        }

        return false;
    }

    public override int GetHashCode() => FullName.GetHashCode();
}
