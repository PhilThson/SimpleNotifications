﻿using System.Net.Http.Headers;
using System.Net.Http.Json;
using Manager.Interfaces;

namespace Manager.Services;

public class HttpClientService : IHttpClientService
{
	private Lazy<HttpClient> _httpClient =>
		new(() =>
			{
				var client = new HttpClient()
				{
					BaseAddress = new Uri("http://localhost:5101")
				};
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
				return client;
			}
		);

	public HttpClientService()
	{

	}

	public async Task<IEnumerable<T>> GetAllNotificationsAsync<T>()
	{
		var endpoint = "/notifications?user_id=Manager";
		var response = await _httpClient.Value.GetAsync(endpoint);
		if (!response.IsSuccessStatusCode)
		{
			throw new ApplicationException("An error occured at notifications fetching.");
		}
		return await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
    }

	public async Task<IEnumerable<T>> GetConnectedUsersAsync<T>()
	{
		var endpoint = "/users?user_id=Manager";
		var response = await _httpClient.Value.GetAsync(endpoint);
		if (!response.IsSuccessStatusCode)
		{
			throw new ApplicationException("An error occured at users fetching");
		}
		return await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
	}
}

