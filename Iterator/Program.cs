using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var element in new UsersCollection())
            {
                Console.WriteLine(element);
            }
            Console.ReadKey();
        }
    }
    class UsersCollection: IEnumerable
    {
        private List<User> _users;
        public UsersCollection()
        {
            _users = new List<User>();
            _users.Add(new User("Pavel", 14));
            _users.Add(new User("Gregor", 44));
            _users.Add(new User("Tom", 29));
        }
        public int Count
        {
            get { return _users.Count(); }
        }
        public void AddItem(User item)
        {
            this._users.Add(item);
        }

        public IEnumerator GetEnumerator()
        {
            return new UserNameIterator(this);
        }

        public List<User> getItems()
        {
            return _users;
        }
    }
    class User
    {
        public User(string name, uint age)
        {
            Name = name;
            Age = age;
        }
        public string Name { get; set; }
        public uint Age { get; set; }
    }
    abstract class Iterator : IEnumerator
    {
        object IEnumerator.Current => Current();

        public abstract object Current();

        public abstract bool MoveNext();

        public abstract void Reset();
    }
    class UserNameIterator : Iterator
    {
        private UsersCollection _collection;
        private int _position = -1;

        public UserNameIterator(UsersCollection collection) => _collection = collection;

        public override object Current()
        {
            return this._collection.getItems()[_position].Name;
        }
  
        public override bool MoveNext()
        {
            int updatedPosition = this._position +  1;

            if (updatedPosition >= 0 && updatedPosition < this._collection.getItems().Count)
            {
                this._position = updatedPosition;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Reset() => this._position = 0;
    }

}
