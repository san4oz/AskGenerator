using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// </summary>
        /// <typeparam name="TDestination">Destination type.</typeparam>
        /// <param name="obj">Source object to map from.</param>
        /// <returns>Mapped destination object.</returns>
        protected TDestination Map<TDestination>(object obj)
        {
            return Mapper.Map<TDestination>(obj);
        }

        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// </summary>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TDestination">Destination type.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <returns>Mapped destination object.</returns>
        protected TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }
    }
}
