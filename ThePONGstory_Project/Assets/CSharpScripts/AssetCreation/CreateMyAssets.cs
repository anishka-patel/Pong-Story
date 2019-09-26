using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateMyAssets
{

    [MenuItem("Assets/Create/CustomAssets/MySliderList")]
    public static void CreateMySliderList()
    {
        SliderList asset = ScriptableObject.CreateInstance<SliderList>();

        AssetDatabase.CreateAsset(asset, "Assets/CreatedAssets/NewSliderList.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/CustomAssets/MyBallList")]
    public static void CreateMyBallList()
    {
        BallList asset = ScriptableObject.CreateInstance<BallList>();

        AssetDatabase.CreateAsset(asset, "Assets/CreatedAssets/NewBallList.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/CustomAssets/MyElement")]
    public static void CreateMyElement()
    {
        Element asset = ScriptableObject.CreateInstance<Element>();

        AssetDatabase.CreateAsset(asset, "Assets/CreatedAssets/NewElement.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/CustomAssets/MySlider")]
    public static void CreateMySlider()
    {
        Slider asset = ScriptableObject.CreateInstance<Slider>();

        AssetDatabase.CreateAsset(asset, "Assets/CreatedAssets/NewSlider.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/CustomAssets/MyBall")]
    public static void CreateMyBall()
    {
        Ball asset = ScriptableObject.CreateInstance<Ball>();

        AssetDatabase.CreateAsset(asset, "Assets/CreatedAssets/NewBall.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

	[MenuItem("Assets/Create/CustomAssets/MyEnemy")]
	public static void CreateMyEnemy()
	{
		Enemy asset = ScriptableObject.CreateInstance<Enemy>();

		AssetDatabase.CreateAsset(asset, "Assets/CreatedAssets/NewEnemy.asset");
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = asset;
	}

	[MenuItem("Assets/Create/CustomAssets/MyMinion")]
	public static void CreateMyMinion()
	{
		Minion asset = ScriptableObject.CreateInstance<Minion>();
		
		AssetDatabase.CreateAsset(asset, "Assets/CreatedAssets/NewMinion.asset");
		AssetDatabase.SaveAssets();
		
		EditorUtility.FocusProjectWindow();
		
		Selection.activeObject = asset;
	}

	[MenuItem("Assets/Create/CustomAssets/MyDarkSlider")]
	public static void CreateMyDarkSlider()
	{
		DarkSlider asset = ScriptableObject.CreateInstance<DarkSlider>();
		
		AssetDatabase.CreateAsset(asset, "Assets/CreatedAssets/NewDarkSlider.asset");
		AssetDatabase.SaveAssets();
		
		EditorUtility.FocusProjectWindow();
        
        Selection.activeObject = asset;
    }
}
