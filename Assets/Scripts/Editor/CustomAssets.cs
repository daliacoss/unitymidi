using UnityEditor;

public static class CustomAssets
{
    [MenuItem("Assets/Create/MidionLevel")]
    public static void CreateMidionLevel()
    {
        CustomAssetUtility.CreateAsset<MidionLevel>();
    }
}
