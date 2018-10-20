using Daisy.Exceptions;
using System;
using System.Collections.Generic;

namespace Daisy
{
    internal abstract class Ruleset<T, I, RuleCollection> where T : Storable<I, RuleCollection>, new()
    {
        private Dictionary<RuleCollection, List<Rule<T, I, RuleCollection>.Merge>> ruleset = new Dictionary<RuleCollection, List<Rule<T, I, RuleCollection>.Merge>>();
        public Ruleset()
        {
            SetupRules();
        }

        public abstract void SetupRules();

        protected void AddRule(RuleCollection selectedCollection, Rule<T, I, RuleCollection>.Merge rule)
        {
            ruleset.TryGetValue(selectedCollection, out List<Rule<T, I, RuleCollection>.Merge> set);
            if (null == set)
            {
                ruleset.Add(selectedCollection, new List<Rule<T, I, RuleCollection>.Merge>() { rule });
            }
            else
            {
                set.Add(rule);
            }
        }

        public T Merge(T from, T onto)
        {
            if (from == null)
            {
                throw new MergeException("from is null, you are attempting to merge null object to state");
            }
            if (onto == null)
            {
                Console.WriteLine($"New state for identifier {from.IdentityKey}");
                onto = new T() { IdentityKey = from.IdentityKey, SelectedRules = from.SelectedRules };
            }

            onto.PreviousHash = onto.HashedValue;
            foreach (Rule<T, I, RuleCollection>.Merge a in ruleset.GetValueOrDefault(from.SelectedRules, new List<Rule<T, I, RuleCollection>.Merge>()))
            {
                a.Invoke(from, onto);
            }
            return onto;
        }
    }
}