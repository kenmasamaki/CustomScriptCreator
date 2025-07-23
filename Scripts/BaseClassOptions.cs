using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Custom Script Creator/Base Class Options")]
public class BaseClassOptions : ScriptableObject
{
	[Tooltip("基底クラスの選択肢を定義します。\n" +
		"継承クラス名を候補として登録しておくことができます。\n" +
		"※No Base Class は、クラスを継承しないスクリプトを作成するためのオプションです。")]
	public string[] baseClassOptions = { "MonoBehaviour", "No Base Class" };
}