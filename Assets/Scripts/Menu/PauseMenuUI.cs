using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject allCanvas;
	
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
		
		menuObjects.SetActive(false);
		warningPanel.SetActive(false);
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
		menuObjects.SetActive(false);
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
		menuObjects.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
		
		foreach (var vp in videoPlayers)
		{
			if (vp.isPlaying)
				vp.Pause();
		}
	}
	
	public void LoadMainMenu()
	{
		warningPanel.SetActive(true);
	}

	private void LeftAnyway()
	{
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