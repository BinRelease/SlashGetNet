using System.Web.Mvc;

namespace WebApplication1.Areas.AngularAuto
{
    public class AngularAutoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AngularAuto";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AngularAuto_default",
                "AngularAuto/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}