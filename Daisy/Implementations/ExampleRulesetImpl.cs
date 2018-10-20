using Daisy.Implementations;
using System;

namespace Daisy
{
    internal class ExampleRulesetImpl : Ruleset<ExampleStorableImpl, int, ExampleRuleCollection>
    {

        public override void SetupRules()
        {
            RulesForZero();
            RulesForOne();
        }


        private void RulesForZero()
        {
            AddRule(ExampleRuleCollection.ZERO, (a, b) =>
            {
                b.Time = a.Time;
            });
            AddRule(ExampleRuleCollection.ZERO, (a, b) =>
            {
                b.Weight = a.Weight;
            });
            AddRule(ExampleRuleCollection.ZERO, (a, b) =>
            {
                b.Width = a.Width + 100;
            });
        }
        private void RulesForOne()
        {
            AddRule(ExampleRuleCollection.ONE, (a, b) =>
            {
                b.Height = a.Height + 10;
            });
            AddRule(ExampleRuleCollection.ONE, (a, b) =>
            {
                b.Width = a.Width + 100;
            });
            AddRule(ExampleRuleCollection.ONE, (a, b) =>
            {
                b.Length = a.Length + 100;
            });
        }
    }
}