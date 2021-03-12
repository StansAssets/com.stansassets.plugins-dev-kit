using System.Linq;

namespace StansAssets.Plugins.Editor
{
    public class GitHubRelease
    {
        public string Name { get; private set; }
        public string Tag { get; private set; }

        public void ReadJson (string json)
        {
            Name = FindValueByKey ("name", json);
            Tag = FindValueByKey ("tag_name", json);
        }

        string FindValueByKey (string key, string json)
        {
            // number of characters from the 1st char of the given key to the 1st char of corresponding value
            int offsetBeforeValue = key.Length + 5;
            string lookForString = $"\"{key}\"";
            int position = json.IndexOf (lookForString) + offsetBeforeValue;
            var value = (position > offsetBeforeValue) 
                            ? new string(json.Skip (position).TakeWhile (c => c != '"').ToArray ()) 
                            : string.Empty;
            return value;
        }
    }
}
