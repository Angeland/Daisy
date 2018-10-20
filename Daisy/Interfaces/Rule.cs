namespace Daisy
{
    internal abstract class Rule<T, I, RuleCollection> where T: Storable<I, RuleCollection>
    {
        public delegate void Merge(T from, T onto);
    }
}