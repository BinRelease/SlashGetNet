using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    /// <summary>
    /// A model representation of a fart
    /// </summary>
    public class Fart
    {
        /// <summary>
        /// Gets or sets the identifier of the fart.
        /// </summary>
        /// <value>
        /// The identifier of the fart.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the fart.
        /// </summary>
        /// <value>
        /// The name of the frat.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is a fart.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it is a fart; otherwise, <c>false</c>.
        /// </value>
        public bool IsFart { get; set; }
    }
}