#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using System.Linq;


/// <summary>
/// Unityエディタのカスタムウィンドウで、新しいスクリプトを作成するためのクラス。<para/>
/// このウィンドウでは、クラス名、継承するベースクラス、およびスクリプトテンプレートを選択できます。
/// </summary>
public class ScriptCreator : EditorWindow
{
	private string className = "NewCustomScript";
	private int baseClassIndex = 0;
	private string baseClassName = "MonoBehaviour";

	// ベースクラス選択肢管理
	private BaseClassOptions baseClassOptionsAsset;
	private string[] baseClassOptions = { "MonoBehaviour", "No Base Class" };

	// テンプレート管理
	private ScriptTemplate[] templates;


	// プルダウンか直接入力かの切り替え
	private bool useDropdownForBaseClass = true;

	// ScriptTemplateアセットの参照フィールド
	private ScriptTemplate selectedTemplate;



	/// <summary>
	/// エディタウィンドウを表示するメニューアイテム。
	/// </summary>
	[MenuItem("Tools/Custom Script Creator")]
	public static void ShowWindow()
	{
		GetWindow<ScriptCreator>("Custom Script Creator");
	}

	/// <summary>
	/// コンテキストメニューからウィンドウを表示する。
	/// </summary>
	[MenuItem("Assets/Create/Custom Script", false, 80)]
	public static void ShowWindowFromContextMenu()
	{
		ShowWindow();
	}

	private void OnEnable()
	{
		// ScriptTemplateアセットをすべてロード
		templates = AssetDatabase.FindAssets("t:ScriptTemplate")
			.Select(guid => AssetDatabase.LoadAssetAtPath<ScriptTemplate>(AssetDatabase.GUIDToAssetPath(guid)))
			.ToArray();

		// BaseClassOptionsアセットをロード（Assets直下に1つだけ配置する想定）
		string[] guids = AssetDatabase.FindAssets("t:BaseClassOptions");
		if (guids.Length > 0)
		{
			baseClassOptionsAsset = AssetDatabase.LoadAssetAtPath<BaseClassOptions>(AssetDatabase.GUIDToAssetPath(guids[0]));
			baseClassOptions = baseClassOptionsAsset.baseClassOptions;
		}
	}

	void OnGUI()
	{
		GUILayout.Label("新規スクリプト作成", EditorStyles.boldLabel);
		GUILayout.Space(10);

		// BaseClassOptionsアセットの参照フィールド（補足文付き）
		EditorGUILayout.BeginHorizontal();
		GUIContent baseClassOptionsLabel = new GUIContent(
			"継承クラス選択肢", "継承クラスの選択肢アセットを指定します。\n" +
			"このアセットは、継承クラスの候補を定義します。\n" +
			"アセットを作成する場合は、Unityのメニューから\n" +
			"「Assets>Create>ScriptableObjects>Custom Script Creator>Base Class Options」を選択してください。" );
		baseClassOptionsAsset = (BaseClassOptions)EditorGUILayout.ObjectField(baseClassOptionsLabel, baseClassOptionsAsset, typeof(BaseClassOptions), false);
		EditorGUILayout.EndHorizontal();
		if (baseClassOptionsAsset != null)
		{
			baseClassOptions = baseClassOptionsAsset.baseClassOptions;
		}
		GUILayout.Space(8);

		// ScriptTemplateアセットの参照フィールド（補足文付き）
		EditorGUILayout.BeginHorizontal();
		GUIContent templateLabel = new GUIContent(
			"テンプレートアセット", "使用するスクリプトテンプレートを指定します。\n" +
			"このアセットは、スクリプトのテンプレート内容を定義します。\n" +
			"アセットを作成する場合は、Unityのメニューから\n" +
			"「Assets>Create>ScriptableObjects>Custom Script Creator>Script Template」を選択してください。");
		selectedTemplate = (ScriptTemplate)EditorGUILayout.ObjectField(templateLabel, selectedTemplate, typeof(ScriptTemplate), false);
		EditorGUILayout.EndHorizontal();
		GUILayout.Space(8);

		// クラス名入力フィールド
		EditorGUILayout.BeginHorizontal();
		GUIContent classNameLabel = new GUIContent("クラス名", "新しく作成するクラス名を入力します。");
		className = EditorGUILayout.TextField(classNameLabel, className, GUILayout.Width(350));
		EditorGUILayout.EndHorizontal();
		GUILayout.Space(8);

		// 継承クラス選択 ＋「継承クラスを手入力する」チェックボックスを横並びに
		EditorGUILayout.BeginHorizontal();
		if (useDropdownForBaseClass)
		{
			GUIContent baseClassLabel = new GUIContent(
				"継承クラス", "継承する基底クラスをプルダウンから選択します。\n" +
				"No Base Classを選択すると、継承クラスは無しになります。");
			baseClassIndex = EditorGUILayout.Popup(baseClassLabel, baseClassIndex, baseClassOptions, GUILayout.Width(350));
			baseClassName = baseClassOptions.Length > 0 ? baseClassOptions[baseClassIndex] : "";
		}
		else
		{
			GUIContent baseClassLabel = new GUIContent(
				"継承クラス", "継承する基底クラス名を直接入力します。\n" +
				"「No Base Class」 と入力すると、継承クラスは無しになります。");
			baseClassName = EditorGUILayout.TextField(baseClassLabel, baseClassName, GUILayout.Width(350));
		}
		GUILayout.Space(5);

		// チェックボックスを右側に配置
		useDropdownForBaseClass = !EditorGUILayout.ToggleLeft(
			new GUIContent("継承クラスを手入力する", "ONで継承クラス名を直接入力、OFFでプルダウン選択"),
			!useDropdownForBaseClass,
			GUILayout.Width(180)
		);

		EditorGUILayout.EndHorizontal();
		GUILayout.Space(16);

		// 作成ボタン
		if (GUILayout.Button(new GUIContent("作成", "指定した内容で新しいスクリプトを作成します。"), GUILayout.Height(30)))
		{
			CreateScript();
		}
	}



	/// <summary>
	/// 選択されたフォルダに新しいスクリプトを作成する。
	/// </summary>
	void CreateScript()
	{
		string folderPath = GetSelectedPathOrFallback();
		string filePath = $"{folderPath}/{className}.cs";

		// 既に同名のスクリプトが存在する場合は警告を表示して処理を中断
		if (System.IO.File.Exists(filePath))
		{
			Debug.LogWarning($"スクリプト作成失敗: 既に同じ名前のスクリプトが存在します。\n{filePath}");
			return;
		}

		string content;
		// ScriptTemplateアセットが選択されていればそれを使う
		if (selectedTemplate != null)
		{
			content = selectedTemplate.GenerateContent(className, baseClassName, null); // nullまたは""で渡す
		}
		else
		{
			content = GenerateScriptContent(className, baseClassName);
		}

		System.IO.File.WriteAllText(filePath, content);
		AssetDatabase.Refresh();
	}



	/// <summary>
	/// 指定されたクラス名とベースクラス名を使用して、スクリプトの内容を生成する。
	/// </summary>
	/// <param name="className">生成するクラスの名前</param>
	/// <param name="baseClass">継承するベースクラスの名前</param>
	/// <returns>生成されたスクリプトの内容</returns>
	string GenerateScriptContent(string className, string baseClass)
	{
		// ベースクラスが空の場合は MonoBehaviour をデフォルトに設定
		string indent = "    ";

		// クラス名が空の場合はデフォルトの名前を使用
		// "No Base Class"の場合は継承句を省略
		string body;
		if (baseClass == "No Base Class" || string.IsNullOrWhiteSpace(baseClass))
		{
			body =
$@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class {className}
{{

}}";
		}
		else
		{
			body =
$@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class {className} : {baseClass}
{{
{indent}void Start()
{indent}{{
{indent}{indent}// 初期化処理
{indent}}}

{indent}void Update()
{indent}{{
{indent}{indent}// 毎フレーム処理
{indent}}}
}}";
		}

		return body;
	}


	/// <summary>
	/// 選択中のアセットから保存先フォルダパスを取得する。
	/// フォルダが選択されていればそのパス、ファイルならその親フォルダを返す。
	/// 何も選択されていない場合は "Assets" を返す。
	/// </summary>
	string GetSelectedPathOrFallback()
	{
		// デフォルトのパスは "Assets"
		string path = "Assets";

		// 選択中のアセットを取得
		// Selection.GetFilteredを使用して、選択されているアセットのパスを取得
		foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
		{
			// 選択されたオブジェクトのパスを取得
			path = AssetDatabase.GetAssetPath(obj);

			// フォルダが選択されていればそのパスを返す
			if (!string.IsNullOrEmpty(path) && System.IO.Directory.Exists(path))
				return path;
			// ファイルが選択されていればその親フォルダを返す
			else
				return System.IO.Path.GetDirectoryName(path);
		}

		// 何も選択されていない場合は "Assets" を返す
		return path;
	}
}

#endif // UNITY_EDITOR