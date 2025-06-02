using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenuUI;

	[SerializeField] private Button resumeButton;
	[SerializeField] private Button toMainMenuButton;
	
	[SerializeField] private VideoPlayer[] videoPlayers;
	
	[SerializeField] private string sceneToLoad;
	
	private bool isPaused = false;

	private void Start()
	{
		resumeButton.onClick.AddListener(ResumeGame);
		toMainMenuButton.onClick.AddListener(LoadMainMenu);
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isPaused)
				ResumeGame();
			else
				PauseGame();
		}
	}

	public void ResumeGame()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
		
		foreach (var vp in videoPlayers)
		{
			if (vp.isPaused)
				vp.Play();
		}
	}

	void PauseGame()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
		
		foreach (var vp in videoPlayers)
		{
			if (vp.isPlaying)
				vp.Pause();
		}
	}

	// public void LoadMainMenu()
	// {
	// 	Time.timeScale = 1f;
	// 	SceneManager.LoadScene(sceneToLoad);
	// }
	public void LoadMainMenu()
	{
		Time.timeScale = 1f;

		if (Application.CanStreamedLevelBeLoaded(sceneToLoad))
		{
			Debug.Log($"Загрузка сцены: {sceneToLoad}");
			SceneManager.LoadScene(sceneToLoad);
		}
		else
		{
			Debug.LogError($"Сцена '{sceneToLoad}' не может быть загружена. Убедись, что она добавлена в Build Settings.");
		}
	}

}