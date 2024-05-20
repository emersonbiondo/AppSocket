using AppClient.Presentations.Models;
using AppServer;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Util;

namespace AppClient.Presentations.ViewModels
{
    public partial class ClientViewModel : ObservableObject
    {
        public static IAlertService AlertService;

        [ObservableProperty]
        public Client client;

        public bool IsMessageValid { get; set; }

        public ClientViewModel(IAlertService alertService, ClientConfiguration clientConfiguration)
        {
            AlertService = alertService;
            Client = new Client(clientConfiguration.Server, clientConfiguration.Port);
        }

        [RelayCommand]
        public Task Send()
        {
            if (Validate())
            {
                Client.SendMessage();
                AlertService.ShowAlert("Success", "The Message is Send");
            }

            return Task.CompletedTask;
        }

        public bool Validate()
        {
            var errors = new List<string>();

            if (!IsMessageValid)
            {
                errors.Add("Message required and longer than 3 letters");
            }

            if (errors.Count > 0)
            {
                AlertService.ShowAlert("Data Validation", string.Join(Environment.NewLine, errors));
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
