using Daisy.Implementations;
using System;

namespace Daisy
{
    internal class ExampleImporterImpl : IImporter<ExampleStorableImpl, int, ExampleRuleCollection>
    {
        private int _value = 0;
        private int identifier = 1;
        private readonly int count;

        public ExampleImporterImpl(int count)
        {
            this.count = count;
        }

        public ExampleStorableImpl FetchNext()
        {
            identifier = 1 - identifier; //flip 1<->0
            return new ExampleStorableImpl()
            {
                IdentityKey = identifier,
                Length = _value * 10,
                Height = _value++,
                Time = DateTime.Now,
                Weight = _value * 1.2,
                Width = _value + 10,
                SelectedRules = identifier == 0 ? ExampleRuleCollection.ZERO : ExampleRuleCollection.ONE
            };
        }

        public bool HasWork()
        {
            return _value < count;
        }
    }
}