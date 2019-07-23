public class PlayerNamesUIParameters : UIParameters
{
    public string ParentName;
    public string ChildName;

    public PlayerNamesUIParameters(string parentName, string childName)
    {
        ParentName = parentName;
        ChildName = childName;
    }
}