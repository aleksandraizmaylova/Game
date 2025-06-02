using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private Button newGameButton;
	[SerializeField] private Button quitButton;

	[SerializeField] private string gameSceneName;

	void Start()
	{
		newGameButton.onClick.AddListener(OnNewGameClicked);
		quitButton.onClick.AddListener(OnQuitClicked);
	}

	void OnNewGameClicked()
	{
		SceneManager.LoadScene(gameSceneName);
	}

	void OnQuitClicked()
	{
		Application.Quit();
		
		Debug.Log("Выход...");
		
// #if UNITY_EDITOR
// 		UnityEditor.EditorApplication.isPlaying = false;
// #endif
	}
}