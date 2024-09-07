using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {
	public WeatherData weatherData;
	public int currentWeatherCode;
	private bool rain;
	private bool snow;
	private bool cloudy;
	private bool sunny;
	public GameObject rainObject;
	public GameObject snowObject;
	public GameObject cloudyObject;
	public GameObject sunnyObject;
	void Update () {
        currentWeatherCode = weatherData.Info.current.condition.code;

        /*if (currentWeather == "rain") {
			SpawnRain ();
		} else if (currentWeather == "snow" || currentWeather == "sleet") {
			SpawnSnow ();
		} else if (currentWeather == "cloudy" || currentWeather == "Patchy rain nearby" || currentWeather == "partly-cloudy-night" || currentWeather == "fog") {
			SpawnCloudy ();
		} else if (currentWeather == "clear-day" || currentWeather == "clear-night" || currentWeather == "wind") {
			SpawnSunny ();
		} else {
			None ();
		}*/
        if (currentWeatherCode == 1063)
        {
            SpawnRain();
        }
    }

	void SpawnRain () {
		rain = true;
		rainObject.SetActive (true);
		if (snow) {
			StartCoroutine (DisableSnow());
		} else if (cloudy) {
			StartCoroutine (DisableCloudy());
		} else if (sunny) {
			StartCoroutine (DisableSunny());
		}
	}
	void SpawnSnow () {
		snow = true;
		snowObject.SetActive (true);
		if (rain) {
			StartCoroutine (DisableRain());
		} else if (cloudy) {
			StartCoroutine (DisableCloudy());
		} else if (sunny) {
			StartCoroutine (DisableSunny());
		}
	}
	void SpawnCloudy () {
		cloudy = true;
		cloudyObject.SetActive (true);
		if (snow) {
			StartCoroutine (DisableSnow());
		} else if (rain) {
			StartCoroutine (DisableRain());
		} else if (sunny) {
			StartCoroutine (DisableSunny());
		}
	}
	void SpawnSunny () {
		sunny = true;
		sunnyObject.SetActive (true);
		if (snow) {
			StartCoroutine (DisableSnow());
		} else if (cloudy) {
			StartCoroutine (DisableCloudy());
		} else if (rain) {
			StartCoroutine (DisableRain());
		}
	}
	void None () {
		if (rain) {
			StartCoroutine (DisableRain());
		} else if (cloudy) {
			StartCoroutine (DisableCloudy());
		} else if (sunny) {
			StartCoroutine (DisableSunny());
		} else if (snow) {
			StartCoroutine (DisableSnow());
		}
	}

	IEnumerator DisableRain() {
		rain = false;
		rainObject.GetComponent<ParticleSystem> ().Stop();
		rainObject.GetComponent<Animator> ().Play ("rain_exit");
		yield return new WaitForSeconds(5);
		rainObject.SetActive (false);
	}

	IEnumerator DisableSnow() {
		snow = false;
		snowObject.GetComponent<ParticleSystem> ().Stop();
		snowObject.GetComponent<Animator> ().Play ("snow_exit");
		yield return new WaitForSeconds(5);
		snowObject.SetActive (false);
	}

	IEnumerator DisableCloudy() {
		cloudy = false;
		cloudyObject.GetComponent<Animator> ().Play ("cloudy_exit");
		yield return new WaitForSeconds(5);
		cloudyObject.SetActive (false);
	}

	IEnumerator DisableSunny() {
		sunny = false;
		sunnyObject.GetComponent<Animator> ().Play ("sunny_exit");
		yield return new WaitForSeconds(5);
		sunnyObject.SetActive (false);
	}
}
