using System.Web.Mvc;

namespace AssignmentWebPhone.Areas.Accuracy
{
    public class AccuracyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Accuracy";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Accuracy_default",
                "Accuracy/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}