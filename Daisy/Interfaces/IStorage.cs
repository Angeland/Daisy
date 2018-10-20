using System.Collections.Generic;

namespace Daisy
{
    internal interface IStorage<T, I, Ruleset> where T : Storable<I, Ruleset>
    {
        T FetchLatest(T toStore);
        T FetchLatest(I identification);
        void Store(T newState, string previousJsonState);
        void Store(int identityKey, string previousHash, string newJsonState, string previousJsonState);
        string Diff(string newJsonState, string previousJsonState);
        void StoreDiff(I identityKey, string stateHash, string changelog);
        void UpdateState(int identityKey, string state);
        IEnumerable<I> GetIdentities();
        IEnumerable<string> GetPatchKeys(I key);
    }
}