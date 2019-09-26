using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Element: ScriptableObject
{
	public string elementName;
	public bool isBaseElement;
	public int atkMOD;
	public int defMOD;
	public int endMOD;
	public List<Element> immuneTo;
	public List<Element> resistantTo;
	public List<Element> weakTo;
}