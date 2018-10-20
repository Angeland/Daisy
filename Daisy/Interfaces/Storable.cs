using Daisy.Implementations;
using Daisy.Interfaces;
using Newtonsoft.Json;
using System;

namespace Daisy
{
    internal class Storable<I, Ruleset>
    {
        public Storable() { }
        [JsonIgnore]
        public string ThisJson { get { return JsonConvert.SerializeObject(this); } }
        [JsonIgnore]
        public virtual string HashedValue { get { return Hashers.ComputeHashSHA1(ThisJson); } }
        public string PreviousHash { get; set; }
        public I IdentityKey { get; set; }
        public Ruleset SelectedRules { get; set; }
        public string RulesetName => SelectedRules.ToString();
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}