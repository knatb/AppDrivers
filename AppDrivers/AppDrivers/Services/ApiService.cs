using AppDrivers.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppDrivers.Services
{
    public class ApiService
    {
        private string ApiUrl = "https://azuresqlapiklopez.azurewebsites.net/";
        //public ApiService() { }
        public async Task<ApiResponse> GetDataAsync<T>(string controller)
        {
            try
            {
                var client = new HttpClient //variable de tipo cliente, se instancia con la apiurl
                {
                    BaseAddress = new System.Uri(ApiUrl)
                };
                var response = await client.GetAsync(controller);   //que consuma el cliente con el metodo get, metodo asincrono
                var result = await response.Content.ReadAsStringAsync();    //leer contenido como cadena

                if (!response.IsSuccessStatusCode)  //respuesta no existosa
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,  //no fue exitoso
                        Message = result    //mensaje
                    };
                }
                //si fue exitosa, deserializa en observale collection
                var data = JsonConvert.DeserializeObject<ObservableCollection<T>>(result);
                return new ApiResponse
                {
                    IsSuccess = true,   //exitosa
                    Message = "EXCELSIOR",
                    Result = data
                };
            }
            catch (System.Exception ex) //si se genera un error, etc
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        public async Task<ApiResponse> PostDataAsync(string controller, object data)
        {
            try
            {
                var serializedData = JsonConvert.SerializeObject(data);
                var content = new StringContent(serializedData, Encoding.UTF8, "application/json");

                var client = new HttpClient
                {
                    BaseAddress = new System.Uri(ApiUrl)
                };

                var response = await client.PostAsync(controller, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result.ToString(),
                        Result = null
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponse>(result);
            }
            catch (System.Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        public async Task<ApiResponse> PutDataAsync(string controller, int id, object data)
        {
            try
            {
                var serializedData = JsonConvert.SerializeObject(data);
                var content = new StringContent(serializedData, Encoding.UTF8, "application/json");

                var client = new HttpClient
                {
                    BaseAddress = new System.Uri(ApiUrl)
                };

                var response = await client.PutAsync(controller + "/" + id, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result.ToString(),
                        Result = null
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponse>(result);
            }
            catch (System.Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        public async Task<ApiResponse> DeleteDataAsync(string controller, int id)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new System.Uri(ApiUrl)
                };

                var response = await client.DeleteAsync(controller + "/" + id);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result.ToString(),
                        Result = null
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponse>(result);
            }
            catch (System.Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }
    }
}
