using AskGenerator.Business.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Mvc.Components
{
    public interface IBaseWebViewPage
    {
        bool IsEditing { get; set; }

        Resolver R { get; set; }

        RobotsInfo Robots { get;set; }

        WebsiteSettings WebsiteSettings { get; }
    }

    public class BaseWebViewPage<TModel> : WebViewPage<TModel>, IBaseWebViewPage
    {
        public bool IsEditing { get; set; }

        public RobotsInfo Robots { get; set; }

        public Resolver R { get; set; }

        private WebsiteSettings websiteSettings;
        public WebsiteSettings WebsiteSettings
        {
            get
            {
                return websiteSettings != null? websiteSettings: websiteSettings = Site.Settings.Website();
            }
        }

        public override void Execute()
        {
        }

        public override void InitHelpers()
        {
            base.InitHelpers();
            Initializer.Init(this);
        }
    }


    public class BaseWebViewPage : WebViewPage, IBaseWebViewPage
    {
        public bool IsEditing { get; set; }

        public RobotsInfo Robots { get; set; }

        public Resolver R { get; set; }

        private WebsiteSettings websiteSettings;
        public WebsiteSettings WebsiteSettings
        {
            get
            {
                return websiteSettings != null ? websiteSettings : websiteSettings = Site.Settings.Website();
            }
        }

        public override void Execute()
        {
        }

        public override void InitHelpers()
        {
            base.InitHelpers();
            Initializer.Init(this);
        }
    }

   
}
