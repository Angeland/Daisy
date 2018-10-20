using Daisy.Implementations;
using System;

namespace Daisy
{
    internal class ExampleStorableImpl : Storable<int, ExampleRuleCollection>
    {
        public ExampleStorableImpl() { }

        public DateTime Time { get; set; }
        public double Weight { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
        /*
public CustomStorable Deserialize(string json)
{
return JsonConvert.DeserializeObject<CustomStorable>(json);
}*/


        /*public override ulong HashedValue()
        {
            return "overridden hash";
        }*/
    }
}