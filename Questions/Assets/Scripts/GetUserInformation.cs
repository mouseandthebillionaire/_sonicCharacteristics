// adapted from Iain McManus YouTube tutorial
// https://github.com/GameDevEducation/UnityTutorial_QueryLocalWeather

using System.Collections;
using System.Collections.Generic;
using CandyCoded.env;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEditor;


public class GetUserInformation : MonoBehaviour
{
	// Boolean to turn on testing which gives us a default long/lat, so as not to spam the geoplugin API
	public  bool   getIP, getLocation, getWeather;
	private string longitude;
	private string latitude;
	private float  temperature;
	
	public enum EPhase
	{
		NotStarted,
		GetPublicIP,
		GetGeographicData,
		GetWeatherData,
		Failed,
		Succeeded
	}
	
	class geoPluginResponse
    {
        [JsonProperty("geoplugin_request")] public string Request { get; set; }
        [JsonProperty("geoplugin_status")] public int Status { get; set; }
        [JsonProperty("geoplugin_delay")] public string Delay { get; set; }
        [JsonProperty("geoplugin_credit")] public string Credit { get; set; }
        [JsonProperty("geoplugin_city")] public string City { get; set; }
        [JsonProperty("geoplugin_region")] public string Region { get; set; }
        [JsonProperty("geoplugin_regionCode")] public string RegionCode { get; set; }
        [JsonProperty("geoplugin_regionName")] public string RegionName { get; set; }
        [JsonProperty("geoplugin_areaCode")] public string AreaCode { get; set; }
        [JsonProperty("geoplugin_dmaCode")] public string DMACode { get; set; }
        [JsonProperty("geoplugin_countryCode")] public string CountryCode { get; set; }
        [JsonProperty("geoplugin_countryName")] public string CountryName { get; set; }
        [JsonProperty("geoplugin_inEU")] public int InEU { get; set; }
        [JsonProperty("geoplugin_euVATrate")] public bool EUVATRate { get; set; }
        [JsonProperty("geoplugin_continentCode")] public string ContinentCode { get; set; }
        [JsonProperty("geoplugin_continentName")] public string ContinentName { get; set; }
        [JsonProperty("geoplugin_latitude")] public string Latitude { get; set; }
        [JsonProperty("geoplugin_longitude")] public string Longitude { get; set; }
        [JsonProperty("geoplugin_locationAccuracyRadius")] public string LocationAccuracyRadius { get; set; }
        [JsonProperty("geoplugin_timezone")] public string TimeZone { get; set; }
        [JsonProperty("geoplugin_currencyCode")] public string CurrencyCode { get; set; }
        [JsonProperty("geoplugin_currencySymbol")] public string CurrencySymbol { get; set; }
        [JsonProperty("geoplugin_currencySymbol_UTF8")] public string CurrencySymbolUTF8 { get; set; }
        [JsonProperty("geoplugin_currencyConverter")] public double CurrencyConverter { get; set; }
    }
	
	public class OpenWeather_Coordinates
    {
        [JsonProperty("lon")] public double Longitude { get; set; }
        [JsonProperty("lat")] public double Latitude { get; set; }
    }

    // Condition Info: https://openweathermap.org/weather-conditions
    public class OpenWeather_Condition
    {
        [JsonProperty("id")] public int ConditionID { get; set; }
        [JsonProperty("main")] public string Group { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("icon")] public string Icon { get; set; }
    }

    public class OpenWeather_KeyInfo
    {
        [JsonProperty("temp")] public double Temperature { get; set; }
        [JsonProperty("feels_like")] public double Temperature_FeelsLike { get; set; }
        [JsonProperty("temp_min")] public double Temperature_Minimum { get; set; }
        [JsonProperty("temp_max")] public double Temperature_Maximum { get; set; }
        [JsonProperty("pressure")] public int Pressure { get; set; }
        [JsonProperty("sea_level")] public int PressureAtSeaLevel { get; set; }
        [JsonProperty("grnd_level")] public int PressureAtGroundLevel { get; set; }
        [JsonProperty("humidity")] public int Humidity { get; set; }
    }

    public class OpenWeather_Wind
    {
        [JsonProperty("speed")] public double Speed { get; set; }
        [JsonProperty("deg")] public int Direction { get; set; }
        [JsonProperty("gust")] public double Gust { get; set; }
    }

    public class OpenWeather_Clouds
    {
        [JsonProperty("all")] public int Cloudiness { get; set; }
    }

    public class OpenWeather_Rain
    {
        [JsonProperty("1h")] public float VolumeInLastHour { get; set; }
        [JsonProperty("3h")] public float VolumeInLast3Hours { get; set; }
    }

    public class OpenWeather_Snow
    {
        [JsonProperty("1h")] public int VolumeInLastHour { get; set; }
        [JsonProperty("3h")] public int VolumeInLast3Hours { get; set; }
    }

    public class OpenWeather_Internal
    {
        [JsonProperty("type")] public int Internal_Type { get; set; }
        [JsonProperty("id")] public int Internal_ID { get; set; }
        [JsonProperty("message")] public double Internal_Message { get; set; }
        [JsonProperty("country")] public string CountryCode { get; set; }
        [JsonProperty("sunrise")] public int SunriseTime { get; set; }
        [JsonProperty("sunset")] public int SunsetTime { get; set; }
    }

    class OpenWeatherResponse
    {
        [JsonProperty("coord")] public OpenWeather_Coordinates Location { get; set; }
        [JsonProperty("weather")] public List<OpenWeather_Condition> WeatherConditions { get; set; }
        [JsonProperty("base")] public string Internal_Base { get; set; }
        [JsonProperty("main")] public OpenWeather_KeyInfo KeyInfo { get; set; }
        [JsonProperty("visibility")] public int Visibility { get; set; }
        [JsonProperty("wind")] public OpenWeather_Wind Wind { get; set; }
        [JsonProperty("clouds")] public OpenWeather_Clouds Clouds { get; set; }
        [JsonProperty("rain")] public OpenWeather_Rain Rain { get; set; }
        [JsonProperty("snow")] public OpenWeather_Snow Snow { get; set; }
        [JsonProperty("dt")] public int TimeOfCalculation { get; set; }
        [JsonProperty("sys")] public OpenWeather_Internal Internal_Sys { get; set; }
        [JsonProperty("timezone")] public int Timezone { get; set; }
        [JsonProperty("id")] public int CityID { get; set; }
        [JsonProperty("name")] public string CityName { get; set; }
        [JsonProperty("cod")] public int Internal_COD { get; set; }
    }

	public  EPhase              phase;
	private geoPluginResponse   location;
	private OpenWeatherResponse weather;
	
	private       string publicIP;
	private const string URL_GetPublicIP    = "https://api.ipify.org";
	private const string URL_GetLocation    = "http://www.geoplugin.net/json.gp?ip=";
	private const string URL_GetWeatherData = "http://api.openweathermap.org/data/2.5/weather";

	public void Start()
	{
		StartCoroutine(GetIP());
	}
	
	private IEnumerator GetIP()
	{
		phase = EPhase.GetPublicIP;

		if (!getIP)
		{
			if (env.TryParseEnvironmentVariable("IP", out string _publicIP))
			{
				publicIP = _publicIP;
				StartCoroutine(GetLocation());
			}
		}
		else
		{
			// attempt to get the public IP address
			using (UnityWebRequest request = UnityWebRequest.Get(URL_GetPublicIP))
			{
				request.timeout = 1;
				yield return request.SendWebRequest();

				// success?
				if (request.result == UnityWebRequest.Result.Success)
				{
					publicIP = request.downloadHandler.text.Trim();
					StartCoroutine(GetLocation());
				}
				else
				{
					Debug.Log($"Failed to get public IP: {request.downloadHandler.text}");
					GlobalVariables.S.error = true;
				}
			}
		}
	}

	private IEnumerator GetLocation()
	{
		phase = EPhase.GetGeographicData;

		if (!getLocation)
		{
			latitude = "45.5878";
			longitude = "-73.6073";
			GlobalVariables.S.city = "Montreal";
			GlobalVariables.S.openAIMessages.Add("Did you know that Montreal is the second largest French-speaking city in the world? Isn't that wild?");
			
			StartCoroutine(GetWeather());
		}
		else
		{
			// attempt to get Location
			using (UnityWebRequest request = UnityWebRequest.Get(URL_GetLocation + publicIP))
			{
				request.timeout = 1;
				yield return request.SendWebRequest();
			
				// success?
				if (request.result == UnityWebRequest.Result.Success)
				{
					location = JsonConvert.DeserializeObject<geoPluginResponse>(request.downloadHandler.text);
					latitude = location.Latitude;
					longitude = location.Longitude;
					GlobalVariables.S.city = location.City;
					
					// Once we've done all this we'll ask OpenAI to comment on the city
					string factRequest =
						$"Tell me one new, unique, random and outrageous fact about the city of {GlobalVariables.S.city} that I probably don't know and you have never told me before. Start by saying something like 'Hey, speaking of {GlobalVariables.S.city}, did you know...' and end with an exclamatory statement. Make sure it is between 25 and 38 words and a complete thought.";
					OpenAIRequests.S.GetOpenAIResponse(factRequest);
					
					StartCoroutine(GetWeather());
				}
				else
				{
					Debug.Log($"Failed to get location: {request.downloadHandler.text}");
					GlobalVariables.S.error = true;
				}
			}
		}

		yield return null;
	}

	private IEnumerator GetWeather()
	{
		phase = EPhase.GetWeatherData;

		if (!getWeather)
		{
			GlobalVariables.S.currentTemperature = 284.15f;
			GlobalVariables.S.currentConditions = "moderate rain";
			GlobalVariables.S.openAIMessages.Add("Can you believe this weather out there today?");
			phase = EPhase.Succeeded;
			GlobalVariables.S.loading = false;
		}
		else
		{
			string weatherURL = URL_GetWeatherData;
			weatherURL += $"?lat={latitude}";
			weatherURL += $"&lon={longitude}";
			if (env.TryParseEnvironmentVariable("OW_API_KEY", out string owKey))
			{
				weatherURL += $"&APPID={owKey}";
			}
			
			// attempt to get Location
			using (UnityWebRequest request = UnityWebRequest.Get(weatherURL))
			{
				request.timeout = 1;
				yield return request.SendWebRequest();

				// success?
				if (request.result == UnityWebRequest.Result.Success)
				{
					weather = JsonConvert.DeserializeObject<OpenWeatherResponse>(request.downloadHandler.text);

					GlobalVariables.S.currentTemperature = (float) weather.KeyInfo.Temperature_FeelsLike;
					GlobalVariables.S.currentConditions = weather.WeatherConditions[0].Description;
					Debug.Log(GlobalVariables.S.currentConditions);
					
					// Once we've done all this we'll ask OpenAI to comment on the weather
					string factRequest =
						$"Make hokey smalltalk about the fact that there is {GlobalVariables.S.currentConditions} in {GlobalVariables.S.city} without restating the question. Make sure it is between 20 and 40 words and a complete thought.";
					OpenAIRequests.S.GetOpenAIResponse(factRequest);
					
					phase = EPhase.Succeeded;
					GlobalVariables.S.loading = false;
					GlobalVariables.S.ready = true;
				}
				else
				{
					Debug.Log($"Failed to get weather: {request.downloadHandler.text}");
					GlobalVariables.S.error = true;
				}
			}
		}
		
		yield return null;
	}
}