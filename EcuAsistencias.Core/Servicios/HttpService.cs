using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EcuAsistencias.Core.Servicios
{
	public class HttpService
	{
		private static readonly string BaseURL = "https://localhost:44372/api/";
		public static async Task<List<T>> GetApiLista<T>(string controller)
		{
			List<T> retorno = new List<T>();

			using (HttpClient cliente = new HttpClient())
			{
				Uri uri = new Uri($"{BaseURL}{controller}");

				HttpRequestMessage request = new HttpRequestMessage();
				request.Method = HttpMethod.Get;
				request.Headers.Add("Accept", "application/json");
				request.RequestUri = uri;

				try
				{
					HttpResponseMessage respuesta = await cliente.SendAsync(request);
					respuesta.EnsureSuccessStatusCode();
					string json = await respuesta.Content.ReadAsStringAsync();
					retorno = JsonConvert.DeserializeObject<List<T>>(json);
				}
				catch( Exception e)
				{
					throw e;
				}

			}
			return retorno;
		}

		public static async Task<T1> GetApiById<T1, T2>(string controller, T2 id)
		{
			T1 retorno;

			using (HttpClient cliente = new HttpClient())
			{
				Uri uri = new Uri($"{BaseURL}{controller}/{id}");

				HttpRequestMessage request = new HttpRequestMessage();
				request.Method = HttpMethod.Get;
				request.Headers.Add("Accept", "application/json");
				request.RequestUri = uri;

				try
				{
					HttpResponseMessage respuesta = await cliente.SendAsync(request);
					respuesta.EnsureSuccessStatusCode();
					string json = await respuesta.Content.ReadAsStringAsync();
					retorno = JsonConvert.DeserializeObject<T1>(json);
				}
				catch (Exception e)
				{
					throw e;
				}

			}
			return retorno;
		}

		public static async Task Post<T>(string controller, T objeto)
		{
			using (HttpClient cliente = new HttpClient())
			{
				Uri uri = new Uri($"{BaseURL}{controller}");

				HttpRequestMessage request = new HttpRequestMessage();
				request.Method = HttpMethod.Post;
				request.Headers.Add("Accept", "application/json");
				request.RequestUri = uri;

				string contenido = JsonConvert.SerializeObject(objeto);
				request.Content = new StringContent(contenido, Encoding.UTF8, "application/json");

				try
				{
					HttpResponseMessage respuesta = await cliente.SendAsync(request);
					respuesta.EnsureSuccessStatusCode();
				}
				catch (Exception e)
				{
					throw e;
				}

			}
		}

		public static async Task Put<T, T2>(string controller, T objeto, T2 id)
		{
			using (HttpClient cliente = new HttpClient())
			{
				Uri uri = new Uri($"{BaseURL}{controller}/{id}");

				HttpRequestMessage request = new HttpRequestMessage();
				request.Method = HttpMethod.Put;
				request.Headers.Add("Accept", "application/json");
				request.RequestUri = uri;

				string contenido = JsonConvert.SerializeObject(objeto);
				request.Content = new StringContent(contenido, Encoding.UTF8, "application/json");

				try
				{
					HttpResponseMessage respuesta = await cliente.SendAsync(request);
					respuesta.EnsureSuccessStatusCode();
				}
				catch (Exception e)
				{
					throw e;
				}

			}
		}

		public static async Task DeleteById<T>(string controller, T id)
		{
			using (HttpClient cliente = new HttpClient())
			{
				Uri uri = new Uri($"{BaseURL}{controller}/{id}");

				HttpRequestMessage request = new HttpRequestMessage();
				request.Method = HttpMethod.Delete;
				request.Headers.Add("Accept", "application/json");
				request.RequestUri = uri;

				try
				{
					HttpResponseMessage respuesta = await cliente.SendAsync(request);
					respuesta.EnsureSuccessStatusCode();
				}
				catch (Exception e)
				{
					throw e;
				}

			}
		}
	}
}
