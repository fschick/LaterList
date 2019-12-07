namespace FS.LaterList.IoC.Interfaces.Application.Services
{
    public interface IInformationService
    {
        string GetProductName();
        string GetProductVersion();
        string GetCopyright();
        string GetImprint();
        string GetPrivacyPolicy();
    }
}