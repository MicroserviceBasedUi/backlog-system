using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JiraAdapter.JiraBridge
{
    public class JiraIssue
    {
        public string Key { get; set; }

        public IDictionary<string, JToken> Fields { get; set; }
    }
}