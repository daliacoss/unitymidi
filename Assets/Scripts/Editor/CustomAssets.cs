using UnityEditor;

public static class CustomAssets
{
    [MenuItem("Assets/DIRTBAGS/Create MidionLevel")]
    public static void CreateMidionLevel()
    {
        CustomAssetUtility.CreateAsset<MidionLevel>();
    }
}
