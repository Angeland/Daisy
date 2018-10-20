using Daisy.Implementations;
using JsonDiffPatchDotNet;
using System.Collections.Generic;
using System.Linq;

namespace Daisy
{
    internal class DefaultStorageImpl : IStorage<ExampleStorableImpl, int, ExampleRuleCollection>
    {
        private readonly Dictionary<int, string> _storage = new Dictionary<int, string>();
        private readonly Dictionary<int, List<(string stateHash, string changelog)>> _changelog = new Dictionary<int, List<(string stateHash, string changelog)>>();
        private static readonly JsonDiffPatch JSON_DIFF_PATCH = new JsonDiffPatch();
        public virtual ExampleStorableImpl FetchLatest(ExampleStorableImpl toStore)
        {
            return FetchLatest(toStore.IdentityKey);
        }
        public virtual ExampleStorableImpl FetchLatest(int identifier)
        {
            _storage.TryGetValue(identifier, out string o);
            if (string.IsNullOrEmpty(o))
            {
                return null;
            }
            return ExampleStorableImpl.Deserialize<ExampleStorableImpl>(o);
        }

        public virtual void Store(ExampleStorableImpl newState, string previousJsonState)
        {
            Store(newState.IdentityKey, newState.PreviousHash, newState.ThisJson, previousJsonState);
        }
        public virtual void Store(int identityKey, string previousHash, string newJsonState, string previousJsonState)
        {
            StoreDiff(identityKey, previousHash, Diff(newJsonState, previousJsonState));
            UpdateState(identityKey, newJsonState);
        }

        public IEnumerable<int> GetIdentities()
        {
            return _changelog.Keys;
        }
        public IEnumerable<string> GetPatchKeys(int key)
        {
            return _changelog[key].Select(a => a.stateHash);
        }

        public string GetState(int key, string stateHash)
        {
            string latest = _storage[key];
            foreach ((string stateHash, string changelog) a in _changelog[key])
            {
                latest = PatchBackwards(latest, a.changelog);
                if (a.stateHash == stateHash)
                {
                    return latest;
                }
            }
            return null;
        }

        public virtual string Diff(string newJsonState, string previousJsonState)
        {
            return JSON_DIFF_PATCH.Diff(previousJsonState, newJsonState);
        }
        public virtual string PatchBackwards(string jsonState, string patch)
        {
            return JSON_DIFF_PATCH.Patch(jsonState, patch);
        }
        public virtual string PatchForwards(string jsonState, string patch)
        {
            return JSON_DIFF_PATCH.Unpatch(jsonState, patch);
        }
        public virtual void StoreDiff(int identityKey, string stateHash, string changelog)
        {
            if (!_changelog.ContainsKey(identityKey))
            {
                _changelog[identityKey] = new List<(string stateHash, string changelog)>();
            }
            _changelog[identityKey].Add((stateHash: stateHash, changelog: changelog));
        }
        public virtual void UpdateState(int identityKey, string state)
        {
            _storage[identityKey] = state;
        }
    }
}