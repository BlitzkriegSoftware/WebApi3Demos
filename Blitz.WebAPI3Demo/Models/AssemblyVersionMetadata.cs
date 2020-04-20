using System;

namespace Blitz.WebAPI3Demo.Models
{
    /// <summary>
    /// Model: Assembly Version Metadata
    /// </summary>
    public class AssemblyVersionMetadata
    {
        /// <summary>
        /// Copyright
        /// </summary>
        public string Copyright { get; set; } = string.Empty;

        /// <summary>
        /// Company
        /// </summary>
        public string Company { get; set; } = string.Empty;

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Semantic Version
        /// </summary>
        public string SemanticVersion { get; set; } = string.Empty;

        /// <summary>
        /// Informational Version
        /// </summary>
        public string InformationalVersion { get; set; } = string.Empty;

        /// <summary>
        /// slash + v + 1st segment of information version
        /// </summary>
        public string MajorVersion
        {
            get
            {
                var version = "1";
                if(!string.IsNullOrWhiteSpace(this.InformationalVersion))
                {
                    var data = this.InformationalVersion.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    if(data.Length > 0)
                    {
                        version = data[0];
                    }
                }

                return $"v{version}";
            }
        }

        /// <summary>
        /// Product
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// GitHub Url
        /// </summary>
        public string GitHubUrl { get; set; } = "https://github.com/BlitzkriegSoftware/WebApi3Demos";

        /// <summary>
        /// Team Name
        /// </summary>
        public string TeamName { get; set; } = "Blitzkrieg Software";

        /// <summary>
        /// Team Email
        /// </summary>
        public string TeamEmail { get; set; } = "stuart.t.williams@outlook.com";

        /// <summary>
        /// Property Get
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="value">value</param>
        public void PropertyGet(string name, string value)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            switch(name.ToLowerInvariant().Trim())
            {
                case "assemblycopyrightattribute": this.Copyright = value; break;
                case "assemblycompanyattribute": this.Company = value; break;
                case "assemblydescriptionattribute": this.Description = value; break;
                case "assemblyinformationversionattribute": this.InformationalVersion = value; break;
                case "assemblyfileversionattribute": this.SemanticVersion = value; break;
                case "assemblyproductattribute": this.Product = value; break;
            }
        }

        /// <summary>
        /// To String
        /// </summary>
        /// <returns>Formatted String</returns>
        public override string ToString()
        {
            return $"{this.Product} {this.Copyright} {this.SemanticVersion}\n{this.Description}";
        }
    }
}
