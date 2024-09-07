using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeatherData : MonoBehaviour {
	private float timer;
	public float minutesBetweenUpdate;
	public WeatherInfo Info;
	public string API_key;
	private float latitude;
	private float longitude;
	public string city;
	private bool locationInitialized;
	public Text currentWeatherText;
	public GetLocation getLocation;

	public void Begin() {
		latitude = getLocation.latitude;
		longitude = getLocation.longitude;
		if (string.IsNullOrEmpty(city))
		{
            city = getLocation.city;
        }
        locationInitialized = true;
	}
	void Update() {
		if (locationInitialized) {
			if (timer <= 0) {
				StartCoroutine (GetWeatherInfo ());
				timer = minutesBetweenUpdate * 60;
			} else {
				timer -= Time.deltaTime;
			}
		}
	}
	private IEnumerator GetWeatherInfo()
	{		
		var www = new UnityWebRequest("https://api.weatherapi.com/v1/current.json?q="+ city + "&lang=en&key=" + API_key)
        {
			downloadHandler = new DownloadHandlerBuffer()
		};

		yield return www.SendWebRequest();

		if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
		{	
			//error
			yield break;
		}

		Info = JsonUtility.FromJson<WeatherInfo>(www.downloadHandler.text);
		currentWeatherText.text = "Current weather: " + Info.current.condition.text;
	}
}
[Serializable]
public class WeatherInfo
{
	public Current current;
	public float cloud;
}

[Serializable]
public class Current
{
	public int last_updated_epoch;
	public string last_updated;
	public float temp_c;
	public float temp_f;
	public int is_day;
	public Condition condition;
}

[Serializable]
public class Condition
{
    public string text;
    public string icon;
    public int code;
}
