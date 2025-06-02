using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject allCanvas;
	[SerializeField] private GameObject background;
	
	[SerializeField] private GameObject menuObjects;
	[SerializeField] private Button resumeButton;
	[SerializeField] private Button toMainMenuButton;
	
	[SerializeField] private GameObject warningPanel;
	[SerializeField] private Button leaveButton;
	[SerializeField] private Button stayButton;
	
	[SerializeField] private VideoPlayer[] videoPlayers;
	
	[SerializeField] private string sceneToLoad;
	
	private bool isPaused = false;

	private void Awake()
	{
		background.SetActive(false);
		menuObjects.SetActive(false);
		warningPanel.SetActive(false);
		
		allCanvas.SetActive(true);
	}
	
	private void Start()
	{
		resumeButton.onClick.AddListener(ResumeGame);
		toMainMenuButton.onClick.AddListener(LoadMainMenu);
		
		leaveButton.onClick.AddListener(LeftAnyway);
		stayButton.onClick.AddListener(Stay);

		// background.SetActive(false);
		// menuObjects.SetActive(false);
		// warningPanel.SetActive(false);
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (warningPanel.activeSelf)
				return;
			
			if (isPaused)
				ResumeGame();
			else
				PauseGame();
		}
	}

	private void ResumeGame()
	{
		background.SetActive(false);
		menuObjects.SetActive(false);
		
		Time.timeScale = 1f;
		isPaused = false;
		
		foreach (var vp in videoPlayers)
		{
			if (vp.isPaused)
				vp.Play();
		}
	}

	private void PauseGame()
	{
		background.SetActive(true);
		menuObjects.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
		
		foreach (var vp in videoPlayers)
		{
			if (vp.isPlaying)
				vp.Pause();
		}
	}
	
	private void LoadMainMenu()
	{
		warningPanel.SetActive(true);
	}

	private void LeftAnyway()
	{
		background.SetActive(false);
		menuObjects.SetActive(false);
		warningPanel.SetActive(false);
		
		allCanvas.SetActive(false);
		
		Time.timeScale = 1f;
		
		SceneManager.LoadScene(sceneToLoad);
	}

	private void Stay()
	{
		warningPanel.SetActive(false);
	}
}