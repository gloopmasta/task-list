using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_list
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CHECKLIST:
            // - mark function
            // - sort by: (newest added, alphabetical, ((most important)))
            //selector to mark/delete
            //edit function??
            //2 Times click mark changes background, 1 time changes text color

            //WHY SELECTION GO DOWN WITH DATEUP???
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;

            List<Task> taskList = new List<Task>();
            ConsoleKey inputKey;
            SortState sort = SortState.DateUp;
            string inputString;
            bool first = true;
            int selection = 0;
            bool justMarked = false;

            taskList.Add(new Task("Btask 1"));
            taskList.Add(new Task("Atask 2"));
            taskList.Add(new Task("Ctask 3"));

            do
            {
                Console.Clear();

                if (first)
                {
                    Console.WriteLine(" + Press Enter to add task.");
                    first = false;
                }
                else
                {
                    WriteTasklist(taskList, selection, justMarked, sort);
                    Console.WriteLine(" + Press Enter to add task.");
                }

                WriteBottomInfo(taskList, sort);


                inputKey = Console.ReadKey(true).Key; //READ KEY INPUT

                switch(inputKey)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        WriteTasklist(taskList, selection, justMarked, sort);
                        Console.CursorVisible = true;
                        Console.Write(" - ");
                        //taskList.Add(new Task(Console.ReadLine()));
                        inputString = Console.ReadLine();
                        if (inputString != "")
                        {
                            taskList.Add(new Task(inputString));
                        }
                        Console.CursorVisible = false;
                        break;

                    case ConsoleKey.UpArrow:
                        if (sort == SortState.DateUp)
                            selection++;
                        else
                            selection--;
                        justMarked = false;
                        break;

                    case ConsoleKey.DownArrow:
                        if (sort == SortState.DateUp)
                            selection--;
                        else
                            selection++;
                        justMarked = false;
                        break;

                    case ConsoleKey.M:
                        if (taskList[selection].Marked)
                            taskList[selection].Marked = false;
                        else
                            taskList[selection].Marked = true;

                        justMarked = true;
                            break;

                    case ConsoleKey.S:
                        if (sort == SortState.DateUp)
                            sort = SortState.DateDown;
                        else if (sort == SortState.DateDown)
                            sort = SortState.Alphabetical;
                        else if (sort == SortState.Alphabetical)
                            sort = SortState.DateUp;
                        break;

                    case ConsoleKey.Backspace:
                        if(taskList.Count > 0)
                            taskList.Remove(taskList[selection]);
                        break;

                    case ConsoleKey.Escape:
                        break;

                    default:
                        break;
                }

                if (selection < 0)
                    selection =+ taskList.Count - 1;
                if (selection > taskList.Count - 1)
                    selection -= taskList.Count;

            } while (inputKey != ConsoleKey.Escape);
            
        }
        static void WriteTasklist(List<Task> taskList, int selection, bool justMarked, SortState sort)
        {
            //Some shit for the alphabetical order
            List<string> namesList = new List<string>();
            List<Task> alphabeticalList = new List<Task>();

            switch (sort)
            {
                case SortState.DateUp: //DRAW WITH DateUp

                    for (int i = taskList.Count - 1; i >= 0; i--)
                    {
                        WriteTask(taskList, selection, justMarked, sort, i);
                    }
                        break;

                case SortState.DateDown: // sort DATEDOWN
                    for (int i = 0; i < taskList.Count; i++)
                    {
                        WriteTask(taskList, selection, justMarked, sort, i);
                    }
                    break;

                case SortState.Alphabetical: // sort ALPHABETICAL
                    namesList.Clear();
                    alphabeticalList.Clear();

                    foreach (Task item in taskList)
                    {
                        namesList.Add(item.TaskName);
                    }

                    namesList.Sort();

                    for (int i = 0; i < namesList.Count; i++)
                    {
                        for (int a = 0; a < taskList.Count; a++)
                        {
                            if (namesList[i] == taskList[a].TaskName)
                            {
                                alphabeticalList.Add(new Task(namesList[i], taskList[a].Marked));
                            }
                        }
                    }



                    for (int i = 0; i < taskList.Count; i++)
                    {
                        WriteTask(alphabeticalList, selection, justMarked, sort, i);
                    }

                    break;
            }
            
        }

        static void WriteTask(List<Task> taskList, int selection, bool justMarked, SortState sort, int i)
        {
            Console.Write(" - ");

            if (taskList[i].Marked == true && justMarked)
            {
                SetColor("m");
            }
            else if (i == selection)
            {
                SetColor("s");
            }
            else if (taskList[i].Marked == true)
            {
                SetColor("m");
            }
            Console.Write($"{taskList[i]}\n");

            SetColor();
        }

        static void SetColor(string type)
        {
            if (type == "m" || type == "M")
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (type == "s" || type == "S")
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }
        static void SetColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void WriteBottomInfo(List<Task> taskList, SortState sort)
        {
            for (int i = 0; i < Console.WindowHeight - taskList.Count - 4; i++)
            {
                Console.WriteLine();
            }
            Console.WriteLine($"Sorted by: {sort} (press \"S\" to change)");
            Console.WriteLine("Navigate with arrow keys, Press \"M\" to mark/unmark a task, Press Backspace to delete a task");
        }
    }
    public enum SortState
    {
        DateUp,
        DateDown,
        Alphabetical,
        //Marked
    }
}
