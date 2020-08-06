
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
	[SerializeField] Image barImage;
	[SerializeField] Gradient gradient;

	Slider slider;
	Health health;


	void Start()
	{
		slider = GetComponent<Slider>();

		var playerObject = GameObject.FindGameObjectWithTag("Player");
		if (playerObject != null) health = playerObject.GetComponent<Health>();
		slider.maxValue = 1f;
	}


	void Update()
	{
		if (health == null) return;

		slider.value = health.GetCurrentValue() / health.GetMaxValue();
		barImage.color = gradient.Evaluate(slider.normalizedValue);
	}
}
