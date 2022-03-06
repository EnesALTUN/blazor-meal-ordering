using MealOrdering.Core.Entities.Dto;
using MealOrdering.Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace MealOrdering.Client.Pages.User
{
    public class UserListProcess : ComponentBase
    {
        [Inject]
        public HttpClient Client { get; set; }

        protected List<UserDto> Users = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadList();
        }

        protected async Task LoadList()
        {
            var serviceResponse = await Client.GetFromJsonAsync<ApiResult<List<UserDto>>>("api/user");

            if (serviceResponse.Success)
                Users = serviceResponse.Data;
        }
    }
}
