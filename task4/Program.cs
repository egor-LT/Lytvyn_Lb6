using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    public delegate void TaskExecution<TTask>(TTask task);

    public class TaskScheduler<TTask, TPriority>
    {
        private SortedDictionary<TPriority, Queue<TTask>> taskQueue = new SortedDictionary<TPriority, Queue<TTask>>();
        private Func<TTask> objectInitializer;
        private Action<TTask> objectReset;

        public TaskScheduler(Func<TTask> initializer, Action<TTask> reset)
        {
            objectInitializer = initializer;
            objectReset = reset;
        }

        public void AddTask(TTask task, TPriority priority)
        {
            if (!taskQueue.ContainsKey(priority))
            {
                taskQueue[priority] = new Queue<TTask>();
            }

            taskQueue[priority].Enqueue(task);
        }

        public void ExecuteNext(TaskExecution<TTask> executeTask)
        {
            if (taskQueue.Count == 0)
            {
                Console.WriteLine("Черга завдань порожня.");
                return;
            }

            var highestPriority = taskQueue.Keys.GetEnumerator().Current;
            var nextTask = taskQueue[highestPriority].Dequeue();
            executeTask(nextTask);
        }

        public TTask GetObjectFromPool()
        {
            return objectInitializer();
        }

        public void ReturnObjectToPool(TTask obj)
        {
            objectReset(obj);
        }
    }

    class Program
    {
        static void Main()
        {
            TaskScheduler<string, int> scheduler = new TaskScheduler<string, int>(
                () => string.Empty,
                (s) => { /* reset logic, if needed */ });

            while (true)
            {
                Console.WriteLine("Введіть завдання:");
                string task = Console.ReadLine();

                Console.WriteLine("Введіть пріоритет:");
                int priority;
                if (!int.TryParse(Console.ReadLine(), out priority))
                {
                    Console.WriteLine("Некоректний пріоритет. Введіть ціле число.");
                    continue;
                }

                scheduler.AddTask(task, priority);

                
                scheduler.ExecuteNext((t) => Console.WriteLine($"Виконується завдання: {t}"));
            }
        }
    }
    }