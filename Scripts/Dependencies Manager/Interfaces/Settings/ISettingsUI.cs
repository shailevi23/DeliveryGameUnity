public interface ISettingsUI
{
    void ToggleSettings();
    void ToggleChangeCar();
    IChangeCar _changeCar { get; }
}
