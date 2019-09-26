using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Slider: ScriptableObject
{
	public string sliderName;
	public int reqdLvl;
	public int hitPoint;
	public int attack;
	public int defence;
	public int endurance;
	public int ballSliderModY;
	public int normalModX;
	public int hitModX;
	public int onHitModX;
	public List<Element> elementProperties;
	public GameObject theSlider;
}