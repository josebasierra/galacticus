using UnityEngine;
using UnityEngine.UI;

public class EnergyDisplay : MonoBehaviour
{
	[SerializeField] Image barImage;
	[SerializeField] Color color;
	[SerializeField] Color overheatedColor;

	Slider slider;
	Energy energy;

	void Start()
	{
		slider = GetComponent<Slider>();

		var playerObject = GameObject.FindGameObjectWithTag("Player");
		if (playerObject != null) energy = playerObject.GetComponent<Energy>();
		slider.maxValue = 1f;
	}

	void Update()
	{
		if (energy == null) return;

		slider.value = energy.GetCurrentValue() / energy.GetMaxValue();
		barImage.color = energy.IsOverheated() ? overheatedColor : color;
	}
}
