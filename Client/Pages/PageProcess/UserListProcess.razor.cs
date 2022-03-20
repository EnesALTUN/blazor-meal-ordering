using MealOrdering.Client.Utilities;
using MealOrdering.Core.Entities.Dto;
using Microsoft.AspNetCore.Components;

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
            var apiResult = await Client.GetServiceResponseAsync<List<UserDto>>("api/user");

            if (apiResult.Success)
                Users = apiResult.Data;
        }
    }
}
