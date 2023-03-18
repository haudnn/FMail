namespace Workdo.Helpers;
public class ToggleHelper
{
    private bool _isToggled = false;

    public bool IsToggled => _isToggled;

    public void Toggle()
    {
        _isToggled = !_isToggled;
    }
}
