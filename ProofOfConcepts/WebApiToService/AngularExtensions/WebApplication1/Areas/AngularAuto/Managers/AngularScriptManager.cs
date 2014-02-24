using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApplication1.Areas.AngularAuto
{
    public static class AngularScriptManager
    {
        public static void GenerateScripts()
        {
            var apidescriptions = GlobalConfiguration.Configuration.Services.GetApiExplorer().ApiDescriptions;
            if (apidescriptions != null && apidescriptions.Count > 0)
            {
                // api descriptions are grouped into their paths
                Dictionary<string, List<ApiDescription>> groupedApiCalls = new Dictionary<string, List<ApiDescription>>();

                foreach (var api in apidescriptions)
                {
                    StringBuilder sb = new StringBuilder();
                    var path = api.RelativePath;
                    int splitIndex = path.IndexOf('?');
                    if (splitIndex > -1)
                    {
                        path = path.Substring(0, splitIndex);
                    }

                    var idPathIndex = path.IndexOf("/{");
                    if(idPathIndex > -1)
                    {
                        path = path.Substring(0, idPathIndex);
                    }

                    if (groupedApiCalls.ContainsKey(path))
                    {
                        groupedApiCalls[path].Add(api);
                    }
                    else
                    {
                        groupedApiCalls.Add(path, new List<ApiDescription>() { api });
                    }
                }

                foreach(var kvp in groupedApiCalls)
                {
                    CreateFilesForApi(kvp.Key, kvp.Value);
                }
            }
        }

        private static void CreateFilesForApi(string relativeBarePath, List<ApiDescription> apis)
        {
            var path = string.Format("AngularAuto/Scripts/{0}Repository.js", relativeBarePath);
            var physicalFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/" + path);
            var physicalFolderPath = Path.GetDirectoryName(physicalFilePath);

            var repositoryName = Path.GetFileNameWithoutExtension(physicalFilePath);
            repositoryName = repositoryName.ToCamelCase();// Char.ToLowerInvariant(repositoryName[0]) + repositoryName.Substring(1);

            if (!Directory.Exists(physicalFolderPath))
            {
                Directory.CreateDirectory(physicalFolderPath);
            }

            List<string> jsData = new List<string>();

            jsData.Add(string.Format("angularAppModule.factory('{0}', function ($http, $q) {{", repositoryName));
            jsData.Add("return {");

            //add each method
            List<string> usedFunctionNames = new List<string>();
            int i = 0;
            foreach(var api in apis)
            {
                var functionName = api.ActionDescriptor.ActionName.ToCamelCase();

                if(usedFunctionNames.Contains(functionName))
                {
                    throw new Exception("WebAPI method names must be unique within a given controller, for Angular's sake! Path: " + relativeBarePath + ", " + " Function Name: " + functionName);
                }
                else
                {
                    usedFunctionNames.Add(functionName);
                }

                string method = api.HttpMethod.Method;
                /*
                 * getProjectAreas: function () {
                        var deferred = $q.defer();
                        $http.get('/api/projectarea').success(deferred.resolve).error(deferred.reject);
                        return deferred.promise;
                    },
                 */
                var properGetPath = SetupAjaxPathWithGetVariables(api.RelativePath, api.ParameterDescriptions);
                var postParameter = SetupAjaxPostParameter(api.ParameterDescriptions);

                var arguments = BuildArguments(api.ParameterDescriptions.ToArray());
                jsData.Add(string.Format("{0}: function ({1}) {{", functionName, arguments));
                jsData.Add("     var deferred = $q.defer();");
                jsData.Add(string.Format("     $http.{0}('/{1}'{2}).success(deferred.resolve).error(deferred.reject);", method.ToLower(), properGetPath, postParameter));
                jsData.Add("     return deferred.promise;");
                
                //the comma is the difference here
                if(i != apis.Count-1)
                {
                    jsData.Add("     },");
                }
                else
                {
                    jsData.Add("     }");
                }
                i += 1;
            }

            jsData.Add("}});");

            System.IO.File.WriteAllLines(physicalFilePath, jsData.ToArray());
        }

        private static string SetupAjaxPostParameter(System.Collections.ObjectModel.Collection<ApiParameterDescription> parameters)
        {
            var returnValue = string.Empty;

            if(parameters != null && parameters.Count > 0 )
            {
                var postParameters = parameters.Where(x => x.Source == ApiParameterSource.FromBody);
                if(postParameters != null && postParameters.Count() == 1)
                {
                    var p = postParameters.First();

                    returnValue = string.Format(", {0}", p.Name.ToCamelCase());

                }
                else if(postParameters.Count() > 1) // more than one. there can only be one!
                {
                    throw new Exception(string.Format("There can only be one body parameter in a WebAPI call!"));
                }
            }

            return returnValue;
        }

        private static string SetupAjaxPathWithGetVariables(string path, System.Collections.ObjectModel.Collection<ApiParameterDescription> parameters)
        {
            var returnValue = path;
            foreach(var p in parameters)
            {
                if(p.Source == ApiParameterSource.FromUri)
                {
                    string toBeReplaced = string.Format("{{{0}}}", p.Name);
                    string jsInjection = string.Format("' + {0} + '", p.Name.ToCamelCase());
                    returnValue = returnValue.Replace(toBeReplaced, jsInjection);
                }
            }
            return returnValue;
        }

        private static string ToCamelCase(this string str)
        {
            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        private static string BuildArguments(ApiParameterDescription[] parameters)
        {
            if(parameters == null || parameters.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            int i = 0;
            foreach(var p in parameters)
            {
                sb.Append(p.Name);
                if(i!= parameters.Length-1)
                {
                    sb.Append(",");
                }
                i += 1;
            }

            return sb.ToString();
        }
    }
}