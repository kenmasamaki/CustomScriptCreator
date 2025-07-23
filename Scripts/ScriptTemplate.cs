using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Custom Script Creator/Script Template")]
public class ScriptTemplate : ScriptableObject
{
	[Tooltip("スクリプトテンプレートの名前を指定します。")]
	public string templateName = "Default";

	[TextArea(5, 20), Tooltip("" +
		"スクリプトのテンプレート内容を定義します。 " +
		"#CLASSNAME# と #BASECLASS# は プレースホルダです。" +
		"プレースホルダは、スクリプト生成時に置き換えられます。")]
	public string templateBody =
@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class #CLASSNAME# : #BASECLASS#
{
    void Start()
    {
        // 初期化処理
    }

    void Update()
    {
        // 毎フレーム処理
    }
}";

	public string GenerateContent(string className, string baseClass, string ns)
	{
		string body = templateBody
			.Replace("#CLASSNAME#", className)
			.Replace("#BASECLASS#", baseClass);

		if (!string.IsNullOrEmpty(ns))
		{
			body = $"namespace {ns}\n{{\n{body}\n}}";
		}
		return body;
	}
}
