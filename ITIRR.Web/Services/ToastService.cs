using Microsoft.JSInterop;

namespace ITIRR.Web.Services
{
    public class ToastService
    {
        private readonly IJSRuntime _js;
        public ToastService(IJSRuntime js) => _js = js;

        public async Task ShowSuccess(string message) => await Show(message, "success");
        public async Task ShowError(string message) => await Show(message, "error");
        public async Task ShowWarning(string message) => await Show(message, "warning");
        public async Task ShowInfo(string message) => await Show(message, "info");

        private async Task Show(string message, string type)
        {
            try { await _js.InvokeVoidAsync("showToast", message, type); }
            catch { }
        }
    }
}