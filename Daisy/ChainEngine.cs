using System.Collections.Generic;

namespace Daisy
{
    internal class ChainEngine<T, I, Ruleset> where T : Storable<I, Ruleset>, new()
    {
        private readonly Ruleset<T, I, Ruleset> ruleset;
        private readonly IImporter<T, I, Ruleset> importer;
        private readonly IStorage<T, I, Ruleset> storage;
        private readonly Queue<T> WorkQueue = new Queue<T>();

        public ChainEngine(Ruleset<T, I, Ruleset> ruleset, IImporter<T, I, Ruleset> importer, IStorage<T, I, Ruleset> storage)
        {
            this.ruleset = ruleset;
            this.importer = importer;
            this.storage = storage;
        }

        internal void Start()
        {
            StartImporter();
            RunBuilder();
        }

        private void RunBuilder()
        {
            while (WorkQueue.TryDequeue(out T newChanges))
            {
                T state = storage.FetchLatest(newChanges.IdentityKey);
                string oldState = state == null ? "{}" : state.ThisJson;
                T newState = ruleset.Merge(newChanges, state);
                storage.Store(newState, oldState);
            }
        }

        private void StartImporter()
        {
            while (importer.HasWork())
            {
                WorkQueue.Enqueue(importer.FetchNext());
            }
        }
    }
}
