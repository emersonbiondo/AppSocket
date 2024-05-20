namespace Util
{
    public interface IAlertService
    {
        void ShowAlert(string title, string message, string cancel = "OK");

        void ShowConfirmation(string title, string message, Action<bool> callback, string accept = "Yes", string cancel = "No");
    }
}
