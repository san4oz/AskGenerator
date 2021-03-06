﻿using System;
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
    }
    public class BaseWebViewPage<TModel> : WebViewPage<TModel>, IBaseWebViewPage
    {
        public bool IsEditing { get; set; }

        public Resolver R { get; set; }

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

        public Resolver R { get; set; }

        public override void Execute()
        {
        }

        public override void InitHelpers()
        {
            base.InitHelpers();
            Initializer.Init(this);
        }
    }

    static class Initializer
    {
        public static void Init<T>(T page) where T : WebViewPage, IBaseWebViewPage
        {
            page.IsEditing = page.ViewBag.IsEditing ?? false;
            page.R = new Resolver();
        }
    }
}
