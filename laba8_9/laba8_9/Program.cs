using System;
using System.IO;

namespace laba8_9
{
    public interface IMyInterface<T>//1
    {
        void add(T obj);
        void del(T obj);
        void show();
    }

    public class Set<T>  : IMyInterface<T>//2
    {
        public int Size;

        public T[] set;
        public int count = 0;
        public DateTime date;

        public Set()//конструктор без параметров
        {
            Size = 100;
            set = new T[Size];
            date = DateTime.Now;
        }
        public Set(int _size)//конструктор с одним размером,а остальное без параметров
        {
            if (_size <= 0)
                throw new WrongSize("Wrong size for set!");
            Size = _size;
            set = new T[_size];
            date = DateTime.Now;
        }

        public bool isFull()//проверка на полноту
        {
            return (count == Size);
        }

        public void add(T elem)
        {
            if (isFull())
            {
                throw new SetIsFull("Set is full!");
            }
            for (int i = 0; i < count; i++)
            {
                if (set[i].Equals(elem))
                {
                    return;
                }
            }
            set[count++] = elem;
        }

        public void del(T elem)
        {
            int? c = null;
            for (int i = 0; i < count; i++)
            {
                if (set[i].Equals(elem))
                {
                    c = i;
                    break;
                }
            }

            if (c == null)
                throw new ElementDoesNotExist("There is no such element!");

            for (int i = (int)c; i < count - 1; i++)
            {
                set[i] = set[i + 1];
            }
            count--;
        }

        public void show()
        {
            Console.WriteLine("Elements:");
            string text = "";
            for (int i = 0; i < count; i++)
            {
                text += set[i] + "\n";
            }


            // запись в файл
            using (FileStream fstream = new FileStream($"./note.txt", FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }

            // чтение из файла
            using (FileStream fstream = File.OpenRead($"./note.txt"))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine($"Текст из файла: \n{textFromFile}");
            }

            Console.WriteLine();
        }

        public static Set<T> operator %(Set<T> set1, Set<T> set2)//перегрузка,только обобщенная
        {
            int newSize = 100;
            if (set1.Size >= set2.Size)
                newSize = set1.Size;
            else
                newSize = set2.Size;
            Set<T> newSet = new Set<T>(newSize);
            for (int i = 0; i < set1.count; i++)
            {
                for (int j = 0; j < set2.count; j++)
                {
                    if (set2.set[j].Equals(set1.set[i]))
                    {
                        newSet.add(set2.set[j]);
                        break;
                    }
                }
            }
            return newSet;
        }

        public static bool operator <(Set<T> set1, Set<T> set2)
        {
            int c = 0;
            for (int i = 0; i < set2.count; i++)
            {
                for (int j = 0; j < set1.count; j++)
                {
                    if (set1.set[j].Equals(set2.set[i]))
                    {
                        c++;
                        break;
                    }
                }
            }
            return (c == set2.count);
        }

        public static bool operator >(Set<T> set1, Set<T> set2)
        {
            int c = 0;
            for (int i = 0; i < set2.count; i++)
            {
                for (int j = 0; j < set1.count; j++)
                {
                    if (set1.set[j].Equals(set2.set[i]))
                    {
                        c++;
                        break;
                    }
                }
            }
            return (c != set2.count);
        }

        public static bool operator ==(Set<T> set1, Set<T> set2)
        {
            int c = 0;
            for (int i = 0; i < set1.count; i++)
            {
                for (int j = 0; j < set2.count; j++)
                {
                    if (set1.set[i].Equals(set2.set[j]))
                    {
                        c++;
                        break;
                    }
                }
            }
            return ((c == set1.count) && (c == set2.count));
        }

        public static bool operator !=(Set<T> set1, Set<T> set2)
        {
            int c = 0;
            for (int i = 0; i < set1.count; i++)
            {
                for (int j = 0; j < set2.count; j++)
                {
                    if (set1.set[i].Equals(set2.set[j]))
                    {
                        c++;
                        break;
                    }
                }
            }
            return ((c != set1.count) && (c != set2.count));
        }
    }

    class Program//3
    {
        static void Main(string[] args)
        {
            try
            {
                Set<int> set1 = new Set<int>();
                Set<Flower> set = new Set<Flower>(10);

                set.add(new Flower("Asd", "asdd", "qwe", "qwqq", "eqw", 400));
                set.add(new Flower("Asd", "asdd", "qwe", "qwqq", "eqw", 400));
                set.add(new Flower("Asd", "asdddqqe1", "312312312", "32131231", "e1231231", 400));
                set.show();

                set1.add(123);
                set1.add(23);

                Set<Flower> set2 = new Set<Flower>(-10);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.WriteLine("Finally!");
            }
        }
    }
}