using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
    public class AnimalQueue
    {
        public int AnimalsOrder = 0;
        public LinkedList<Dog> Dogs;
        public LinkedList<Cat> Cats;

        public AnimalQueue()
        {
            Dogs = new LinkedList<Dog>();
            Cats = new LinkedList<Cat>();
        }

        public void EnQueue(Animal a)
        {
            //we need to manage the order here during enqueue
            AnimalsOrder++;
            a.SetOrder(AnimalsOrder);


            //add to the corresponding linked list
            if (a is Dog)
            {
                Dogs.AddLast((Dog)a);
            }
            else if (a is Cat)
            {
                Cats.AddLast((Cat)a);
            }
            
        }


        public Dog DequeueDog()
        {
            Dog first = Dogs.First.Value;
            Dogs.RemoveFirst();

            return first;
        }

        public Cat DequeueCat()
        {
            Cat first = Cats.First.Value;
            Cats.RemoveFirst();

            return first;
        }

        public Animal DequeueAny()
        {
            Animal dog = (Animal) Dogs.First.Value;
            Animal cat = (Animal)Cats.First.Value;

            if (dog.IsOlderThan(cat))
            {
                //remove dog
                Dogs.RemoveFirst();
                return dog;

            }
            else
            {
                Cats.RemoveFirst();
                return cat;
            }


        }
    }

    public class Animal
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public Animal(string name)
        {
            this.Name = name;
        }

        public void SetOrder(int order)
        {
            this.Order = order;
        }

        public int GetOrder()
        {
            return Order;
        }

        //the one that has less order no is older
        public bool IsOlderThan(Animal a)
        {
            return (this.GetOrder() < a.GetOrder());
        }
      
    }


    public class Dog : Animal
    {
        public Dog(string name ="Dog") : base(name)
        {
           
        }
    }

    public class Cat : Animal
    {
        public Cat(string name ="Cat")
            : base(name)
        {

        }
    }
}
