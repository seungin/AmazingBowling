using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public GameObject readyPanel;
	public Text messageText;
	public Text scoreText;
	public Text bestScoreText;

	public ShooterRotator shooterRotator;
	public CamFollow cam;

	public bool isRoundActive = false;
	private int score = 0;

	private void Awake()
	{
		instance = this;
		ResetRound();
	}

	private void Start()
	{
		StartCoroutine("RoundRoutine");
	}

	IEnumerator RoundRoutine()
	{
		ResetRound();

		// Ready
		readyPanel.SetActive(true);
		messageText.text = "Ready...";
		shooterRotator.enabled = false;
		cam.SetTarget(shooterRotator.transform, CamFollow.State.Idle);
		isRoundActive = false;
		yield return new WaitForSeconds(2);

		// Play
		readyPanel.SetActive(false);
		shooterRotator.enabled = true;
		cam.SetTarget(shooterRotator.transform, CamFollow.State.Ready);
		isRoundActive = true;

		while (isRoundActive)
		{
			yield return null;
		}

		// End
		readyPanel.SetActive(true);
		messageText.text = "Wait for next round...";
		shooterRotator.enabled = false;
		cam.SetTarget(shooterRotator.transform, CamFollow.State.Idle);
		isRoundActive = false;
		yield return new WaitForSeconds(2);

		StartCoroutine("RoundRoutine");
	}

	public void ResetRound()
	{
		score = 0;
		UpdateUI();
	}

	public void OnBallDestroy()
	{
		isRoundActive = false;
	}

	public void OnPropDestroy(int score)
	{
		AddScore(score);
	}

	public void AddScore(int value)
	{
		score += value;
		UpdateBestScore();
		UpdateUI();
	}

	private void UpdateBestScore()
	{
		if (score > GetBestScore())
		{
			PlayerPrefs.SetInt("BestScore", score);
		}
	}

	private int GetBestScore()
	{
		return PlayerPrefs.GetInt("BestScore");
	}

	private void UpdateUI()
	{
		scoreText.text = "Score: " + score;
		bestScoreText.text = "BestScore: " + GetBestScore();
	}
}
