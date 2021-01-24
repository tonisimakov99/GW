using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWLibrary
{
    public class GetRequestBuilder
    {
        private Uri uri;
        private Dictionary<string, string> Parameters;
        public GetRequestBuilder(Uri uri):this(uri, new Dictionary<string, string>())
        {
        }
        public GetRequestBuilder(Uri uri, Dictionary<string,string> parameters)
        {
            this.uri = uri;
            Parameters = parameters;
        }
        public GetRequestBuilder Append(string key, string value)
        {
            Parameters.Add(key, value);
            return this;
        }
        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(uri);
            strBuilder.Append('?');
            foreach(var keyValue in Parameters)
            {
                strBuilder.Append($"{keyValue.Key}={keyValue.Value}&");
            }
            strBuilder.Remove(strBuilder.Length-1, 1);
            return strBuilder.ToString();
        }
    }
}
