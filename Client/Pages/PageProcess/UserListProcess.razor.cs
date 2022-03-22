using Blazored.Toast.Services;
using MealOrdering.Client.Utilities;
using MealOrdering.Core.Entities.Dto;
using MealOrdering.Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Components;

namespace MealOrdering.Client.Pages.User
{
    public class UserListProcess : ComponentBase
    {
        [Inject]
        public HttpClient Client { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public ModalManager modalManager { get; set; }

        [Inject]
        public IToastService toastService { get; set; }

        protected List<UserDto> Users = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadList();
        }

        protected async Task LoadList()
        {
            var apiResult = await Client.GetServiceResponseAsync<List<UserDto>>("api/v1/user");

            if (apiResult.Success)
                Users = apiResult.Data;
        }

        protected void GoCreateUserPage()
        {
            navigationManager.NavigateTo("/user/add");
        }

        protected void GoUpdateUserPage(Guid id)
        {
            navigationManager.NavigateTo($"/user/edit/{id}");
        }

        protected async Task DeleteUser(Guid id)
        {
            bool isConfirmed = await modalManager.ConfirmationAsync("Delete User Confirmation", "User will be deleted. Are you sure?");

            if (!isConfirmed) return;

            try
            {
                ApiResult<bool> isDeleted = await Client.DeleteGetServiceResponseAsync<bool>($"api/v1/user/{id}");

                if (isDeleted.Success && isDeleted.Data)
                {
                    toastService.ShowSuccess("User deletion successful", "Success");
                    await LoadList();
                }
                else
                    toastService.ShowError("User deletion failed.", "Error");
            }
            catch (Exception)
            {
                toastService.ShowError("User deletion failed.", "Error");
            }
        }
    }
}