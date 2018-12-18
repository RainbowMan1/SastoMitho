using System.Web.Mvc;

namespace SastoMithoMVC.Areas.Deliverer
{
    public class DelivererAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Deliverer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Deliverer_default",
                "Deliverer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}