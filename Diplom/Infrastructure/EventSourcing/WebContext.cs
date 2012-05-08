﻿using System.Web;

namespace Infrastructure.EventSourcing
{
    /// <summary>
    /// Implements a context for web applications.
    /// </summary>
    public class WebContext : IContext
    {
        public object this[object key]
        {
            get
            {
                return HttpContext.Current.Items[key];
            }
            set
            {
                HttpContext.Current.Items[key] = value;
            }
        }
    }
}
