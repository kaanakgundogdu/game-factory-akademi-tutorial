using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class LevelManager : MonoBehaviour
{
	public void RestartScene()
	{

		StartCoroutine(RestartSceneCoroutine());
	}

	private IEnumerator RestartSceneCoroutine()
	{
		yield return new WaitForSeconds(1f);

		LoadNewScene(GetActiveSceneIndex());
	}
	public void NextLevel()
	{
		LoadNewScene(GetActiveSceneIndex() + 1);

	}
	private void LoadNewScene( int buildIndex)
	{
		SceneManager.LoadScene(buildIndex);
	}

	private int GetActiveSceneIndex()
	{
		return SceneManager.GetActiveScene().buildIndex;
	}



	public void GameOverScene()
	{
		SceneManager.LoadScene("GameOverScene");
	}
	public void StartMenuScene()
	{
		SceneManager.LoadScene(0);
	}
	public void QuitGame()
	{
		Application.Quit();
	}

}
