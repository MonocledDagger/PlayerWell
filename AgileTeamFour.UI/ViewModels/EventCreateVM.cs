using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgileTeamFour.UI.ViewModels
{
    public class EventCreateVM
    {
        public Events Event { get; set; }
        public IEnumerable<SelectListItem> Games { get; set; }
    }
}
