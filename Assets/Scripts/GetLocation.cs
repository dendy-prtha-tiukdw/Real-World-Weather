﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GetLocation : MonoBehaviour
{
	public LocationInfo Info;
	public float latitude;
	public float longitude;
	public string city;
    public WeatherData weatherData;
	private string IPAddress;
	void Start() {
		StartCoroutine (GetIP());
	}
	private IEnumerator GetIP()
	{
		var www = new UnityWebRequest("http://botwhatismyipaddress.com/")
		{
			downloadHandler = new DownloadHandlerBuffer()
		};

		yield return www.SendWebRequest();
		
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
		{
			//error
			yield break;
		}

		IPAddress = www.downloadHandler.text;
		StartCoroutine (GetCoordinates());
	}

	private IEnumerator GetCoordinates()
	{
		var www = new UnityWebRequest("http://ip-api.com/json/" + IPAddress)
		{
			downloadHandler = new DownloadHandlerBuffer()
		};

		yield return www.SendWebRequest();

		if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
			//error
			yield break;
		}

		Info = JsonUtility.FromJson<LocationInfo>(www.downloadHandler.text);
		latitude = Info.lat;
		longitude = Info.lon;
		city = Info.city;
        weatherData.Begin ();
	}
}

[Serializable]
public class LocationInfo
{
	public string status;
	public string country;
	public string countryCode;
	public string region;
	public string regionName;
	public string city;
	public string zip;
	public float lat;
	public float lon;
	public string timezone;
	public string isp;
	public string org;
	public string @as;
	public string query;
}
