using System;

namespace Vlingo.Xoom.Annotation.Persistence
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class Persistence : Attribute
    {
        private StorageType _storageType;
        private string _basePackage;

        public Persistence(StorageType storageType, string basePackage)
        {
            _storageType = storageType;
            _basePackage = basePackage;
        }

        bool Cqrs() => false;

        public bool IsStateStore() => _storageType == StorageType.StateStore;

        public bool IsJournal() => _storageType == StorageType.Journal;

        public bool IsObjectStore() => _storageType == StorageType.ObjectStore;

    }

    public enum StorageType
    {
        None,
        StateStore,
        ObjectStore,
        Journal
    }
}
