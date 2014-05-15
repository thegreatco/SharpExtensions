using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpExtensions
{
    public class SortedObservableCollection<T> : SortedSet<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private readonly SimpleMonitor _monitor = new SimpleMonitor();
        
        public virtual event NotifyCollectionChangedEventHandler CollectionChanged;
        protected virtual event PropertyChangedEventHandler PropertyChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                PropertyChanged += value;
            }
            remove
            {
                PropertyChanged -= value;
            }
        }

        public SortedObservableCollection()
        {
        }

        public SortedObservableCollection(IList<T> list)
            : base(list)
        {
        }

        public SortedObservableCollection(IEnumerable<T> collection) : base(collection)
        {
        }

        public override void Clear()
        {
            CheckReentrancy();
            base.Clear();
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnCollectionReset();
        }

        public new bool Remove(T item)
        {
            CheckReentrancy();
            base.Remove(item);
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);
        }

        protected void Add(int index, T item)
        {
            CheckReentrancy();
            Add(item);
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, (object)item, index);
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged == null)
                return;
            this.PropertyChanged((object)this, e);
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.CollectionChanged == null)
                return;
            using (this.BlockReentrancy())
                this.CollectionChanged((object)this, e);
        }

        protected IDisposable BlockReentrancy()
        {
            this._monitor.Enter();
            return (IDisposable)this._monitor;
        }

        protected void CheckReentrancy()
        {
            if (this._monitor.Busy && this.CollectionChanged != null && this.CollectionChanged.GetInvocationList().Length > 1)
                throw new InvalidOperationException(SR.GetString("ObservableCollectionReentrancyNotAllowed"));
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
        {
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
        {
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
        {
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        }

        private void OnCollectionReset()
        {
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        [TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
        private class SimpleMonitor : IDisposable
        {
            private int _busyCount;

            public bool Busy
            {
                get
                {
                    return this._busyCount > 0;
                }
            }

            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            public SimpleMonitor()
            {
            }

            public void Enter()
            {
                ++this._busyCount;
            }

            public void Dispose()
            {
                --this._busyCount;
            }
        }
    }
}
