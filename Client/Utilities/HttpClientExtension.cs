using MealOrdering.Core.Utilities.Results.Concrete;
using System.Net.Http.Json;

namespace MealOrdering.Client.Utilities;

public static class HttpClientExtension
{
    public async static Task<ApiResult<TResult>> PostGetServiceResponseAsync<TResult, TValue>(this HttpClient client, string url, TValue value, bool ThrowSuccessException = false)
    {
        var httpRes = await client.PostAsJsonAsync(url, value);

        if (httpRes.IsSuccessStatusCode)
        {
            var res = await httpRes.Content.ReadFromJsonAsync<ApiResult<TResult>>();

            return !res.Success && ThrowSuccessException 
                ? throw new Exception(res.Message) 
                : res;
        }

        throw new Exception(httpRes.ReasonPhrase);
    }

    public async static Task<ApiResult<TResult>> PutGetServiceResponseAsync<TResult, TValue>(this HttpClient client, string url, TValue value, bool ThrowSuccessException = false)
    {
        var httpRes = await client.PutAsJsonAsync(url, value);

        if (httpRes.IsSuccessStatusCode)
        {
            var res = await httpRes.Content.ReadFromJsonAsync<ApiResult<TResult>>();

            return !res.Success && ThrowSuccessException 
                ? throw new Exception(res.Message) 
                : res;
        }

        throw new Exception(httpRes.ReasonPhrase);
    }

    public async static Task<ApiResult<TResult>> DeleteGetServiceResponseAsync<TResult>(this HttpClient client, string url, bool ThrowSuccessException = false)
    {
        var httpRes = await client.DeleteAsync(url);

        if (httpRes.IsSuccessStatusCode)
        {
            var res = await httpRes.Content.ReadFromJsonAsync<ApiResult<TResult>>();

            return !res.Success && ThrowSuccessException 
                ? throw new Exception(res.Message) 
                : res;
        }

        throw new Exception(httpRes.ReasonPhrase);
    }

    public async static Task<ApiResult<TResult>> GetServiceResponseAsync<TResult>(this HttpClient client, string url, bool ThrowSuccessException = false)
    {
        var httpRes = await client.GetFromJsonAsync<ApiResult<TResult>>(url);

        return !httpRes.Success && ThrowSuccessException
            ? throw new Exception(httpRes.Message)
            : httpRes;
    }
}